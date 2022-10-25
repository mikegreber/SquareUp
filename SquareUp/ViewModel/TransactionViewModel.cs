using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SquareUp.Model;
using SquareUp.Services.Session;
using SquareUp.Shared.Models;
using IQueryAttributable = Microsoft.Maui.Controls.IQueryAttributable;
using static SquareUp.Resources.Statics.TransactionCategories;
namespace SquareUp.ViewModel;

public partial class TransactionViewModel : BaseViewModel, IQueryAttributable
{
    [ObservableProperty]
    private ObservableTransaction _transaction = new();

    [ObservableProperty] 
    private PageMode _pageMode;

    [ObservableProperty]
    private string _title = string.Empty;

    public TransactionViewModel(ISessionData session) : base(session)
    {
        AnimateBackTransitions = false;
    }

    public async void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        await Task.Delay(1);

        Title = (string)query[nameof(Title)];
        Transaction = (ObservableTransaction)query[nameof(Transaction)];
        PageMode = (PageMode)query[nameof(PageMode)];
        Transaction.Amount = Math.Round(Transaction.Amount, 2);
        await Task.Delay(1);

        Transaction.Participant = Session.Group.Participants.FirstOrDefault(p => p.Id == Transaction.ParticipantId, new());
        Transaction.SecondaryParticipant = Session.Group.Participants.FirstOrDefault(p => p.Id == Transaction.SecondaryParticipantId, new());
    }

    public static Dictionary<string, object> Params(ObservableTransaction transaction, PageMode pageMode, string title) => new()
    {
        [nameof(Title)] = title,
        [nameof(Transaction)] = transaction,
        [nameof(PageMode)] = pageMode
    };

    [RelayCommand]
    private async Task Create()
    {
        Transaction.Type = Transaction.Category switch
        {
            Income => SplitType.Income,
            Transfer => SplitType.Payment,
            _ => Transaction.Type
        };

        await Session.CreateTransaction(Transaction);
        await Shell.Current.GoToAsync("..", false);
    }

    [RelayCommand]
    private async Task Update()
    {
        await Session.UpdateTransaction(Transaction);
        await Shell.Current.GoToAsync("..", false);
    }

    [RelayCommand]
    private async Task Delete()
    {
        await Session.DeleteTransaction(Transaction);
        await Shell.Current.GoToAsync("..", false);
    }
}


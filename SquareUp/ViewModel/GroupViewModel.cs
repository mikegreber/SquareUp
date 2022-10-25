using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SquareUp.Model;
using SquareUp.Shared.Types;
using SquareUp.View;
using IQueryAttributable = Microsoft.Maui.Controls.IQueryAttributable;
using SquareUp.Services.Session;

namespace SquareUp.ViewModel;

public partial class GroupViewModel : BaseViewModel, IQueryAttributable
{
    [ObservableProperty] 
    private FullyObservableCollection<Debt> _debts = new();

    public GroupViewModel(ISessionData session) : base(session) { }

    public async void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        var group = (ObservableGroupInfo)query["Group"];

        // TODO set colors and details so we don't have to wait for this call to finish
        await Session.GetGroup(group.Id);
    }

    public static Dictionary<string, object> Params(ObservableGroupInfo group) => new()
    {
        ["Group"] = group
    };

    [RelayCommand]
    private async Task TapSettings()
    {
        await GroupDetailsPage.OpenAsync(new ObservableGroup(Session.Group), PageMode.Edit, "Group Settings");
    }

    [RelayCommand]
    private async Task TapDebts()
    {
        await DebtsPage.OpenAsync(Session.Group);
    }

    public override Func<Task> OnActionButtonClicked { get; set; } = async () =>
    {
        await TransactionPage.OpenAsync(new ObservableTransaction(), PageMode.Create, "New Transaction");
    };

    [RelayCommand]
    private async Task TapTransaction(ObservableTransaction transaction)
    {
        await TransactionPage.OpenAsync(transaction, PageMode.Edit, "Edit Transaction");
    }
}
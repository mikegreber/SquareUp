using CommunityToolkit.Mvvm.ComponentModel;
using SquareUp.Shared.Models;
using SquareUp.Shared.Types;

namespace SquareUp.Model;

[ObservableObject]
public partial class ObservableTransaction : ITransactionBase, IUpdateable<ObservableTransaction>
{
    public int Id { get; set; }

    [ObservableProperty] 
    private string _name = string.Empty;

    [ObservableProperty] 
    private decimal _amount;

    [ObservableProperty]
    private string _category = string.Empty;

    [ObservableProperty]
    private SplitType _type;

    public DateTime Date { get; set; }

    [ObservableProperty]
    private int _participantId;

    [ObservableProperty]
    private int _SecondaryParticipantId;

    [ObservableProperty] 
    private FullyObservableCollection<ObservableParticipant> _participants;

    [ObservableProperty] 
    private ObservableParticipant _participant = new(){Name="DefaultName"};

    [ObservableProperty]
    private ObservableParticipant _secondaryParticipant = new() { Name = "DefaultName" };

    public ObservableTransaction() { }
    public ObservableTransaction(ITransactionBase transaction, FullyObservableCollection<ObservableParticipant> participants)
    {
        Id = transaction.Id;
        Name = transaction.Name;
        Amount = transaction.Amount;
        Category = transaction.Category;
        Date = transaction.Date;
        Type = transaction.Type;
        ParticipantId = transaction.ParticipantId;
        SecondaryParticipantId = transaction.SecondaryParticipantId;

        Participants = participants;
        Participant = participants.FirstOrDefault(p => p.Id == ParticipantId, new ObservableParticipant { Name = "None" });
        SecondaryParticipant = participants.FirstOrDefault(p => p.Id == SecondaryParticipantId, new ObservableParticipant { Name = "None" });
    }

    public void Update(ObservableTransaction obj)
    {
        Name = obj.Name;
        Amount = obj.Amount;
        Type = obj.Type;
        Category = obj.Category;
        ParticipantId = obj.ParticipantId;
    }

    public static implicit operator TransactionBase(ObservableTransaction transaction) => new(transaction);
}
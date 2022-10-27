using System.ComponentModel;
using CommunityToolkit.Mvvm.ComponentModel;
using SquareUp.Shared.Models;
using SquareUp.Shared.Types;

namespace SquareUp.Model;

[ObservableObject]
public partial class ObservableGroupBase : IGroupBase, IUpdateable<IGroupBase>
{
    public ObservableGroupBase() { }
    public ObservableGroupBase(IGroupBase group)
    {
        Id = group.Id;
        Name = group.Name;
        Color = group.Color;
        Date = group.Date;
        LastEdit = group.LastEdit;
    }
    public int Id { get; set; }
    public DateTime Date { get; set; }

    [ObservableProperty]
    private string _color = string.Empty;

    [ObservableProperty] 
    private DateTime _lastEdit;

    [ObservableProperty]
    private string _name = string.Empty;

    public void Update(IGroupBase obj)
    {
        Name = obj.Name;
        Color = obj.Color;
        LastEdit = obj.LastEdit;
    }

    public static implicit operator GroupBase(ObservableGroupBase group) => new(group);
}

public partial class ObservableGroupInfo : ObservableGroupBase, IGroupInfo, IUpdateable<IGroupInfo>
{
    public ObservableGroupInfo() { }

    public ObservableGroupInfo(IGroupBase group, int users) : base(group)
    {
        Participants = users;
    }

    public ObservableGroupInfo(IGroupInfo group) : base(group)
    {
        Participants = group.Participants;
    }

    [ObservableProperty] 
    private int _participants;

    public void Update(IGroupInfo obj)
    {
        base.Update(obj);
        Participants = obj.Participants;
    }

    public static implicit operator ObservableGroupInfo(ObservableGroup group) => new(group, group.Users.Count);
}

public partial class ObservableGroup : ObservableGroupBase,
    IGroup<FullyObservableCollection<ObservableUserBase>, FullyObservableCollection<ObservableParticipant>, GroupedTransactionCollection>, IUpdateable<ObservableGroup>
{
    [ObservableProperty]
    private FullyObservableCollection<ObservableUserBase> _users = new();

    [ObservableProperty]
    private GroupedTransactionCollection _transactions = new();

    [ObservableProperty]
    private FullyObservableCollection<ObservableParticipant> _participants = new();

    [ObservableProperty]
    private FullyObservableCollection<Debt> _debts = new();

    [ObservableProperty]
    private FullyObservableCollection<Settlement> _settlements = new();

    public ObservableGroup() { }

    public ObservableGroup(ObservableGroup group) : base(group)
    {
        Users = new FullyObservableCollection<ObservableUserBase>(group.Users);
        Transactions = new GroupedTransactionCollection(group.Transactions.SelectMany(t => t));
        Participants = new FullyObservableCollection<ObservableParticipant>(group.Participants);
    }

    public ObservableGroup(Group<List<UserBase>, List<Participant>, List<TransactionBase>> group) : base(group)
    {
        Users = new FullyObservableCollection<ObservableUserBase>(group.Users.Select(u => new ObservableUserBase(u)));
        Participants = new FullyObservableCollection<ObservableParticipant>(group.Participants.Select(p => new ObservableParticipant(p)));
        Transactions = new GroupedTransactionCollection(group.Transactions.Select(e => new ObservableTransaction(e, Participants)));
    }

    public static implicit operator ObservableGroup(Group<List<UserBase>, List<Participant>, List<TransactionBase>> group) => new(group);

    public void Update(ObservableGroup obj)
    {
        base.Update(obj);
        Transactions = obj.Transactions;
        Participants = obj.Participants;
        Users = obj.Users;
        CalculateDebts();
    }

    public void Add(ObservableTransaction transaction)
    {
        transaction.Participant = Participants.FirstOrDefault(p => p.Id == transaction.ParticipantId, new ObservableParticipant());
        transaction.SecondaryParticipant = Participants.FirstOrDefault(p => p.Id == transaction.SecondaryParticipantId, new ObservableParticipant());
        Transactions.Add(transaction);
        CalculateDebts();
    }

    public void Add(ObservableUserBase user)
    {
        Users.Add(user);
    }

    public void Add(ObservableParticipant participant)
    {
        Participants.Add(participant);
    }

    public void Update(ObservableTransaction transaction)
    {
        Transactions.Update(transaction);
        CalculateDebts();
    }

    public void Remove(ObservableTransaction transaction)
    {
        Transactions.Delete(transaction);
        CalculateDebts();
    }

    public void Remove(ObservableParticipant participant)
    {
        Participants.Remove(participant);
    }

    protected override void OnPropertyChanged(PropertyChangedEventArgs e)
    {
        base.OnPropertyChanged(e);
        if (e.PropertyName == nameof(Transactions))
        {
            CalculateDebts();
            CalculateSettlements();
        }
    }

    private void CalculateDebts()
    {
        Dictionary<SplitType, decimal> total = new()
        {
            [SplitType.EvenlySplit] = 0,
            [SplitType.IncomeProportional] = 0,
            [SplitType.Income] = 0,
            [SplitType.Payment] = 0,
        };

        foreach (var transaction in Transactions.SelectMany(x => x))
            total[transaction.Type] += transaction.Amount;

        var debts = Participants.Select(participant => new Debt(participant, this, total)).ToList();
        debts.Sort((a, b) => a.Amount.CompareTo(b.Amount));

        Debts = new FullyObservableCollection<Debt>(debts);

        CalculateSettlements();
        //OnPropertyChanged(new PropertyChangedEventArgs(nameof(Debts)));
    }

    private void CalculateSettlements()
    {
        var owing = Debts
            .Where(d => d.Amount > 0)
            .Select(d => new DebtBase { Participant = d.Participant, Amount = d.Amount })
            .ToList();
        owing.Sort((a, b) => b.Amount.CompareTo(a.Amount));

        var owed = Debts
            .Where(d => d.Amount < 0)
            .Select(d => new DebtBase { Participant = d.Participant, Amount = -d.Amount })
            .ToList();
        owed.Sort((a, b) => b.Amount.CompareTo(a.Amount));

        var settlements = new List<Settlement>();
        while (owing.Count > 0 && owed.Count > 0)
        {
            var settlement = new Settlement { From = owing.First().Participant, To = owed.First().Participant };

            if (owed.First().Amount > owing.First().Amount)
            {
                settlement.Amount = owing.First().Amount;
                owed.First().Amount -= owing.First().Amount;
                owing.Remove(owing.First());
            }
            else if (owed.First().Amount < owing.First().Amount)
            {
                settlement.Amount = owed.First().Amount;
                owing.First().Amount -= owed.First().Amount;
                owed.Remove(owed.First());
            }
            else
            {
                settlement.Amount = owing.First().Amount;
                owed.Remove(owed.First());
                owing.Remove(owing.First());
            }

            if (settlement.Amount > 0.1m)
            {
                settlements.Add(settlement);
            }
        }

        Settlements = new FullyObservableCollection<Settlement>(settlements);
    }
}

public class GroupedTransactionCollection : GroupedFullyObservableCollection<DateTime, ObservableTransaction>
{
    public GroupedTransactionCollection() : base(
        compareGroups: (time, transaction) => transaction.Date.Date.CompareTo(time),
        compareItems: (e1, e2) => e2.Date.CompareTo(e1.Date),
        getGroupKey: transaction => transaction.Date.Date
    ) { }

    public GroupedTransactionCollection(IEnumerable<ObservableTransaction> transactions) : base(transactions, compareGroups: (time, transaction) => transaction.Date.Date.CompareTo(time),
        compareItems: (e1, e2) => e2.Date.CompareTo(e1.Date),
        getGroupKey: transaction => transaction.Date.Date
    ) { }
}

public partial class ObservableParticipant : ObservableObject, IParticipant
{
    public ObservableParticipant() { }
    public ObservableParticipant(IParticipant participant)
    {
        Id = participant.Id;
        UserId = participant.UserId;
        Name = participant.Name;
    }

    public int Id { get; set; }

    [ObservableProperty] 
    private int _userId;

    [ObservableProperty]
    private string _name = string.Empty;
}
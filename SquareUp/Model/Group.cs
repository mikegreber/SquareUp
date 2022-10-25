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
    }

    public void Add(ObservableTransaction transaction)
    {
        transaction.Participant = Participants.FirstOrDefault(p => p.Id == transaction.ParticipantId, new ObservableParticipant());
        transaction.SecondaryParticipant = Participants.FirstOrDefault(p => p.Id == transaction.SecondaryParticipantId, new ObservableParticipant());
        Transactions.Add(transaction);
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
    }

    public void Remove(ObservableTransaction transaction)
    {
        Transactions.Delete(transaction);
    }

    public void Remove(ObservableParticipant participant)
    {
        Participants.Remove(participant);
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
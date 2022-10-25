using System.Collections;

namespace SquareUp.Shared.Models;

public interface IGroupBase : IId
{
    public string Name { get; set; }
    public string Color { get; set; }
    public DateTime Date { get; set; }
    public DateTime LastEdit { get; set; }
}

public interface IGroup<TUserCollection, TParticipantCollection, TTransactionCollection> : IGroupBase
    where TUserCollection : ICollection, new()
    where TTransactionCollection : ICollection, new()
    where TParticipantCollection : ICollection, new()
{
    public TUserCollection Users { get; set; }

    public TParticipantCollection Participants { get; set; }

    public TTransactionCollection Transactions { get; set; }
}

public interface IGroupInfo : IGroupBase
{
    public int Participants { get; set; }
}

public class GroupBase : IGroupBase
{
    public GroupBase() { }
    public GroupBase(IGroupBase group)
    {
        Id = group.Id;
        Name = group.Name;
        Color = group.Color;
        Date = group.Date;
        LastEdit = group.LastEdit;
    }

    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Color { get; set; } = string.Empty;
    public DateTime Date { get; set; }
    public DateTime LastEdit { get; set; }
}

public class GroupInfo : GroupBase, IGroupInfo
{
    public GroupInfo() {}
    public GroupInfo(IGroupBase group, int participants) : base(group)
    {
        Participants = participants;
    }
    public int Participants { get; set; }
}

public class Group<TUserCollection, TParticipantCollection, TTransactionCollection> : GroupBase,
    IGroup<TUserCollection, TParticipantCollection, TTransactionCollection>
    where TUserCollection : ICollection, new()
    where TTransactionCollection : ICollection, new()
    where TParticipantCollection : ICollection, new()
{
    public Group() { }
    public Group(IGroupBase groupBase) : base(groupBase) {}
    public TParticipantCollection Participants { get; set; } = new();
    public TUserCollection Users { get; set; } = new();
    public TTransactionCollection Transactions { get; set; } = new();
}
using SquareUp.Shared.Models;

namespace SquareUp.Server.Models;

public class GroupData : Group<List<UserData>, List<Participant>, List<TransactionData>>
{
    public GroupData() { }
    public GroupData(IGroupBase group) : base(group) { }

    public static implicit operator Group(GroupData data) => new(data);
    public static implicit operator GroupInfo(GroupData data) => new(data, data.Users.Count);
}

public class Group : Group<List<UserBase>, List<Participant>, List<TransactionBase>>
{
    public Group(GroupData data) : base(data)
    {
        Users = data.Users.Select(u => u as UserBase).ToList();
        Transactions = data.Transactions.Select(e => new TransactionBase(e)).ToList();
        Participants = data.Participants;
    }
}
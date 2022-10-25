using SquareUp.Shared.Models;

namespace SquareUp.Server.Models;

public class TransactionData : Transaction<GroupData>
{
    public TransactionData() { }

    public TransactionData(ITransactionBase transaction, GroupData group) : base(transaction)
    {
        Id = transaction.Id;
        Name = transaction.Name;
        Amount = transaction.Amount;
        Type = transaction.Type;
        Category = transaction.Category;
        ParticipantId = transaction.ParticipantId;
        Group = group;
        Date = DateTime.Now;
    }

    public void Update(ITransactionBase transaction)
    {
        Name = transaction.Name;
        Amount = transaction.Amount;
        Type = transaction.Type;
        Category = transaction.Category;
        ParticipantId = transaction.ParticipantId;
    }

    //public static implicit operator TransactionBase(TransactionData data) => new(data);
}

// public class Transaction : Transaction<int>
// {
//     public Transaction() { }
//
//     public Transaction(TransactionData transaction) : base(transaction)
//     {
//         Participant = transaction.Participant;
//     }
//
//     public void Update(ITransactionBase transaction)
//     {
//         Name = transaction.Name;
//         Amount = transaction.Amount;
//         Type = transaction.Type;
//         Category = transaction.Category;
//     }
//
//     public void Update(Transaction transaction)
//     {
//         Name = transaction.Name;
//         Amount = transaction.Amount;
//         Type = transaction.Type;
//         Participant = transaction.Participant;
//         Date = transaction.Date;
//     }
// }
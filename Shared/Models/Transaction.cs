namespace SquareUp.Shared.Models;

public interface ITransactionBase : IId
{
    public string Name { get; set; }
    public decimal Amount { get; set; }
    public SplitType Type { get; set; }
    public DateTime Date { get; set; }
    public string Category { get; set; }
    public int ParticipantId { get; set; }
    public int SecondaryParticipantId { get; set; }
}

public interface ITransaction<TGroup> : ITransactionBase
{
    public TGroup Group { get; set; }
}

public class TransactionBase : ITransactionBase
{
    public TransactionBase() { }

    public TransactionBase(ITransactionBase transaction)
    {
        Id = transaction.Id;
        Name = transaction.Name;
        Amount = transaction.Amount;
        Type = transaction.Type;
        Date = transaction.Date;
        Category = transaction.Category;
        ParticipantId = transaction.ParticipantId;
        SecondaryParticipantId = transaction.SecondaryParticipantId;
    }
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public string Category { get; set; } = string.Empty;
    public SplitType Type { get; set; }
    public DateTime Date { get; set; } = DateTime.Now;
    public int ParticipantId { get; set; }
    public int SecondaryParticipantId { get; set; }
}


public class Transaction<TGroup> : TransactionBase, ITransaction<TGroup>
    where TGroup : GroupBase, new()
{
    public Transaction() { }
    public Transaction(ITransactionBase transaction) : base(transaction) { }
    public TGroup Group { get; set; } = new();
}

public enum SplitType
{
    EvenlySplit, IncomeProportional, Income, Payment
}
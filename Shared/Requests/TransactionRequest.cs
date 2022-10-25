using SquareUp.Shared.Models;

namespace SquareUp.Shared.Requests;

public class TransactionRequest
{
    public TransactionRequest()
    { }
    public TransactionRequest(ITransactionBase transaction, int groupId)
    {
        Transaction = new TransactionBase(transaction);
        GroupId = groupId;
    }
    public TransactionBase Transaction { get; set; }
    public int GroupId { get; set; }
}
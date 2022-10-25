using SquareUp.Shared.Types;
using SquareUp.Shared.Models;
using SquareUp.Shared.Requests;

namespace SquareUp.Server.Services.Transactions;

public interface ITransactionService
{
    Task<ServiceResponse<List<TransactionBase>>> GetAllTransactions();
    Task<ServiceResponse<TransactionBase>> Create(HttpRequest request, TransactionRequest payload);
    Task<ServiceResponse<TransactionBase>> Update(HttpRequest request, TransactionBase update);
    Task<ServiceResponse<int>> Delete(HttpRequest request, int id);
}
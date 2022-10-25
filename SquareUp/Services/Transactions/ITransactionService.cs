using SquareUp.Model;
using SquareUp.Shared.Types;

namespace SquareUp.Services.Transactions;

public interface ITransactionService
{
    Task<ServiceResponse<ObservableTransaction>> Create(ObservableTransaction transaction, int groupId);
    Task<ServiceResponse<ObservableTransaction>> Update(ObservableTransaction transaction);
    Task<ServiceResponse<int>> Delete(ObservableTransaction transaction);
}
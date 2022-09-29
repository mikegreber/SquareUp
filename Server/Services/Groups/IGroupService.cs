using SquareUp.Server.Models;
using SquareUp.Shared.Requests;
using SquareUp.Shared.Types;

namespace SquareUp.Server.Services.Groups;

public interface IGroupService
{
    Task<ServiceResponse<List<GroupClient>>> GetGroups();
    Task<ServiceResponse<GroupClient>> AddGroup(AddGroupRequest addGroup);
    Task<ServiceResponse<Expense>> AddExpense(AddExpenseRequest request);
    Task<ServiceResponse<User>> AddUser(AddUserRequest request);
    Task<ServiceResponse<GroupClient>> GetGroup(int id);
}
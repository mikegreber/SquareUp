using SquareUp.Model;
using SquareUp.Shared.Requests;
using SquareUp.Shared.Types;

namespace SquareUp.Services.Groups;

public interface IGroupService
{
    public Task<List<Group>> GetGroups(User user);
    public Task<ServiceResponse<GroupClient>> GetGroup(int id);
    public Task<ServiceResponse<GroupClient>> AddGroup(AddGroupRequest request);
    public Task<ServiceResponse<Expense>> AddExpense(AddExpenseRequest request);
    public Task<ServiceResponse<User>> AddUser(AddUserRequest request);
}
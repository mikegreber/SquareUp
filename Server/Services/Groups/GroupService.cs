using Microsoft.EntityFrameworkCore;
using SquareUp.Server.Data;
using SquareUp.Server.Models;
using SquareUp.Server.Services.Users;
using SquareUp.Shared.Requests;
using SquareUp.Shared.Types;

namespace SquareUp.Server.Services.Groups
{
    public class GroupService : IGroupService
    {
        private readonly DataContext _context;
        private readonly IUserService _userService;

        public GroupService(DataContext context, IUserService userService)
        {
            _context = context;
            _userService = userService;
        }

        public async Task<ServiceResponse<List<GroupClient>>> GetGroups()
        {
            var result = await _context
                .Groups
                .Include(g => g.Users)
                .Include(g => g.Expenses)
                .Select(g => new GroupClient(g))
                .ToListAsync();

            return new ServiceResponse<List<GroupClient>>(result, $"{result.Count} groups.");
        }

        public async Task<ServiceResponse<GroupClient>> AddGroup(AddGroupRequest request)
        {
            var user = await _context.Users.FindAsync(request.UserId);
            
            var group = new GroupData { Name = request.Name, Users = new List<UserData>{ user } };

            var result = await _context.Groups.AddAsync(group);

            user.Groups.Add(group);

            await _context.SaveChangesAsync();

            return new ServiceResponse<GroupClient>(result.Entity, $"{group.Name} created.");
        }

        public async Task<ServiceResponse<Expense>> AddExpense(AddExpenseRequest request)
        {
            var user = await _context.Users.FindAsync(request.UserId);
            if (user == null) 
                return new ServiceResponse<Expense>(message: "User does not exist.");
            
            var group = await _context.Groups.FindAsync(request.GroupId);
            if (group == null)
                return new ServiceResponse<Expense>(message: "Group does not exist.");

            var data = request.Expense;
            var expense = new ExpenseData
            {
                Name = data.Name, 
                Amount = data.Amount, 
                Type = data.Type, 
                User = user
            };

            group.Expenses.Add(expense);

            await _context.SaveChangesAsync();

            return new ServiceResponse<Expense>(expense, $"{expense.Name} expense for ${expense.Amount:0.00} added to {group.Name}.");
        }

        public async Task<ServiceResponse<User>> AddUser(AddUserRequest request)
        {
            var user = await _context
                .Users
                .FirstOrDefaultAsync(u => u.Email == request.UserEmail);

            if (user == null)
                return new ServiceResponse<User>(message: "User does not exist.");

            var group = await _context
                .Groups
                .Where(g => g.Id == request.GroupId)
                .Include(g => g.Users)
                .FirstOrDefaultAsync();

            if (group == null) 
                return new ServiceResponse<User>(message: "Group does not exist.");

            if (group.Users.Find(u => u.Id == user.Id) != null) 
                return new ServiceResponse<User>(message: $"{user.Name} is already in {group.Name}.");
            
            group.Users.Add(user);
            user.Groups.Add(group);

            await _context.SaveChangesAsync();

            return new ServiceResponse<User>(user, $"{user.Name} added to {group.Name}.");
        }

        public async Task<ServiceResponse<GroupClient>> GetGroup(int id)
        {
            var group = await _context.Groups
                .Where(g => g.Id == id)
                .Include(g => g.Users)
                .Include(g => g.Expenses)
                .FirstOrDefaultAsync();

            if (group == null) 
                return new ServiceResponse<GroupClient>(message: "Group not found.");

            return new ServiceResponse<GroupClient>(group, message: $"Found group {group.Name}.");
        }
    }
}

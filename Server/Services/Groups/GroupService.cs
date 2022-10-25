using Microsoft.EntityFrameworkCore;
using SquareUp.Server.Data;
using SquareUp.Server.Models;
using SquareUp.Server.Services.Users;
using SquareUp.Shared.Models;
using SquareUp.Shared.Requests;
using SquareUp.Shared.Types;

namespace SquareUp.Server.Services.Groups;

public class GroupService : IGroupService
{
    private readonly DataContext _context;
    
    public GroupService(DataContext context, IUserService userService)
    {
        _context = context;
    }

    public async Task<ServiceResponse<List<Group>>> GetGroups()
    {
        var result = await _context
            .Groups
            .Include(g => g.Users)
            .Include(g => g.Participants)
            .Include(g => g.Transactions)
            .Select(g => new Group(g))
            .ToListAsync();

        return new ServiceResponse<List<Group>>(result, $"{result.Count} groups.");
    }

    public async Task<ServiceResponse<List<GroupInfo>>> GetGroupsInfo()
    {
        var groups = await _context
            .Groups
            .Include(g => g.Users)
            .Include(g => g.Participants)
            .Include(g => g.Transactions)
            .Select(g => new Group(g))
            .ToListAsync();

        return new ServiceResponse<List<GroupInfo>>(groups.Select(g => new GroupInfo(g, g.Participants.Count)).ToList(), $"{groups.Count} groups.");
    }

    public async Task<ServiceResponse<Group>> GetGroup(HttpRequest request, int id)
    {
        var userId = request.GetUserId();
        if (userId == 0) return new ServiceResponse<Group>(message: "Request denied, user not authenticated.");
        
        var group = await _context.Groups
            .Where(g => g.Id == id)
            .Include(g => g.Participants)
            .Include(g => g.Users)
            .Include(g => g.Transactions)
            .FirstOrDefaultAsync();

        if (group == null)
        {
            return new ServiceResponse<Group>(message: "Group not found.");
        }

        if (group.Users.FirstOrDefault(u => u.Id == userId) == null)
        {
            return new ServiceResponse<Group>(message: "Request denied, user is not in this group.");
        }
            

        return new ServiceResponse<Group>(group, message: $"Found group {group.Name}.");
    }
    public async Task<ServiceResponse<GroupInfo>> UpdateGroup(HttpRequest request, GroupRequest payload)
    {
        var userId = request.GetUserId();
        if (userId == 0) return new ServiceResponse<GroupInfo>(message: "Request denied, user not authenticated.");

        var group = await _context.Groups
            .Where(g => g.Id == payload.GroupId)
            .Include(g => g.Users)
            .Include(g => g.Transactions)
            .FirstOrDefaultAsync();

        if (group == null) return new ServiceResponse<GroupInfo>(message: "Group not found.");

        if (group.Users.FirstOrDefault(u => u.Id == userId) == null)
        {
            return new ServiceResponse<GroupInfo>(message: "Request denied, user is not in this group.");
        }

        group.Name = payload.Name;
        group.Color = payload.Color;

        await _context.SaveChangesAsync();
        
        return new ServiceResponse<GroupInfo>(new GroupInfo(group, group.Users.Count), "Update successful");
    }

    public async Task<ServiceResponse<List<GroupBase>>> GetUserGroups(HttpRequest request)
    {
        var userId = request.GetUserId();

        if (userId == 0) return new ServiceResponse<List<GroupBase>>(message: "Request denied, user not authenticated.");

        var user = await _context
            .Users
            .Where(u => u.Id == userId)
            .Include(u => u.Groups)
            .FirstOrDefaultAsync();
        
        if (user == null) return new ServiceResponse<List<GroupBase>>(message: "User not found.");

        var result = user.Groups.Select(g => new GroupBase(g)).ToList();

        return new ServiceResponse<List<GroupBase>>(result, $"{result.Count} groups.");
    }

    public async Task<ServiceResponse<List<GroupInfo>>> GetUserGroupsInfo(HttpRequest request)
    {
        var userId = request.GetUserId();

        if (userId == 0) return new ServiceResponse<List<GroupInfo>>(message: "Request denied, user not authenticated.");
        
        var user = await _context
            .Users
            .Where(u => u.Id == userId)
            .Include(u => u.Groups).ThenInclude(g => g.Participants)
            .FirstOrDefaultAsync();

        if (user == null) return new ServiceResponse<List<GroupInfo>>(message: "User not found.");

        var result = user.Groups.Select(g => new GroupInfo(g, g.Participants.Count)).ToList();

        return new ServiceResponse<List<GroupInfo>>(result, $"{result.Count} groups.");
    }

    public async Task<ServiceResponse<GroupInfo>> CreateGroup(HttpRequest request, GroupBase payload)
    {
        var userId = request.GetUserId();

        if (userId == 0) return new ServiceResponse<GroupInfo>(message: "Request denied, user not authenticated.");
        
        var user = await _context.Users.FindAsync(userId);

        if (user == null) return new ServiceResponse<GroupInfo>(message: "User not found.");

        var group = new GroupData(payload)
        {
            Users = new List<UserData>{ user }, 
            Participants = new List<Participant>{ new() { Name = user.Name, UserId = user.Id } }, 
            Date = DateTime.Now, 
            LastEdit = DateTime.Now
        };

        await _context.Groups.AddAsync(group);

        user.Groups.Add(group);

        await _context.SaveChangesAsync();

        return new ServiceResponse<GroupInfo>(group, $"{group.Name} created.");
    }

    public async Task<ServiceResponse<Participant>> AddParticipant(HttpRequest request, AddParticipantRequest payload)
    {
        var userId = request.GetUserId();
        if (userId == 0) return new ServiceResponse<Participant>(message: "Request denied, user not authenticated.");

        var group = await _context.Groups
            .Where(g => g.Id == payload.GroupId)
            .Include(g => g.Users)
            .FirstOrDefaultAsync();

        if (group == null) return new ServiceResponse<Participant>(message: "Group does not exist.");

        if (group.Users.Find(u => u.Id == userId) == null)
        {
            return new ServiceResponse<Participant>(message: "Request denied, user is not in this group.");
        }

        var participant = new Participant() { Name = payload.Name };
        group.Participants.Add(participant);

        await _context.SaveChangesAsync();

        return new ServiceResponse<Participant>(participant, message: $"Added participant {participant.Name} to {group.Name}.");
    }

    public async Task<ServiceResponse<Participant>> InviteParticipant(HttpRequest request, InviteParticipantRequest payload)
    {
        var userId = request.GetUserId();
        if (userId == 0) return new ServiceResponse<Participant>(message: "Request denied, user not authenticated.");

        var user = await _context.Users.Where(u => u.Email == payload.UserEmail).FirstOrDefaultAsync();
        if (user == null) return new ServiceResponse<Participant>(message: $"User with email \"{payload.UserEmail}\" not found.");

        var group = await _context.Groups
            .Where(g => g.Id == payload.GroupId)
            .Include(g => g.Users)
            .Include(g => g.Participants)
            .FirstOrDefaultAsync();
        if (group == null) return new ServiceResponse<Participant>(message: "Group does not exist.");

        if (group.Users.Find(u => u.Id == userId) == null)
            return new ServiceResponse<Participant>(message: "Request denied, requesting user is not in this group.");

        if (group.Users.Contains(user)) return new ServiceResponse<Participant>(message: $"User is already in {group.Name}");

        var participant = group.Participants.Find(p => p.Id == payload.ParticipantId);
        if (participant == null) return new ServiceResponse<Participant>(message: "Participant does not exist.");
        if (participant.UserId != 0) return new ServiceResponse<Participant>(message: "Participant is already assigned to user.");

        participant.UserId = user.Id;
        participant.Name = user.Name;

        user.Groups.Add(group);
        group.Users.Add(user);

        await _context.SaveChangesAsync();

        return new ServiceResponse<Participant>(participant, $"Participant {participant.Name} assigned to user {user.Email}.");
    }

    public async Task<ServiceResponse<User>> AddUser(HttpRequest request, AddUserRequest payload)
    {
        var userId = request.GetUserId();

        if (userId == 0) return new ServiceResponse<User>(message: "Request denied, user not authenticated.");
        
        var user = await _context
            .Users
            .FirstOrDefaultAsync(u => u.Email == payload.UserEmail);

        if (user == null) return new ServiceResponse<User>(message: "User does not exist.");

        var group = await _context
            .Groups
            .Where(g => g.Id == payload.GroupId)
            .Include(g => g.Users)
            .FirstOrDefaultAsync();

        if (group == null) return new ServiceResponse<User>(message: "Group does not exist.");

        if (group.Users.Find(u => u.Id == userId) == null)
        {
            return new ServiceResponse<User>(message: "Request denied, user is not in this group.");
        }

        if (group.Users.Contains(user)) return new ServiceResponse<User>(message: $"{user.Name} is already in {group.Name}.");

        group.Users.Add(user);
        user.Groups.Add(group);

        group.LastEdit = DateTime.Now;

        await _context.SaveChangesAsync();

        return new ServiceResponse<User>(user, $"{user.Name} added to {group.Name}.");
    }

    public async Task<ServiceResponse<int>> DeleteGroup(HttpRequest request, int id)
    {
        var userId = request.GetUserId();

        if (userId == 0) return new ServiceResponse<int>(message: "Request denied, user not authenticated.");

        var group = await _context.Groups
            .Where(g => g.Id == id)
            .Include(g => g.Users)
            .Include(g => g.Participants)
            .Include(g => g.Transactions)
            .FirstOrDefaultAsync();

        if (group == null) return new ServiceResponse<int>(message: "Group not found.");

        if (group.Users.Find(u => u.Id == userId) == null)
        {
            return new ServiceResponse<int>(message: "Request denied, user is not in this group.");
        }

        _context.Participants.RemoveRange(group.Participants);
        _context.Transactions.RemoveRange(group.Transactions);
        _context.Groups.Remove(group);

        await _context.SaveChangesAsync();

        return new ServiceResponse<int>(id, "Group deleted.");
    }
}
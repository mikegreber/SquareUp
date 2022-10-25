using Microsoft.EntityFrameworkCore;
using SquareUp.Server.Data;
using SquareUp.Server.Models;
using SquareUp.Shared.Models;
using SquareUp.Shared.Requests;
using SquareUp.Shared.Types;

namespace SquareUp.Server.Services.Transactions;

public class TransactionService : ITransactionService
{
    private readonly DataContext _context;

    public TransactionService(DataContext context)
    {
        _context = context;
    }
    public async Task<ServiceResponse<List<TransactionBase>>> GetAllTransactions()
    {
        var result = await _context
            .Transactions
            .Include(e => e.Group)
            .Select(e => new TransactionBase(e))
            .ToListAsync();

        return new ServiceResponse<List<TransactionBase>>(result);
    }

    public async Task<ServiceResponse<TransactionBase>> Create(HttpRequest request, TransactionRequest payload)
    {
        // get authorized user id
        var userId = request.GetUserId();
        if (userId == 0) return new ServiceResponse<TransactionBase>(message: "Request denied, user not authenticated.");

        var group = await _context.Groups
            .Where(g => g.Id == payload.GroupId)
            .Include(g => g.Users)
            .Include(g => g.Participants)
            .FirstOrDefaultAsync();
        if (group == null) return new ServiceResponse<TransactionBase>(message: "Group does not exist.");

        // make sure authorized user is in this group
        if (group.Users.Find(u => u.Id == userId) == null)
        {
            return new ServiceResponse<TransactionBase>(message: "Request denied, requesting user is not in group.");
        }

        var participant = group.Participants.Find(p => p.Id == payload.Transaction.ParticipantId);
        if (participant == null)
        {
            return new ServiceResponse<TransactionBase>(message: "Participant does not exist in group.");
        }

        var transaction = new TransactionData(payload.Transaction, group);

        group.Transactions.Add(transaction);

        group.LastEdit = DateTime.Now;

        await _context.SaveChangesAsync();

        return new ServiceResponse<TransactionBase>(transaction, $"{transaction.Name} Transaction for ${transaction.Amount:0.00} added to {group.Name}.");
    }

    public async Task<ServiceResponse<TransactionBase>> Update(HttpRequest request, TransactionBase update)
    {
        var userId = request.GetUserId();
        if (userId == 0) return new ServiceResponse<TransactionBase>(message: "Request denied, user not authenticated.");

        var transaction = await _context
            .Transactions
            .Where(transaction => transaction.Id == update.Id)
            .Include(transaction => transaction.Group).ThenInclude(group => group.Users)
            .Include(transaction => transaction.Group).ThenInclude(group => group.Participants)
            .FirstOrDefaultAsync();

        if (transaction == null) return new ServiceResponse<TransactionBase>(message: "Transaction id not found.");

        if (transaction.Group.Users.Find(u => u.Id == userId) == null)
        {
            return new ServiceResponse<TransactionBase>(message: "Request denied, user is not in group.");
        }

        if (transaction.ParticipantId != update.ParticipantId)
        {
            var participant = transaction.Group.Participants.Find(p => p.Id == update.ParticipantId);
            if (participant == null) return new ServiceResponse<TransactionBase>(message: "Participant not in group.");

            transaction.ParticipantId = participant.Id;
        }

        transaction.Update(update);

        transaction.Group.LastEdit = DateTime.Now;

        await _context.SaveChangesAsync();

        return new ServiceResponse<TransactionBase>(data: transaction, message: $"Updated {transaction.Name}.");
    }

    public async Task<ServiceResponse<int>> Delete(HttpRequest request, int id)
    {
        var userId = request.GetUserId();
        if (userId == 0) return new ServiceResponse<int>(message: "Request denied, user not authenticated.");

        var transaction = await _context
            .Transactions
            .Where(e => e.Id == id)
            .Include(t => t.Group).ThenInclude(g => g.Users)
            .FirstOrDefaultAsync();

        if (transaction == null) return new ServiceResponse<int>(message: "Transaction not found.");

        if (transaction.Group.Users.Find(u => u.Id == userId) == null)
        {
            return new ServiceResponse<int>(message: "Request denied, user is not in group.");
        }

        transaction.Group.LastEdit = DateTime.Now;
        await _context.SaveChangesAsync();

        _context.Transactions.Remove(transaction);
        await _context.SaveChangesAsync();

        return new ServiceResponse<int>(data: transaction.Id, message: $"{transaction.Name} transaction deleted.");
    }
}
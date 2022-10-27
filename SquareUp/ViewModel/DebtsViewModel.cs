using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SquareUp.Model;
using SquareUp.Services.Session;
using SquareUp.Shared.Models;
using SquareUp.Shared.Types;
using SquareUp.View;
using IQueryAttributable = Microsoft.Maui.Controls.IQueryAttributable;
using static SquareUp.Resources.Statics.TransactionCategories;

namespace SquareUp.ViewModel;

public partial class DebtsViewModel : BaseViewModel//, IQueryAttributable
{
    
    public DebtsViewModel(ISessionData session) : base(session)
    {
        
    }

    // public async Task Load()
    // {
    //     //await Session.GetGroup()
    //
    //     await Task.Delay(3000);
    //     Group = Session.Group;
    //     CalculateDebts();
    //     CalculateSettlements();
    // }
    //
    // public void ApplyQueryAttributes(IDictionary<string, object> query)
    // {
    //     Group = (ObservableGroup)query[nameof(Group)];
    //     
    //     CalculateDebts();
    //     CalculateSettlements();
    // }
    // public static Dictionary<string, object> Params(ObservableGroup group) => new()
    // {
    //     [nameof(Group)] = group
    // };

    // private void CalculateDebts()
    // {
    //     Dictionary<SplitType, decimal> total = new()
    //     {
    //         [SplitType.EvenlySplit] = 0,
    //         [SplitType.IncomeProportional] = 0,
    //         [SplitType.Income] = 0,
    //         [SplitType.Payment] = 0,
    //     };
    //
    //     foreach (var transaction in Group.Transactions.SelectMany(x => x))
    //         total[transaction.Type] += transaction.Amount;
    //
    //     var debts = Group.Participants.Select(participant => new Debt(participant, Group, total)).ToList();
    //     debts.Sort((a,b) => a.Amount.CompareTo(b.Amount));
    //
    //     Debts = new FullyObservableCollection<Debt>(debts);
    // }
    //
    // private void CalculateSettlements()
    // {
    //     var owing = Debts
    //         .Where(d => d.Amount > 0)
    //         .Select(d => new DebtBase { Participant = d.Participant, Amount = d.Amount })
    //         .ToList();
    //     owing.Sort((a, b) => b.Amount.CompareTo(a.Amount));
    //
    //     var owed = Debts
    //         .Where(d => d.Amount < 0)
    //         .Select(d => new DebtBase{ Participant = d.Participant, Amount = -d.Amount })
    //         .ToList();
    //     owed.Sort((a,b) => b.Amount.CompareTo(a.Amount));
    //
    //     var settlements = new List<Settlement>();
    //     while (owing.Count > 0 && owed.Count > 0)
    //     {
    //         var settlement = new Settlement { From = owing.First().Participant, To = owed.First().Participant };
    //
    //         if (owed.First().Amount > owing.First().Amount)
    //         {
    //             settlement.Amount = owing.First().Amount;
    //             owed.First().Amount -= owing.First().Amount;
    //             owing.Remove(owing.First());
    //         } 
    //         else if (owed.First().Amount < owing.First().Amount)
    //         {
    //             settlement.Amount = owed.First().Amount;
    //             owing.First().Amount -= owed.First().Amount;
    //             owed.Remove(owed.First());
    //         }
    //         else
    //         {
    //             settlement.Amount = owing.First().Amount;
    //             owed.Remove(owed.First());
    //             owing.Remove(owing.First());
    //         }
    //
    //         if (settlement.Amount > 0.1m)
    //         {
    //             settlements.Add(settlement);
    //         }
    //     }
    //
    //     Settlements = new FullyObservableCollection<Settlement>(settlements);
    // }

    [RelayCommand]
    private async void Settle(Settlement settlement)
    {
        await TransactionPage.OpenAsync(new ObservableTransaction
        {
            Amount = settlement.Amount,
            Category = Transfer,
            Participants = Session.Group.Participants,
            Participant = settlement.From,
            ParticipantId = settlement.From.Id,
            SecondaryParticipant = settlement.To,
            SecondaryParticipantId = settlement.To.Id,
            Name = $"Transfer to {settlement.To.Name}"
        }, PageMode.Create, "New Transaction");
    }
}
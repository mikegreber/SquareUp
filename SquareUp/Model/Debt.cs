using CommunityToolkit.Mvvm.ComponentModel;
using SquareUp.Shared.Models;

namespace SquareUp.Model;

public partial class DebtBase : ObservableObject
{
    [ObservableProperty]
    private decimal _amount;

    [ObservableProperty]
    private ObservableParticipant _participant = new();
}


public partial class Debt : DebtBase
{
    [ObservableProperty]
    private decimal _proportion;

    [ObservableProperty]
    private Dictionary<SplitType, decimal> _amounts = new()
    {
        [SplitType.EvenlySplit] = 0,
        [SplitType.IncomeProportional] = 0,
        [SplitType.Income] = 0,
        [SplitType.Payment] = 0
    };

    public Debt(ObservableParticipant participant, ObservableGroup group, IReadOnlyDictionary<SplitType, decimal> total)
    {
        Participant = participant;
        Amount = 0;
        foreach (var transaction in group.Transactions.SelectMany(x => x.Where(t => t.ParticipantId == participant.Id || t.SecondaryParticipantId == participant.Id)))
        {
            if (transaction.Type == SplitType.Payment)
            {
                if (transaction.ParticipantId == Participant.Id)
                {
                    Amount -= transaction.Amount;
                }
                else 
                {
                    Amount += transaction.Amount;
                }
            }
            else
            {
                Amounts[transaction.Type] += transaction.Amount;
            }
        }

        Proportion = 1m / group.Participants.Count;
        if (total[SplitType.Income] > 0)
            Proportion = Amounts[SplitType.Income] / total[SplitType.Income];

        Amount += total[SplitType.IncomeProportional] * Proportion - Amounts[SplitType.IncomeProportional];

        Amount += total[SplitType.EvenlySplit] / group.Participants.Count - Amounts[SplitType.EvenlySplit];
    }
}

using CommunityToolkit.Mvvm.ComponentModel;

namespace SquareUp.Model;

public partial class Settlement : ObservableObject
{
    [ObservableProperty]
    private ObservableParticipant _from;

    [ObservableProperty]
    private ObservableParticipant _to;

    [ObservableProperty]
    private decimal _amount;
}
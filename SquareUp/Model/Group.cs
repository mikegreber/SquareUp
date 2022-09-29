using CommunityToolkit.Mvvm.ComponentModel;
using SquareUp.Shared.Models;
using SquareUp.Shared.Types;

namespace SquareUp.Model;

[ObservableObject]
public partial class Group : IGroup
{
    [ObservableProperty]
    private string _name = string.Empty;
}

public partial class GroupClient : Group, IGroupClient<FullyObservableCollection<User>, User, FullyObservableCollection<Expense>, Expense>
{
    public int Id { get; set; }

    [ObservableProperty]
    private FullyObservableCollection<User> _users = new();

    [ObservableProperty]
    private FullyObservableCollection<Expense> _expenses = new();
}

using SquareUp.Model;
using CommunityToolkit.Mvvm.ComponentModel;
using IQueryAttributable = SquareUp.Model.IQueryAttributable;

namespace SquareUp.ViewModel;

[INotifyPropertyChanged]
public abstract partial class BaseViewModel
{
    protected BaseViewModel() { }

    public IQueryAttributable.OnAttributesSetDelegate OnAttributesSet { get; set; }
}
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SquareUp.Services.Session;

namespace SquareUp.ViewModel;

[INotifyPropertyChanged]
public abstract partial class BaseViewModel
{
    public ISessionData Session { get; }

    protected bool AnimateBackTransitions = true;

    protected BaseViewModel(ISessionData session)
    {
        Session = session;
    }

    public virtual async Task OnBackButtonClicked()
    {
        await Shell.Current.GoToAsync("..", AnimateBackTransitions);
    }

    public virtual Func<Task> OnActionButtonClicked { get; set; } = null;

    [RelayCommand]
    private async Task Back()
    {
        await OnBackButtonClicked();
    }

    [RelayCommand]
    protected async Task ActionButton()
    {
        if (OnActionButtonClicked != null) await OnActionButtonClicked();
    }

    

    protected Page Page => Application.Current!.MainPage!;
}

public enum PageMode { Create, Edit }
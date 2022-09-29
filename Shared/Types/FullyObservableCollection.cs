
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;


namespace SquareUp.Shared.Types;

public sealed class FullyObservableCollection<T> : ObservableCollection<T> where T : INotifyPropertyChanged
{

    public FullyObservableCollection() => Initialize();

    public FullyObservableCollection(IEnumerable<T> input) : base(input) => Initialize();

    private void Initialize()
    {
        // CollectionChanged += FullCollectionChanged;
        // FullCollectionChanged(this, new NotifyCollectionChangedEventArgs(action: NotifyCollectionChangedAction.Replace, Items, Enumerable.Empty<T>(), 0));
    }
    private void FullCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
    {
        if (e.NewItems != null)
            foreach (T item in e.NewItems)
                item.PropertyChanged += ElementPropertyChanged;

        if (e.OldItems != null)
            foreach (T item in e.OldItems)
                item.PropertyChanged -= ElementPropertyChanged;
    }

    private void ElementPropertyChanged(object? item, PropertyChangedEventArgs e) => NotifyChange((T?)item);

    public void ElementPropertyChanged(T? item)
    {
        NotifyChange(item);
    }

    public void NotifyChange(T? item)
    {
        if (item != null)
        {
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Replace, item, item, IndexOf(item)));
        }
    }

    // public static implicit operator FullyObservableCollection<T>(IEnumerable<T> enumerable) => new(enumerable);
}

public static class Extensions
{
    public static FullyObservableCollection<T> ToFullyObservableCollection<T>(this IEnumerable<T> enumerable) where T : INotifyPropertyChanged => new(enumerable);
}
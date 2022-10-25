
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;


namespace SquareUp.Shared.Types;

public class FullyObservableCollection<T> : ObservableCollection<T> where T : INotifyPropertyChanged
{

    public FullyObservableCollection() => Initialize();

    public FullyObservableCollection(T item) : this(new List<T>{ item }) { }
    public FullyObservableCollection(IEnumerable<T> input) : this()
    {
        foreach(var item in input) Add(item);
    }
    private void Initialize() => CollectionChanged += FullCollectionChanged;

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
}

public static class Extensions
{
    public static FullyObservableCollection<T> ToFullyObservableCollection<T>(this IEnumerable<T> enumerable) where T : INotifyPropertyChanged => new(enumerable);
    // public static ObservableCollectionGroup<>
}
using System.Collections.ObjectModel;
using System.ComponentModel;
using SquareUp.Shared.Models;

namespace SquareUp.Shared.Types;

public class GroupedFullyObservableCollection<TKey, TItem> :
    ObservableCollection<ObservableCollectionGroup<TKey, TItem>>
    where TItem : INotifyPropertyChanged, IId, IUpdateable<TItem>
{
    public GroupedFullyObservableCollection(Func<TKey, TItem, int> compareGroups, Func<TItem, TItem, int> compareItems,
        Func<TItem, TKey> getGroupKey)
    {
        CompareGroups = compareGroups;
        CompareItems = compareItems;
        GetGroupKey = getGroupKey;
    }

    public GroupedFullyObservableCollection(IEnumerable<TItem> input, Func<TKey, TItem, int> compareGroups, Func<TItem, TItem, int> compareItems,
        Func<TItem, TKey> getGroupKey) : this(compareGroups, compareItems, getGroupKey)
    {
        foreach(var item in input) Add(item);
    }

    public Func<TKey, TItem, int> CompareGroups { get; set; }
    public Func<TItem, TItem, int> CompareItems { get; set; }
    public Func<TItem, TKey> GetGroupKey { get; set; }

    public void Add(TItem item)
    {
        for (var i = 0; i < Count; i++)
        {
            switch (CompareGroups(this[i].Key, item))
            {
                case < 0:
                    continue;
                case 0:
                    this[i].InsertSorted(item);
                    return;
                default:
                    Insert(i, new ObservableCollectionGroup<TKey, TItem>(item, GetGroupKey, CompareItems));
                    return;
            }
        }

        Add(new ObservableCollectionGroup<TKey, TItem>(item, GetGroupKey, CompareItems));
    }

    public bool Update(TItem item)
    {
        foreach (var group in Items)
            if (CompareGroups(group.Key, item) == 0)
                return group.Update(item);

        return false;
    }

    public bool Delete(TItem item)
    {
        foreach (var group in Items)
            if (CompareGroups(group.Key, item) == 0)
            {
                if (group.Count != 0) 
                    return group.Delete(item);
                
                Remove(group);
                return true;
            }

        return false;
    }
}

public class ObservableCollectionGroup<TKey, TItem> : FullyObservableCollection<TItem>
    where TItem : INotifyPropertyChanged, IId, IUpdateable<TItem>
{
    public ObservableCollectionGroup(TKey key, Func<TItem, TKey> getKey, Func<TItem, TItem, int> compare)
    {
        Key = key;
        GetKey = getKey;
        Compare = compare;
    }

    public ObservableCollectionGroup(TItem item, Func<TItem, TKey> getKey, Func<TItem, TItem, int> compare) : this(
        new List<TItem> { item }, getKey, compare)
    {
    }

    public ObservableCollectionGroup(IEnumerable<TItem> input, Func<TItem, TKey> getKey, Func<TItem, TItem, int> compare)
        : base(input)
    {
        GetKey = getKey;
        Key = GetKey(input.First());
        Compare = compare;
    }

    public TKey Key { get; set; }
    public Func<TItem, TItem, int> Compare { get; set; }
    public Func<TItem, TKey> GetKey { get; set; }


    public new void Add(TItem item)
    {
        for (var i = 0; i < Count; ++i)
        {
            if (Compare(this[i], item) <= 0) continue;

            Insert(i, item);
            return;
        }

        Insert(Items.Count, item);
    }
    public void InsertSorted(TItem item)
    {
        for (var i = 0; i < Count; ++i)
        {
            if (Compare(this[i], item) <= 0) continue;

            Insert(i, item);
            return;
        }

        Insert(Items.Count, item);
    }

    public bool Update(TItem update)
    {
        foreach (var item in Items)
        {
            if (item.Id != update.Id) continue;

            item.Update(update);
            return true;
        }

        return false;
    }

    public bool Delete(TItem delete)
    {
        return Remove(delete);
    }
}
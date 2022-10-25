using System.Collections.ObjectModel;

namespace SquareUp.Resources.Statics;

public static class TransactionCategories
{
    public const string Entertainment = "🎮*Entertainment";
    public const string Food = "🍕*Food";
    public const string Groceries = "🛒*Groceries";
    public const string Travel = "🚕*Travel";
    public const string HomeExpenses = "🏡*Home Expense";
    public const string SharedPayment = "💳*Payment";
    public const string Income = "💰*Income";
    public const string Transfer = "💸*Money Transfer";

    public static IReadOnlyCollection<string> All = new ReadOnlyCollection<string>(new List<string>{ Entertainment, Food, Groceries, Travel, HomeExpenses, SharedPayment, Income, Transfer });
}
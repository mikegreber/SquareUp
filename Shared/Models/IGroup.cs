using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SquareUp.Shared.Models;

public interface IGroup
{
    public string Name { get; set; }
}

public interface IGroupClient<TUserCollection, TUser, TExpenseCollection, TExpense> : IGroup 
    where TUserCollection : ICollection<TUser> 
    where TExpenseCollection : ICollection<TExpense>
{
    public int Id { get; set; }
    public TUserCollection Users { get; set; }// = new();
    public TExpenseCollection Expenses { get; set; }// = new();
}
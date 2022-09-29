using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using SquareUp.View;
using SquareUp.ViewModel;

namespace SquareUp.Model;

public interface IQueryAttributable : Microsoft.Maui.Controls.IQueryAttributable
{
    public void ReadQueryAttributes(IDictionary<string, object> query);

    void Microsoft.Maui.Controls.IQueryAttributable.ApplyQueryAttributes(IDictionary<string, object> query)
    {
        ReadQueryAttributes(query);
        OnAttributesSet?.Invoke();
    }

    public delegate void OnAttributesSetDelegate();
    public OnAttributesSetDelegate OnAttributesSet { get; set; }
}
using System;
using System.Collections.Generic;
using Company.Project.UI;
using Company.Project.Features.Items;

namespace Company.Project.Features.Inventory
{
    public interface IInventoryView : IView
    {
        event EventHandler<IItem> Selected;
        event EventHandler<IItem> Deselected;
        void Display(List<IItem> itemInfoCollection);
    }
}
using System;
using System.Collections.Generic;
using Company.Project.Features.Items;
using Company.Project.UI;

namespace Company.Project.Features.Inventory
{
    public interface IInventoryController
    {
        IReadOnlyList<IItem> GetEquippedItems();
        void ShowInventory(Action hideAction);
        void HideInventory();
    }
}
using System.Collections.Generic;
using Company.Project.Features.Items;

namespace Company.Project.Features.Inventory
{
    public interface IInventoryModel
    {
        IReadOnlyList<IItem> GetEquippedItems();
        void EquipItem(IItem item);
        void UnequipItem(IItem item);
    }
}
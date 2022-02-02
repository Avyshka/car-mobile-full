using System;
using System.Collections.Generic;
using Company.Project.UI;
using Company.Project.Features.Items;

namespace Company.Project.Features.Abilities
{
    public interface IAbilityCollectionView : IView
    {
        event EventHandler<IItem> UseRequested;
        void Display(IReadOnlyList<IItem> abilityItems);
    }
}
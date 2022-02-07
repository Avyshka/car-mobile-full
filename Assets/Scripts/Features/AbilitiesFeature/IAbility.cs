using System;
using System.Collections.Generic;
using Company.Project.Features.Items;
using Company.Project.UI;
using UnityEngine;

namespace Company.Project.Features.Abilities
{
    public interface IAbility
    {
        void Apply(IAbilityActivator activator);
    }
}
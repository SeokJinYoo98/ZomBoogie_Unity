using System.Collections.Generic;
using UnityEngine;
namespace AbilitySystem.Core.Helper
{
    public class AbilityHasher : IEqualityComparer<IAbility>
    {
        public bool Equals(IAbility a, IAbility b)
        {
            return a != null && b != null && a.Data.Id == b.Data.Id;
        }
        public int GetHashCode(IAbility ability)
        {
            return ability.Data.Id.GetHashCode( );
        }
    }
}



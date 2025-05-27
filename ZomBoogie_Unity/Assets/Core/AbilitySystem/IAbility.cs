using AbilitySystem.Data;
using UnityEngine;
namespace AbilitySystem.Core
{
    public interface IAbility
    {
        AbilityData Data { get; }
        int Level { get; }

        void LevelUp(int increased);
        void SetLevel(int level);
        void Activate(Boogie boogie);
    }
}



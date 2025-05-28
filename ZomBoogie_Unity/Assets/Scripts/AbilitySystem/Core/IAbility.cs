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
    //public interface IActiveAbility : IAbility
    //{
    //    new ActiveAbilityData Data { get; }
    //    void Cast(); 
    //}

    //public interface IPassiveAbility : IAbility
    //{
    //    new PassiveAbilityData Data { get; }
    //    void Apply();
    //}
}



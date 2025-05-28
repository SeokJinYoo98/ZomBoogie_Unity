using AbilitySystem.Data;
using AbilitySystem.Module.Base;
using System.Diagnostics;

namespace AbilitySystem.Module.Passive
{
    public class SpeedUp : PassiveAbilityBase
    {
        public SpeedUp(PassiveAbilityData data) : base( data )
        {

        }

        protected override void Apply(Boogie boogie)
        {
            float amount = _valuePerLevel * _level;
            
        }
    }
}

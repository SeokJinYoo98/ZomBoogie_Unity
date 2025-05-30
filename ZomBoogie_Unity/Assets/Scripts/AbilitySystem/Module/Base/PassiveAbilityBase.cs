using AbilitySystem.Core;
using AbilitySystem.Data;
namespace AbilitySystem.Module.Base
{
    public abstract class PassiveAbilityBase : IAbility
    {
        protected readonly PassiveAbilityData   _data;
        protected int                           _level;

        protected float _baseValue;
        protected float _valuePerLevel;
        public PassiveAbilityBase(PassiveAbilityData data)
        {
            _data = data;
            _baseValue      = _data.BaseValue;
            _valuePerLevel  = _data.ValuePerLevel;
            _level = 0;
        }

        public AbilityData Data => _data;
        public int Level => _level;

        public void LevelUp(int increased) =>   _level += increased;
        public void SetLevel(int level) =>      _level = level;

        public virtual void Activate(Boogie boogie)
        {
            Apply( boogie );
        }

        protected abstract void Apply(Boogie boogie);
    }
}



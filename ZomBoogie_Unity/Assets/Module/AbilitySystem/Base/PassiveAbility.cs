using AbilitySystem.Core;
using AbilitySystem.Data;
namespace AbilitySystem.Module.Base
{
    public abstract class PassiveAbility : IAbility
    {
        protected readonly PassiveAbilityData   _data;
        protected int                           _level;

        public PassiveAbility(PassiveAbilityData data)
        {
            _data = data;
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



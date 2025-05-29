using Common.Interface;
using System;
using System.Collections.Generic;
using WeaponSystem.Data;

namespace WeaponSystem.Core
{
    public interface IWeapon
    {
        string  Id { get; }
        float   Damage { get; }
        float   Range { get; }

        void Fire();
        bool CanFire();
    }
}



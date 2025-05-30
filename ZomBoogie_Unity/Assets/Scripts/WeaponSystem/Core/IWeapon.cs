using System;
using UnityEngine;
using StatSystem.Runtime;
using Common.CharInterface;
namespace WeaponSystem.Core
{
    public interface IWeapon
    {
        string Id { get; }
        string Name { get; }
        Sprite Icon { get; }

        void InitBoogieStats(StatsComponent targetStats);
        void Attack();
        void LookAt(Vector2 pos);
        void FlipX(bool flipX);
    }
}



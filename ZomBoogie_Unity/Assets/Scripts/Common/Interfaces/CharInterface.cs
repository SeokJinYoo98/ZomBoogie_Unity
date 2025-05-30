using System;
using UnityEngine;

namespace Common.CharInterface
{
    internal class ICommand
    {
    }

    public interface IMoveAble
    {
        void Move(Vector2 dir);
    }
    public interface IAttackAble
    {
        void Attack(Vector2 dir);
    }
    public interface IFlipAble
    {
        void FlipX(float targetX);
    }
        public interface IControllAble
    {
        bool IsDeath();
        void Move(Vector2 dir);
        void Attack();
        void LookAt(Vector2 pos);
    }


}

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
        void FlipX(bool flip);
        void FlipY(bool flip);
    }
}

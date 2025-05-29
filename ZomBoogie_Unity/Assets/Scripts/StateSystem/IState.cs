using UnityEngine;

namespace StateSystem.Core
{
    public interface IControllAble
    {
        void PlayAnimation(string name);
        bool IsDeath();
        void Move(Vector2 dir);
        void Attack(Vector2 dir);
        void LookAt(Vector2 pos);
    }
    public enum StateType
    {
        Idle,
        Walk,
        Hit,
        Death
    }
    public interface IState
    {
        StateType StateType { get; }
        void Enter(IControllAble boogie);
        void Update(IControllAble boogie);
        void Exit(IControllAble boogie);
    }

}


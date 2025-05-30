using UnityEngine;
using Common.CharInterface;
using StatSystem.Runtime;
using WeaponSystem.Core;
using WeaponSystem.Module;

[RequireComponent(typeof(StatsComponent))]
[RequireComponent(typeof(MovementComponent))]
[RequireComponent(typeof(AnimComponent))]
[RequireComponent(typeof(ModelComponent))]
public class NewBoogie : MonoBehaviour, IMoveAble, IControllAble
{
    private StatsComponent _stats;
    private MovementComponent _movement;
    private AnimComponent _anim;
    private ModelComponent _model;
    private IWeapon         _weapon;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        _stats = GetComponent<StatsComponent>();
        _model = GetComponent<ModelComponent>();
        _movement = GetComponent<MovementComponent>();
        _anim = GetComponent<AnimComponent>();

        // 부기가 Mediator가 되어서 서브 컴포넌트로 메서드를 뿌려보자.
        _model.SubscribeFlipEvent(FlipX);
    }
    void Start()
    {
        _weapon = GetComponentInChildren<WeaponBase>() as IWeapon;
        _weapon.InitBoogieStats(_stats);
    }

    // Update is called once per frame
    void Update()
    {
    }


    public bool IsDeath() => _stats.IsDeath();
    public void Move(Vector2 dir)
    {
        _anim.EnterAnimation(dir.sqrMagnitude <= 0.01f ? "Idle" : "Walk");
        _movement.SetMovement(dir);
    }
    public void LookAt(Vector2 pos)
    {
        _model.FlipX(pos.x);
        _weapon.LookAt(pos);
    }
    public void Attack()
    {
        _weapon.Attack();
    }

    private void FlipX(bool flip)
    {
        _anim.SetReverse(flip);
        _weapon.FlipX(flip);
    }
}

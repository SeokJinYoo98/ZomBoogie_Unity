using Common.CharInterface;
using StateSystem.Core;
using StatSystem.Runtime;
using UnityEngine;
[RequireComponent(typeof(StatsComponent))]
[RequireComponent(typeof(MovementComponent))]
[RequireComponent(typeof(AnimComponent))]
[RequireComponent(typeof(ModelComponent))]
public class NewBoogie : MonoBehaviour, IFlipAble, IMoveAble, IControllAble
{
    private StatsComponent       _stats;
    private MovementComponent    _movement;
    private AnimComponent        _anim;
    private ModelComponent       _model;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        _stats       = GetComponent<StatsComponent>();
        _model       = GetComponent<ModelComponent>();
        _movement    = GetComponent<MovementComponent>();
        _anim        = GetComponent<AnimComponent>();
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PlayAnimation(string name)
    {
        _anim.EnterAnimation(name);
    }
    public void FlipX(bool flip) => _model.FlipX(flip);
    public void FlipY(bool flip) => _model.FlipY(flip);
    public bool IsDeath() => _stats.IsDeath();
    public void Move(Vector2 dir)
    {
        _anim.SetReverse(dir.x);
        _anim.EnterAnimation(dir.sqrMagnitude <= 0.01f ? "Idle" : "Walk");
        _movement.SetMovement(dir);
    }
    public void Attack(Vector2 dir)
    {
        throw new System.NotImplementedException();
    }

    public void LookAt(Vector2 pos)
    {
        throw new System.NotImplementedException();
    }
}

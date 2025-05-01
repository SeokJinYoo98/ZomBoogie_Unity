using System.Data;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public abstract class BaseEnemy : MonoBehaviour
{
    public enum State
    {
        Idle,
        Walk,
        Attack,
        Death
    }
  
    protected SpriteRenderer      _spriteRenderer;
    protected Rigidbody2D         _rigidbody;
    protected EnemyData           _enemyData;

    protected float               _moveSpeed = 1.0f;
    protected float               _animTimer;
    protected int                 _animIndex;

    private   State               _currState;

    protected void Awake()
    {
        _spriteRenderer     = GetComponent<SpriteRenderer>();
        _rigidbody          = GetComponent<Rigidbody2D>();

        _currState          = State.Idle;
    }
    protected void Update()
    {
        UpdateState( );
    }
    private void HandleState(State state)
    {
        switch (state)
        {
            case State.Idle:    UpdateAnimation(_enemyData.Idle);   break;
            case State.Walk:    UpdateAnimation(_enemyData.Walk);   break;
            case State.Attack:  UpdateAnimation(_enemyData.Attack); break;
            case State.Death:   UpdateAnimation(_enemyData.Death);  break;
        }
    }
    private void UpdateState()
    {
        // 1. 플레이어를 찾는다.
        Transform target = FindPlayer();
        if (target == null)
        {
            _currState = State.Idle;
            return;
        }

        // 2. 방향을 구한다.
        Vector2 dir = GetDirection(target);

        // 3. 플립 여부를 결정한다.
        CheckFlipX( dir.x );

        // 4. 움직인다.
        if (dir.sqrMagnitude > 0.0001f)
        {
            _currState = State.Walk;
            _rigidbody.linearVelocity = dir * _moveSpeed;
        }
        else
        {
            _rigidbody.linearVelocity = Vector2.zero;
        }

        // 5. 공격이 있다면 공격을 수행한다.
        if (PrepareAttack( ))
        {
            Attack( );
        }

        // 6. 상태를 업데이트한다.
        HandleState( _currState );
    }
    private void UpdateAnimation(AnimationData anim)
    {
        _animTimer += Time.deltaTime;

        if (_animTimer >= anim.frameTime)
        {
            _animTimer -= anim.frameTime;
            _animIndex = (_animIndex + 1) % anim.frames.Length;
            _spriteRenderer.sprite = anim.frames[_animIndex];
        }
    }
    public void SetEnemyData(EnemyData data)
    {
        _enemyData = data;
        _animTimer = 0.0f;
        _animIndex = 0;
    }

    private Transform FindPlayer()
    {
        return GameObject.FindWithTag("Player")?.transform;
    }
    void CheckFlipX(float dirX)
    {
        _spriteRenderer.flipX = dirX <= 0;
    }
    Vector2 GetDirection(Transform target)
    {
        Vector3 dir = target.position - transform.position;
        return dir.normalized;
    }
    protected virtual bool PrepareAttack()
    {
        return false;
    }
    protected virtual bool Attack()
    {
        return false;
    }

}
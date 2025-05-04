using System.Data;
using UnityEngine;
using System;

using static UnityEditor.Experimental.GraphView.GraphView;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public abstract class BaseEnemy : MonoBehaviour, IDamageable
{
    public enum State { Idle, Walk, Attack, Hit, Dead, Think }

    protected EnemyData           _data;
    protected SpriteRenderer      _sr;
    protected Rigidbody2D         _rb;


    private   Vector2             _moveDir;

    protected float               _animTimer;
    protected int                 _animIndex;

    private   State               _currState;
    private   Transform           _target;
    private   float               _deadHitTime = 0.0f;

    private int _hp;

    private Vector2 _movement;
    protected void Awake()
    {
        _sr         = GetComponent<SpriteRenderer>();
        _rb          = GetComponent<Rigidbody2D>();

        _target             = FindPlayer( );
        _moveDir            = Vector2.zero;

        SetState(State.Idle);
    }
    protected void Update()
    {
        UpdateState( );
        UpdateAnimation( );
    }
    private void FixedUpdate()
    {
        _rb.linearVelocity = _movement;
    }
    private void UpdateState()
    {
        if (_currState == State.Hit)
        {
            _deadHitTime += Time.deltaTime;
            if (0.5f <= _deadHitTime)
            {
                _deadHitTime = 0.0f;
                if (_hp <= 0)
                {
                    SetState( State.Dead );
                }
                else
                {
                    SetState( State.Idle );
                }
            }
        }
        else if (_currState == State.Dead)
        {
            _deadHitTime += Time.deltaTime;
            if (0.3f <= _deadHitTime)
            {
                _deadHitTime = 0.0f;
                SetState( State.Idle );
                EnemySpawner.gInstance.ReturnEnemy(gameObject);
            }
            if (0 < _hp)
            {
                SetState( State.Idle );
            }
        }
        else if (_currState == State.Idle)
        {
            if (_target == null)
            {
                var target = FindPlayer( );

                if (target != null)
                {
                    _target = target;
                }
            }
            else
            {
                SetState( State.Walk );
            }
        }
        else if (_currState == State.Walk)
        {
            Vector2 toTarget = _target.position - transform.position;
            float   distSq   = toTarget.sqrMagnitude;
            if (distSq > 0.0001f)
            {
                _moveDir = toTarget.normalized;
                SetState( State.Walk );

            }
            else if (_data.EnemyStats.attackRange != 0)
            {
                Debug.Log( "Attack" );
            }
            else
            {
                _moveDir = Vector2.zero;
                SetState( State.Idle );
            }
            CheckFlipX( _moveDir.x );
        }
        _movement = _moveDir * _data.EnemyStats.moveSpeed;
    }
    private void SetState(State next)
    {
        if (_currState == next) return;

        _currState = next;
        _moveDir = Vector2.zero;
        _animTimer  = _animIndex = 0;
    }
    private void UpdateAnimation()
    {
        var anim = _data.GetAnimData( _currState );
        // 프레임이 1개밖에 없으면 매 Update마다 그 프레임 그리기
        if (anim.frames.Length == 1)
        {
            _sr.sprite = anim.frames[0];
            return;
        }

        _animTimer += Time.deltaTime;
        while (_animTimer >= anim.frameTime)
        {
            _animTimer -= anim.frameTime;
            _animIndex = (_animIndex + 1) % anim.frames.Length;
            _sr.sprite = anim.frames[_animIndex];
        }
    }

   
    public void SetEnemyData(EnemyData data)
    {
        _data = data;
        _animTimer = 0.0f;
        _animIndex = 0;
        _moveDir = Vector2.zero;
        _hp = data.EnemyStats.hp;
        SetState( State.Idle );
    }

    private Transform FindPlayer()
    {
        return GameObject.FindWithTag("Player")?.transform;
    }


    public bool TakeDamage(int damage)
    {
        // 이미 피격 무적(Hit) 중이거나 죽어 있으면 무시
        if (_currState == State.Hit || _currState == State.Dead)
            return false;

        _hp -= damage;
        SetState(State.Hit);
        
        return true;
    }
    public int GetDamage()
    {
        return _data.EnemyStats.damage;
    }
    void CheckFlipX(float dirX)
    {
        _sr.flipX = dirX <= 0;
    }
}
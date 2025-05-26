using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public abstract class BaseEnemy : MonoBehaviour, IDamageable
{
    public enum State { Idle, Walk, Hit, Dead }

    protected EnemyData           _data;
    protected SpriteRenderer      _sr;
    protected Rigidbody2D         _rb;

    protected Vector2             _moveDir;

    protected float               _animTimer;
    protected int                 _animIndex;

    private     State               _currState;
    private     float               _currFrameTime = 0.0f;
   
    private     Transform           _target;
    private     float               _deadHitTime = 0.0f;
    protected   string              _hitAudioName;

    const       float           _hitTime = 0.1f;
    const       float           _deadTime = 0.5f;
    protected   int             _frameDivide = 1;
    protected   float           _speedMag;
    private     int             _hp;

    protected void Awake()
    {
        _sr                 = GetComponent<SpriteRenderer>();
        _rb                 = GetComponent<Rigidbody2D>();

        _target             = FindPlayer( );
        _moveDir            = Vector2.zero;

        SetState(State.Idle);
        _speedMag = 1.0f;
    }
    protected void Update()
    {
        if (!_data || Time.timeScale <= 0.1f) return;
        UpdateState( );
        UpdateAnimation( );
        CoolTime( );
    }
    private void FixedUpdate()
    {
        _rb.linearVelocity = _moveDir * _data.EnemyStats.moveSpeed * _speedMag;
    }
    private void UpdateState()
    {
        if (_currState == State.Hit)
        {
            _moveDir = Vector2.zero;
            _deadHitTime += Time.deltaTime;
            if (_hitTime <= _deadHitTime)
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
            if (_deadTime <= _deadHitTime)
            {
                EnemyManager.Instance.ReturnEnemy( gameObject );
            }
        }
        else if (_currState == State.Idle)
        {
            _moveDir = Vector2.zero;
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
            if (distSq > 0.1f)
            {
                _moveDir = toTarget.normalized;
                SetState( State.Walk );
            }

            else
            {
                _moveDir = Vector2.zero;
                SetState( State.Idle );
            }

            if (PrepareAttack( distSq ))
            {
                Attack( );
            }

            CheckFlipX( _moveDir.x );
        }
        
    }
    protected void SetState(State next)
    {
        if (_currState == next) return;

        _currState = next;
        _moveDir = Vector2.zero;
        _animTimer  = _animIndex = 0;
    }
    private void UpdateAnimation()
    {
        if (!_data) return;

        var anim = _data.GetAnimData(_currState);
        _currFrameTime = anim.frameTime / _frameDivide;

        if (anim.frames.Length == 1)
        {
            _sr.sprite = anim.frames[0];
            return;
        }

        _animTimer += Time.deltaTime;
        while (_animTimer >= _currFrameTime)
        {
            _animTimer -= _currFrameTime;
            _animIndex = (_animIndex + 1) % anim.frames.Length;
            _sr.sprite = anim.frames[_animIndex];
        }
    }

    public void Init()
    {
        _speedMag = 1.0f;
        _animTimer = 0.0f;
        _animIndex = 0;
        _moveDir = Vector2.zero;
        _hp = _data.EnemyStats.hp;
        SetState( State.Idle );

        var anim = _data.GetAnimData(State.Idle);
        _sr.sprite = anim.frames[0];
        gameObject.SetActive( true );
    }
    public void SetEnemyData(EnemyData data)
    {
        _data = data;
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
        AudioManager.Instance.PlaySfx( _hitAudioName );
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

    protected abstract bool PrepareAttack(float distSq);
    protected abstract void Attack();
    protected virtual void CoolTime()
    {

    }
    public (int currHp, int maxHp) GetHp() => (_hp, _data.EnemyStats.hp);
}
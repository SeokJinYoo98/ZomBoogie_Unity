using System.Collections;
using Unity.IO.LowLevel.Unsafe;
using Unity.VisualScripting;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
// gameObject는 현재 스크립트가 붙어 있는 오브젝트 그 자체를 가리키는 참조
public class Boogie : MonoBehaviour, IDamageable
{
    [SerializeField] private BoogieStatus   _status;
    [SerializeField] private Weapon         _gun;
    private Animator       _anim;
    private Rigidbody2D    _rb;
    private SpriteRenderer _sr;
   
    private Vector2                         _moveDir = Vector2.zero;
    private BaseStates                      _state;
    private float _coolTime;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        _anim = GetComponent<Animator>( );
        _sr = GetComponent<SpriteRenderer>( );
        _rb = GetComponent<Rigidbody2D>( );
        _coolTime = 0.0f;

        GetComponent<CircleCollider2D>( ).radius = _status.ItemRange;
    }
    void Start()
    {
        _status.CurrHp = _status.MaxHp;
        _state = new BaseStates();
        _anim.SetBool("Idle", true);
        _state.SetState("Idle");
    }

    // Update is called once per frame
    void Update()
    {
        CoolTime( );
        InputProcess();
        StateProcess(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        AnimateProcess();
    }
    private void FixedUpdate()
    {
        Move();
    }
    private void InputProcess()
    {
        _moveDir = Vector2.zero;

        if (Input.GetKey(KeyCode.A))        _moveDir += Vector2.left;
        if (Input.GetKey(KeyCode.D))        _moveDir += Vector2.right;
        if (Input.GetKey(KeyCode.S))        _moveDir += Vector2.down;
        if (Input.GetKey(KeyCode.W))        _moveDir += Vector2.up;
        if (Input.GetMouseButtonDown( 0 ))  Attack( );
        if (Input.GetMouseButtonDown(1))    EnemyManager.Instance.SpawnEnemy();
        
    }
    private void AnimateProcess()
    {
        if (_state.IsDirtyState())
        {
            _anim.SetBool(_state.mPrevState, false);
            _anim.SetBool(_state.mCurrState, true);
        }
    }
    private void StateProcess(Vector3 targetPos)
    {
        _sr.flipX = (_rb.position.x > targetPos.x);

        if (_moveDir.sqrMagnitude <= 0.001f)
        {
            _state.SetState("Idle");
        }
        else
        {
            bool isForward = 
                (_moveDir.x < 0f && _sr.flipX)
                || 
                (_moveDir.x >= 0f && !_sr.flipX);

            _state.SetState(isForward ? "WalkForward" : "WalkBackward");
        }
        _gun?.LookAt(targetPos, _sr.flipX);
    }
    private void Move()
    {
        _rb.linearVelocity = _moveDir * _status.Speed;
    }

    public bool TakeDamage(int damage)
    {
        AudioManager.Instance.PlaySfx( "PlayerHit" );
        _status.CurrHp -= damage;
        return true;
    }
    public int GetDamage()
    {
        return _status.Attack;
    }
    bool ReadyToAttack()
    {
        if (_status.AttackCoolTime <= _coolTime)
        {
            _coolTime = 0;
            return true;
        }
        return false;
    }
    void Attack()
    {
        if (ReadyToAttack())
        {
            _gun?.Fire( _status.Attack, _status.BulletSpeed);
        }
    }
    void CoolTime()
    {
        if (_coolTime < _status.AttackCoolTime)
            _coolTime += Time.deltaTime;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<BaseItem>(out var item))
        {
            item.Activate( _status );
        }
    }
    void OnTriggerEnter2D(Collider2D c)
    {

    }

}
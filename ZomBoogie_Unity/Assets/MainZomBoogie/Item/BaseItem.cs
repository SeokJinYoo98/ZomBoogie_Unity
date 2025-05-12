using UnityEngine;

[RequireComponent( typeof( SpriteRenderer ) )]
[RequireComponent( typeof( BoxCollider2D ) )]
public abstract class BaseItem : MonoBehaviour
{
    private SpriteRenderer _sr;
    private Rigidbody2D _rb;
    private Vector2 _moveDir = Vector2.zero;
    private float   _moveSpeed = 3.0f;
    private void Awake()
    {
        _sr = GetComponent<SpriteRenderer>();
        _rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        Debug.Log( "Item Enable" );
        _moveDir = Vector2.zero;
        gameObject.SetActive(true);
    }
    private void OnDisable()
    {
        Debug.Log( "Item DisEnable" );
        _moveDir = Vector2.zero;
        gameObject.SetActive(false);
    }
    private void FixedUpdate()
    {
        _rb.linearVelocity = _moveDir;
    }
    protected abstract void SpecialFunc();


    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!collision.CompareTag( "Player" )) return;
        
        Vector2 delta = (Vector2)collision.transform.position - (Vector2)transform.position;
        _moveDir = delta.normalized * _moveSpeed;
    }
}





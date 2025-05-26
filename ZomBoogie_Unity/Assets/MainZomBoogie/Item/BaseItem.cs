using UnityEngine;

[RequireComponent( typeof( SpriteRenderer ) )]
[RequireComponent( typeof( BoxCollider2D ) )]
public abstract class BaseItem : MonoBehaviour
{
    [SerializeField] private ItemType       _type;
    [SerializeField] private string         _sfxName;
    private                  Rigidbody2D    _rb;
    private                  float          _moveSpeed = 5.0f;
   
    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        _rb.linearVelocity = Vector2.zero;
    }
    private void OnDisable()
    {
        _rb.linearVelocity = Vector2.zero;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        //Debug.Log( "Stay" );
        Vector2 delta = (Vector2)other.transform.position - (Vector2)transform.position;
 
        _rb.linearVelocity = delta.normalized * _moveSpeed;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        //Debug.Log( "Exit" );
        _rb.linearVelocity = Vector2.zero;
    }
    public void Activate(BoogieStatus target)
    {
        PlaySound( );
        SpecialFunc( target );
        ReturnItem( );
    }
    protected abstract void SpecialFunc(BoogieStatus target);
    private void ReturnItem()
    {
        ItemManager.Instance.ReturnItem( gameObject, _type );
    }
    private void PlaySound()
    {
        AudioManager.Instance.PlaySfx( _sfxName );
    }
}





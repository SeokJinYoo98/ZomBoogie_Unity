using UnityEngine;
using System.Collections.Generic;
using System.Collections;
/*
    불릿이 가져야할 정보?
    1. 방향
    2. 속도
    3. 범위
    4. 관통 횟수
*/


[RequireComponent (typeof(SpriteRenderer))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public class Bullet : MonoBehaviour
{
    public struct BulletInfo
    {
        public Vector2     direction;
        public float       speed;
        public float       range;
        public float       travel;
        public int         penetrate;
        public bool        isZombie;
        public int         Damage;

        public BulletInfo(Vector2 dir, float speed, float range, int pen, bool isZombie, int damage)
        {
            this.direction  = dir;
            this.speed      = speed;
            this.range      = range;
            this.travel     = 0.0f;
            this.penetrate  = pen;
            this.isZombie   = isZombie;
            this.Damage     = damage;
        }
    }
   //

    private SpriteRenderer      mSprite;
    private Rigidbody2D         mRigidBody;
    private Collider2D          mCollider;

    [SerializeField] private List<Sprite> _sprites;

    private BulletInfo         mInfo;

    private void Awake()
    {
        mSprite     = GetComponent<SpriteRenderer>();
        mRigidBody  = GetComponent<Rigidbody2D>();
        mCollider   = GetComponent<Collider2D>();
    }

    public void Init(BulletInfo bulletInfo)
    {
        mInfo = bulletInfo;
        
        mRigidBody.linearVelocity = mInfo.direction * mInfo.speed;

        if (mInfo.isZombie)
        {
            mSprite.sprite = _sprites[2];
        }
        else
        {
            mSprite.sprite = _sprites[1];
        }
    }

    private void FixedUpdate()
    {
        float delta = mRigidBody.linearVelocity.magnitude * Time.fixedDeltaTime;
        mInfo.travel += delta;

        if (mInfo.travel >= mInfo.range)
        {
            BulletPool.gInstance.ReturnBullet(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var collider = collision.collider;
        if (mInfo.isZombie && !collider.CompareTag( "Player" ))
            return;
        
        else if (!mInfo.isZombie && !collider.CompareTag( "Enemy" )) 
            return;

        if (collider.TryGetComponent<IDamageable>( out var target ))
        {
            if (target.TakeDamage( mInfo.Damage ))
            {
                if (--mInfo.penetrate <= 0)
                    BulletPool.gInstance.ReturnBullet( gameObject );
            }
        }
    }
}

using UnityEngine;

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

        public BulletInfo(Vector2 dir, float speed, float range, int pen, bool isZombie)
        {
            this.direction  = dir;
            this.speed      = speed;
            this.range      = range;
            this.travel     = 0.0f;
            this.penetrate  = pen;
            this.isZombie   = isZombie;
        }
    }

    private SpriteRenderer      mSprite;
    private Rigidbody2D         mRigidBody;
    private Collider2D          mCollider;

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
}

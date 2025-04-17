using UnityEngine;

/*
    불릿이 가져야할 정보?
    1. 방향
    2. 속도
    3. 범위
    4. 관통 횟수
*/
public struct BulletInfo 
{
    Vector2 direction;
    float   speed;
    float   range;
    float   travel;
    int     penetrate;
    bool    isZombie;
}

[RequireComponent (typeof(SpriteRenderer))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public class CS_Bullet : MonoBehaviour
{
    SpriteRenderer  mSprite;
    Rigidbody2D     mRigidBody;
    Collider2D      mCollider;
    BulletInfo      mBulletInfo;
    public void Init(BulletInfo bulletInfo, Vector3 startPos)
    {
        transform.position  = startPos;
        mBulletInfo         = bulletInfo;
    }
    private void Awake()
    {
        
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        
    }
}

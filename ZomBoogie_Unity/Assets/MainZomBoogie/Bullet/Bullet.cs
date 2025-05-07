using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using Unity.AppUI.Core;
using UnityEditor;
/*
    불릿이 가져야할 정보?
    1. 방향
    2. 속도
    3. 범위
    4. 관통 횟수
*/


[RequireComponent (typeof(SpriteRenderer))]
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
    private float       _lifeTime = 0.0f;
    private Collider2D  _prevTarget = null;
   //

    private SpriteRenderer      mSprite;

    [SerializeField] private List<Sprite> _sprites;

    private BulletInfo         mInfo;

    private void Awake()
    {
        mSprite     = GetComponent<SpriteRenderer>();
    }

    public void Init(BulletInfo bulletInfo)
    {
        mInfo = bulletInfo;
       
        if (mInfo.isZombie)
        {
            mSprite.sprite = _sprites[2];
        }
        else
        {
            mSprite.sprite = _sprites[1];
        }

        _lifeTime = mInfo.range / mInfo.speed;

        CancelInvoke( nameof( Return ) );
        Invoke( nameof( Return ), _lifeTime );

    }
    private void Update()
    {
        transform.Translate( mInfo.direction * mInfo.speed * Time.deltaTime, Space.World );
        if (mInfo.range <= mInfo.travel)
        {
            BulletPool.gInstance.ReturnBullet( gameObject );
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (mInfo.isZombie && !other.CompareTag( "Player" ))
            return;

        else if (!mInfo.isZombie && !other.CompareTag( "Enemy" ))
            return;

        if (_prevTarget == other) return;
        if (other.TryGetComponent<IDamageable>( out var target ))
        {
            bool hit = target.TakeDamage( mInfo.Damage );
            if (hit)
            {
                _prevTarget = other;
                --mInfo.penetrate;
            }
            

            if ( mInfo.penetrate <= 0 )
            {
                CancelInvoke( nameof( Return ) );
                Return( );
            }
        }
    }
    private void Return()
    {
        // 풀에 반환
        BulletPool.gInstance.ReturnBullet( gameObject );
    }
    private void OnDisable()
    {
        // 비활성화될 때 예약 취소
        CancelInvoke( nameof( Return ) );
    }
}
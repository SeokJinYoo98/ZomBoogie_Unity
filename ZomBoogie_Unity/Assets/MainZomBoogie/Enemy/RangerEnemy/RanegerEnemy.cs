using UnityEngine;

public class RanegerEnemy : BaseEnemy
{
    private float _attackCoolTime = 1.0f;
    private float _attackElapsed = 0.0f;

    bool  _canAttack = false;
    float _halfRange;
  
    void Start()
    {
        _halfRange = _data.EnemyStats.attackRange / 2;
    }

    override protected bool  PrepareAttack(float distSq)
    {
        if (distSq < _halfRange)
        {
            _speedMag = -1.0f;
        }
        else if (_halfRange <= distSq && distSq <= _data.EnemyStats.attackRange)
        {
            _speedMag = 0.0f;
        }
        else
        {
            _speedMag = 1.0f;
        }

        if (_data.EnemyStats.attackRange < distSq) return false;
        if (!_canAttack) return false;

 
        return true;
    }
    override protected void Attack() 
    {
        AudioManager.Instance.PlaySfx( "RangerAttack" );
        _attackElapsed = 0.0f;
        _canAttack = false;

        var bulletObj = BulletPool.Instance.GetBullet();
        bulletObj.transform.position = transform.position;
        Vector2 dir = _moveDir * (_sr.flipX ? -1 : 1);
        var bulletInfo = new Bullet.BulletInfo(
                _moveDir,
                5.0f,
                _data.EnemyStats.attackRange,
                1,
                true,
                _data.EnemyStats.damage);
        bulletObj.GetComponent<Bullet>( ).Init( bulletInfo );
    }

    override protected void CoolTime()
    {
        if (_attackElapsed <= _attackCoolTime)
        {   
            _attackElapsed += Time.deltaTime;
        }
        else
        {
            _canAttack = true;
        }
    }
}

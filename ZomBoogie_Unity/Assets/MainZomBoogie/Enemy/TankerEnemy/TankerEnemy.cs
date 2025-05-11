using UnityEngine;

public class TankerEnemy : BaseEnemy
{
    bool _run = false;
    bool _prev = false;
    private void Start()
    {
        _hitAudioName = "TankerHit";
    }
    protected override bool PrepareAttack(float distSq)
    {
        if (_moveDir.magnitude <= 0.1f)
        {
            _speedMag = 0.0f;
        }
        _prev = _run;
        _run = distSq < _data.EnemyStats.attackRange;

        if (_run != _prev)
        {
            if (_run)
            {
                _speedMag = 2.0f;
                _frameDivide = 2;
            }
            else
            {
                _speedMag = 1.0f;
                _frameDivide = 1;
            }
        }

        return false;
    }
    protected override void Attack()
    {

        
    }
}

using UnityEngine;

public class DefaultZombie : BaseEnemy
{
    private void Start()
    {
        _hitAudioName = "DefaultHit";
    }

    protected override bool PrepareAttack(float distSq)
    {
        return false;
    }
    protected override void Attack()
    {
    }
    protected override void CoolTime()
    {
    }
}

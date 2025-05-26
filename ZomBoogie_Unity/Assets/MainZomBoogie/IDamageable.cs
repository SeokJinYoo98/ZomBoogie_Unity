using UnityEngine;

public interface IDamageable
{
    bool    TakeDamage(int damage);
    int     GetDamage();
    public (int currHp, int maxHp) GetHp();
}

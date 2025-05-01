using UnityEngine;

[System.Serializable]
public struct AnimationData
{
    public Sprite[] frames;
    public float    frameTime;
}
[System.Serializable]
public struct EnemyStats
{
    public int maxHp;
    public int moveSpeed;
    public int damage;
}

[CreateAssetMenu(fileName = "Enemy", menuName = "Enemy/EnemyData")]
public class EnemyData : ScriptableObject
{
    public AnimationData Idle;
    public AnimationData Attack;
    public AnimationData Death;
    public AnimationData Walk;

    public EnemyStats    EnemyStats;
}

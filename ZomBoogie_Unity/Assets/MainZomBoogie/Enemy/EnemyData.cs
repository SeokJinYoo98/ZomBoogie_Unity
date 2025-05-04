using Unity.VisualScripting;
using UnityEngine;

[System.Serializable]
public class AnimationData
{
    public Sprite[] frames;
    public float    frameTime;
}
[System.Serializable]
public struct EnemyStats
{
    public int      hp;
    public float    moveSpeed;
    public int      damage;
    public float    attackRange;
}

[CreateAssetMenu(fileName = "Enemy", menuName = "Enemy/EnemyData")]
public class EnemyData : ScriptableObject
{
    public AnimationData GetAnimData(BaseEnemy.State state) => state switch
    {
        BaseEnemy.State.Idle =>     Idle,
        BaseEnemy.State.Walk =>     Walk,
        BaseEnemy.State.Attack =>   Attack,
        BaseEnemy.State.Dead =>     Dead,
        BaseEnemy.State.Hit =>      Hit,
        _ => default 
    };
    public AnimationData Idle;
    public AnimationData Attack;
    public AnimationData Dead;
    public AnimationData Hit;
    public AnimationData Walk;

    public EnemyStats    EnemyStats;
}


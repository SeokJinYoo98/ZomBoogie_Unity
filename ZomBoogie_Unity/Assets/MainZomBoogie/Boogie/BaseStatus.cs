using System.Collections.Generic;
using System.Diagnostics;
using System.Numerics;

[System.Serializable]
public class BoogieStatus
{
    public int CurrXp;
    public int MaxXp;
    public int CurrHp;
    public int MaxHp;
    public int Attack;
    public float Speed;
    public float AttackCoolTime;
    public float AttackRange;
    public float ItemRange;
    public float BulletSpeed;
    public float MagTime;

    public void IncreaseXp(int xp)
    {
        CurrXp += xp;
        if (MaxXp <= CurrXp)
        {
            CurrXp = 0;
            ++MaxXp;
            LevelManager.Instance.PlayerLevelUp( );
        }
    }
}
public class BaseStates
{
    public bool IsDirtyState()
    {
        return mCurrState != mPrevState;
    }
    public void SetState(string state)
    {
        mPrevState = mCurrState;
        mCurrState = state;
    }
    public string   mCurrState;
    public string   mPrevState;
}

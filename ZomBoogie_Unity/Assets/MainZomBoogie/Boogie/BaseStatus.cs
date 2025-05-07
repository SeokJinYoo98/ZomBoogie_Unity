using System.Collections.Generic;
using System.Diagnostics;
using System.Numerics;



[System.Serializable]
public class BaseStatus
{
    public int mHealth;
    public int mAttack;
    public float mSpeed;
}
[System.Serializable]
public class BoogieStaus : BaseStatus
{
    public float mAttackCoolTime;
    public float mAttackRange;
    public float mItemRange;
    public float BulletSpeed;
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

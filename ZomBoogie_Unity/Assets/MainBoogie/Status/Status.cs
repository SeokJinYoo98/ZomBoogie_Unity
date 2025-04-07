using System.Collections.Generic;
using System.Diagnostics;
using System.Numerics;

[System.Serializable]
public class BoogieStatus
{
    public float mHealth;
    public float mAttack;
    public float mSpeed;
}

public struct BoogieStates
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

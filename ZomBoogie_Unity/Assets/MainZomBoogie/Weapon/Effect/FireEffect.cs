using UnityEngine;
using UnityEngine.Audio;

public class FireEffect : MonoBehaviour
{
    [SerializeField] private SpriteRenderer     mEffectRenderer;
    [SerializeField] private Sprite[]           mFrames;
    [SerializeField] private float              mFrameTime;
    private int         mCurrFrame = 0;
    private float       mTimer = 0f;

    private bool    mPaused;
    private void Awake()
    {
        mPaused                 = false;
        mEffectRenderer.enabled = false;
    }
    // Update is called once per frame
    void Update()
    {
        if (mPaused) return;

        mTimer += Time.deltaTime;
        if (mTimer > mFrameTime)
        {
            mTimer = 0.0f;
            if (mCurrFrame >= mFrames.Length)
            {
                mCurrFrame = 0;
                mEffectRenderer.enabled = false;
                mPaused = true;
            }
            mEffectRenderer.sprite = mFrames[mCurrFrame];
            ++mCurrFrame;
        }
    }
    public void Play()
    {
        mCurrFrame = 0;
        mEffectRenderer.enabled = true;
        mPaused = false;
    }

    public void FlipX(bool flipX)
    {
        mEffectRenderer.flipX = flipX;
    }
}

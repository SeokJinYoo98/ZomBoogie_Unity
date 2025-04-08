using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private Boogie         mOwner;
    [SerializeField] private SpriteRenderer mSpriteRenderer;
    private float                           mPosX;
    private float                           mAngle;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mPosX   = 0.12f;
        mAngle  = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        Rotate(mAngle);

        if (mOwner.IsFlipX())
        {
            if (!mSpriteRenderer.flipX)
            {
                mSpriteRenderer.flipX = true;
                mPosX = -0.12f;
            }
        }
        else
        {
            if (mSpriteRenderer.flipX)
            {
                mSpriteRenderer.flipX = false;
                mPosX = 0.12f;
            }

        }
        transform.localPosition = new Vector3(mPosX, -0.07f, 0.0f);
    }
    private void FixedUpdate()
    {

    }
    void Rotate(float angle)
    {
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
    public void LookAt(Vector3 pos)
    {
        float deltaX = pos.x - transform.position.x;
        float deltaY = pos.y - transform.position.y;
        mAngle = Mathf.Atan2(deltaY, deltaX) * Mathf.Rad2Deg;

        if (mOwner.IsFlipX())
        {
            mAngle += 180f;  // 플립 보정
        }
    }
}

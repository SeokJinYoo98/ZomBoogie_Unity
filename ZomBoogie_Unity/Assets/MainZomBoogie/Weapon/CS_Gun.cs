using Unity.VisualScripting;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private SpriteRenderer mSpriteRenderer;
    [SerializeField] private Transform      mFireTrasnform;
    private CS_FireEffect                   mFireEffect;
    private float mAngle;
    private void Awake()
    {
        mFireEffect = GetComponentInChildren<CS_FireEffect>();   
    }

    private void Start()
    {
        mAngle = 0.0f;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void LookAt(Vector3 pos, bool flipX)
    {
        Vector3 dir = pos - transform.position;
        mAngle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        if (flipX) mAngle += 180.0f;

        transform.rotation = Quaternion.AngleAxis(mAngle, Vector3.forward);

        if (flipX != mSpriteRenderer.flipX)
        {
            mSpriteRenderer.flipX = flipX;

            Vector3 newMyPos = transform.localPosition;
            newMyPos.x = -newMyPos.x;
            transform.localPosition = newMyPos;

            Vector3 newAimPos = mFireTrasnform.transform.localPosition;
            newAimPos.x = -newAimPos.x;
            mFireTrasnform.localPosition = newAimPos;
            mFireEffect.FlipX(flipX);
        }
    }
    public void Attack()
    {
        Debug.Log(transform.forward);
        mFireEffect?.Play();
    }
}

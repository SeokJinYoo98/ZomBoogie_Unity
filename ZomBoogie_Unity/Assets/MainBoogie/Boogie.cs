using System.Collections;
using Unity.IO.LowLevel.Unsafe;
using Unity.VisualScripting;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.EventSystems;
// gameObject는 현재 스크립트가 붙어 있는 오브젝트 그 자체를 가리키는 참조
public class Boogie : MonoBehaviour
{
    [SerializeField] private BoogieStatus   mStatus;
    [SerializeField] private Animator       mAnim;
    [SerializeField] private Rigidbody2D    mRigidBody;
    [SerializeField] private SpriteRenderer mSpriteRenderer;
    [SerializeField] private Weapon         mCurrWeapon;
    private Vector2                         mMoveDirection = Vector2.zero;
    private BoogieStates                    mState;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mAnim.SetBool("Idle", true);
        mState.SetState("Idle");
    }

    // Update is called once per frame
    void Update()
    {
        InputProcess();
        StateProcess(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        AnimateProcess();
    }
    private void FixedUpdate()
    {
        Move();
    }
    private void InputProcess()
    {
        mMoveDirection = Vector2.zero;

        if (Input.GetKey(KeyCode.A)) mMoveDirection += Vector2.left;
        if (Input.GetKey(KeyCode.D)) mMoveDirection += Vector2.right;
        if (Input.GetKey(KeyCode.S)) mMoveDirection += Vector2.down;
        if (Input.GetKey(KeyCode.W)) mMoveDirection += Vector2.up;
    }
    private void AnimateProcess()
    {
        if (mState.IsDirtyState())
        {
            Debug.Log("Dirty");
            Debug.Log(mState.mPrevState + " " + mState.mCurrState);
            mAnim.SetBool(mState.mPrevState, false);
            mAnim.SetBool(mState.mCurrState, true);
        }
    }
    private void StateProcess(Vector3 targetPos)
    {
        if (mRigidBody.position.x <= targetPos.x)
        {
            if (mSpriteRenderer.flipX)
            {
                mSpriteRenderer.flipX = false;
            }
        }
        else
        {
            if (!mSpriteRenderer.flipX)
            {
                mSpriteRenderer.flipX = true;
            }
        }

        if (mMoveDirection.sqrMagnitude <= 0.001f)
        {
            mState.SetState("Idle");
        }
        else
        {
            if (mMoveDirection.x < 0.00f)
            {
                if (mSpriteRenderer.flipX)
                    mState.SetState("WalkForward");
                else
                    mState.SetState("WalkBackward");
            }
            else
            {
                if (mSpriteRenderer.flipX)
                    mState.SetState("WalkBackward");
                else
                    mState.SetState("WalkForward");
            }
        }
        if (mCurrWeapon)
        {
            mCurrWeapon.LookAt(targetPos);
        }
    }
    private void Move()
    {
        mRigidBody.MovePosition(mRigidBody.position + mMoveDirection * mStatus.mSpeed * Time.fixedDeltaTime);
    }

    public bool IsIdleState()
    {
        return !mAnim.GetBool("isMove");
    }
    public bool IsFlipX()
    {
        return mSpriteRenderer.flipX;
    }
}
using System.Collections;
using Unity.IO.LowLevel.Unsafe;
using Unity.VisualScripting;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
// gameObject는 현재 스크립트가 붙어 있는 오브젝트 그 자체를 가리키는 참조
public class Boogie : MonoBehaviour
{
    [SerializeField] private BoogieStaus    mStatus;
    [SerializeField] private Animator       mAnim;
    [SerializeField] private Rigidbody2D    mRigidBody;
    [SerializeField] private SpriteRenderer mSpriteRenderer;
    [SerializeField] private Weapon         mCurrWeapon;
    private Vector2                         mMoveDirection = Vector2.zero;
    private BaseStates                      mState;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mState = new BaseStates();
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

        if (Input.GetKey(KeyCode.A))        mMoveDirection += Vector2.left;
        if (Input.GetKey(KeyCode.D))        mMoveDirection += Vector2.right;
        if (Input.GetKey(KeyCode.S))        mMoveDirection += Vector2.down;
        if (Input.GetKey(KeyCode.W))        mMoveDirection += Vector2.up;
        if (Input.GetMouseButtonDown(0))    mCurrWeapon?.Fire();
        if (Input.GetMouseButtonDown(1))    EnemySpawner.gInstance.SpawnEnemy();
        
    }
    private void AnimateProcess()
    {
        if (mState.IsDirtyState())
        {
            mAnim.SetBool(mState.mPrevState, false);
            mAnim.SetBool(mState.mCurrState, true);
        }
    }
    private void StateProcess(Vector3 targetPos)
    {
        mSpriteRenderer.flipX = (mRigidBody.position.x > targetPos.x);

        if (mMoveDirection.sqrMagnitude <= 0.001f)
        {
            mState.SetState("Idle");
        }
        else
        {
            bool isForward = 
                (mMoveDirection.x < 0f && mSpriteRenderer.flipX)
                || 
                (mMoveDirection.x >= 0f && !mSpriteRenderer.flipX);

            mState.SetState(isForward ? "WalkForward" : "WalkBackward");
        }
        mCurrWeapon?.LookAt(targetPos, mSpriteRenderer.flipX);
    }
    private void Move()
    {
        mRigidBody.linearVelocity = mMoveDirection * mStatus.mSpeed;
    }
}
using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using Unity.Mathematics;

public class BackGround : MonoBehaviour
{
    [SerializeField] private Transform      mTargetTransform;
    [SerializeField] private Grid           mBackGroundGrid;
    
    private List<GameObject>                mTilemapObjs;

    private int                             mTileCount;
    private Vector3                         mPrevPos;
    private Vector3                         mScrollOffset;
    
    private Vector2                         mMinVector;
    private Vector2                         mMaxVector;
    private void Awake()
    {
        mTilemapObjs = new List<GameObject>();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mTileCount      =   30;
        mScrollOffset   =   Vector3.zero;
        mPrevPos        =   mTargetTransform.position;

        TileGenerator tileGen = gameObject.GetComponent<TileGenerator>();

        Transform gridTransform = mBackGroundGrid.transform;
        mTilemapObjs.Add(tileGen.GenTilemapObj(mTileCount, "Layer1", gridTransform));
        mTilemapObjs.Add(tileGen.GenTilemapObj(mTileCount, "Layer2", gridTransform));
        mTilemapObjs.Add(tileGen.GenTilemapObj(mTileCount, "Layer3", gridTransform));

        mTilemapObjs.Add(tileGen.GenTilemapObj(mTileCount, "Layer4", gridTransform));
        mTilemapObjs.Add(tileGen.GenTilemapObj(mTileCount, "Layer5", gridTransform));
        mTilemapObjs.Add(tileGen.GenTilemapObj(mTileCount, "Layer6", gridTransform));

        mTilemapObjs.Add(tileGen.GenTilemapObj(mTileCount, "Layer7", gridTransform));
        mTilemapObjs.Add(tileGen.GenTilemapObj(mTileCount, "Layer8", gridTransform));
        mTilemapObjs.Add(tileGen.GenTilemapObj(mTileCount, "Layer9", gridTransform));

        mTilemapObjs[0].transform.position = new Vector3(+0,            +0,         +0);
        mTilemapObjs[1].transform.position = new Vector3(-mTileCount,   +0,         +0);
        mTilemapObjs[2].transform.position = new Vector3(+mTileCount,   +0,         +0);

        mTilemapObjs[3].transform.position = new Vector3(+0,            +mTileCount,    +0);
        mTilemapObjs[4].transform.position = new Vector3(-mTileCount,   +mTileCount,    +0);
        mTilemapObjs[5].transform.position = new Vector3(+mTileCount,   +mTileCount,    +0);

        mTilemapObjs[6].transform.position = new Vector3(+0,            -mTileCount,    +0);
        mTilemapObjs[7].transform.position = new Vector3(-mTileCount,   -mTileCount,    +0);
        mTilemapObjs[8].transform.position = new Vector3(+mTileCount,   -mTileCount,    +0);

        mMinVector = new Vector2(-mTileCount, -mTileCount);
        mMaxVector = new Vector2(+mTileCount, +mTileCount);
    }

    private void FixedUpdate()
    {
        Vector3 pos = mTargetTransform.position;

        mScrollOffset += (pos - mPrevPos);
        mPrevPos = pos;

        if (mScrollOffset.x >= mTileCount)          ShiftRight();
        else if (mScrollOffset.x <= -mTileCount)    ShiftLeft();
        if (mScrollOffset.y >= mTileCount)          ShiftUp();
        else if (mScrollOffset.y <= -mTileCount)    ShiftDown();    
    }

    void ShiftLeft()
    {
        mScrollOffset.x = 0.0f;

        float ShiftPosX = mMinVector.x - mTileCount;

        foreach (GameObject obj in mTilemapObjs)
        {
            Vector3 pos = obj.transform.position; 
            if (Mathf.Approximately(pos.x, mMaxVector.x))
            {
                obj.transform.position = new Vector3(ShiftPosX, pos.y, 0.0f);
            }
        }
        mMaxVector.x -= mTileCount;
        mMinVector.x -= mTileCount;
    }
    void ShiftRight()
    {
        mScrollOffset.x = 0.0f;

        float ShiftPosX = mMaxVector.x + mTileCount;

        foreach (GameObject obj in mTilemapObjs)
        {
            Vector3 pos = obj.transform.position;
            if (Mathf.Approximately(pos.x, mMinVector.x))
            {
                obj.transform.position = new Vector3(ShiftPosX, pos.y, 0.0f);
            }
        }
        mMaxVector.x += mTileCount;
        mMinVector.x += mTileCount;
    }
    void ShiftUp()
    {
        mScrollOffset.y = 0.0f;

        float ShiftPosY = mMaxVector.y + mTileCount;

        foreach (GameObject obj in mTilemapObjs)
        {
            Vector3 pos = obj.transform.position;
            if (Mathf.Approximately(pos.y, mMinVector.y))
            {
                obj.transform.position = new Vector3(pos.x, ShiftPosY, 0.0f);
            }
        }
        mMaxVector.y += mTileCount;
        mMinVector.y += mTileCount;
    }
    void ShiftDown()
    {
        mScrollOffset.y = 0.0f;

        float ShiftPosY = mMinVector.y - mTileCount;

        foreach (GameObject obj in mTilemapObjs)
        {
            Vector3 pos = obj.transform.position;
            if (Mathf.Approximately(pos.y, mMaxVector.y))
            {
                obj.transform.position = new Vector3(pos.x, ShiftPosY, 0.0f);
            }
        }
        mMaxVector.y -= mTileCount;
        mMinVector.y -= mTileCount;
    }
}

using UnityEngine;
using System.Collections.Generic;

public class BulletPool : MonoBehaviour
{
    public static BulletPool            gInstance;
    [SerializeField] private GameObject mBulletPrefab;
    [SerializeField] private int        mPoolSize = 30;

    private Transform                   mBulletParent;
    private Queue<GameObject>           mBulletPool;
    private void Awake()
    {
        if (gInstance) return;
        gInstance = this;

        if (mBulletParent == null)
        {
            GameObject container = new GameObject("Bullets");
            container.transform.SetParent(this.transform);
            mBulletParent = container.transform;
        }

        mBulletPool = new Queue<GameObject>();

        for (int i = 0; i < mPoolSize; i++)
        {
            GameObject bullet = Instantiate(mBulletPrefab, mBulletParent);
            bullet.SetActive(false);
            mBulletPool.Enqueue(bullet);
        }
    }
    public GameObject GetBullet()
    {

        if (mBulletPool.Count > 0)
        {
            GameObject bullet = mBulletPool.Dequeue();
            bullet.SetActive(true);

            return bullet;
        }
        return Instantiate(mBulletPrefab, mBulletParent);
    }
    public void ReturnBullet(GameObject bullet)
    {
        bullet.SetActive(false);
        mBulletPool.Enqueue(bullet);
    }
}

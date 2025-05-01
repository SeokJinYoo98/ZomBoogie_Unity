using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;

public class EnemySpawner : MonoBehaviour
{
    public static EnemySpawner                  gInstance;
    [Header("프리팹 목록")]
    [SerializeField] private List<GameObject>   mEnemyPrefabs;
    [SerializeField] private List<EnemyData>    mEnemyDatas;

    public int EnemyIndex = 0;

    private void Awake()
    {
        if (gInstance) return;

        gInstance = this;
    }
    public void SpawnEnemy()
    {
        var enemy = Instantiate(mEnemyPrefabs[0]);
        enemy.SetActive(true);
        enemy.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        enemy.transform.position = new Vector3(enemy.transform.position.x, enemy.transform.position.y, 0.0f);
        enemy.GetComponent<BaseEnemy>().SetEnemyData(mEnemyDatas[EnemyIndex]);
    }
}

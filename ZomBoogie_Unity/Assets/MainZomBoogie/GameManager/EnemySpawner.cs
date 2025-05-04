using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;
enum EnemyType
{
    Default_1,
    Default_2,
    Default_3,
    Default_4,
    Ranger,
    Tanker
};

public class EnemySpawner : MonoBehaviour
{

    public static EnemySpawner                  gInstance;
    [Header("프리팹 목록")]
    [SerializeField] private List<GameObject>   _enemyPrefabs;
    [SerializeField] private List<EnemyData>    _enemyDatas;

    private Transform                           _enemyParent;
    private Queue<GameObject>                   _defaultPool;

    public int EnemyIndex = 0;

    //private int     _stageLevel = 0;
    //private float   _enemyZenTime = 0.0f;

    //private int     _defaultEnemies = 0;
    //private int     _rangeEnemies   = 0;
    //private int     _tankerEnemies  = 0;

    //private int _currDCnt = 0;
    //private int _currRCnt = 0;
    //private int _cuurDCnt = 0;
    private void Awake()
    {
        if (gInstance) return;

        gInstance = this;

        if (_enemyParent == null)
        {
            GameObject container = new GameObject("Enemies");
            container.transform.SetParent( this.transform );
            _enemyParent = container.transform;
        }

        _defaultPool = new Queue<GameObject>( );
    }
    public void SpawnEnemy()
    {
        GameObject enemy; 
        if (0 < _defaultPool.Count)
        {
            enemy = _defaultPool.Dequeue();
            enemy.SetActive( true );
        }
        else
        {
            enemy = Instantiate( _enemyPrefabs[0], _enemyParent );
        }
        enemy.transform.position = Camera.main.ScreenToWorldPoint( Input.mousePosition );
        enemy.transform.position = new Vector3(enemy.transform.position.x, enemy.transform.position.y, 0.0f);
        enemy.GetComponent<BaseEnemy>().SetEnemyData(_enemyDatas[EnemyIndex]);
    }
    public void ReturnEnemy(GameObject enemy)
    {
        enemy.SetActive(false);
        if (enemy.TryGetComponent<DefaultZombie>( out var dz ))
        {
            _defaultPool.Enqueue(enemy);
        }
        else
        {
            _defaultPool.Enqueue( enemy );
        }
    }
}

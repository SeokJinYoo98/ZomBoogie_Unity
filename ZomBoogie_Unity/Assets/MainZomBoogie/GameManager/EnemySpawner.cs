using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;

public class EnemySpawner : MonoBehaviour
{

    public static EnemySpawner                  Instance { get; private set; }
    [Header("프리팹 목록")]
    [SerializeField] private List<GameObject>   _enemyPrefabs;
    [SerializeField] private List<EnemyData>    _enemyDatas;

    private Transform                           _enemyParent;
    private Queue<GameObject>                   _defaultPool;
    private Queue<GameObject>                   _rangerPool;

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
        if (Instance)
        {
            Destroy( gameObject );
            return;
        }

        Instance = this;

        if (_enemyParent == null)
        {
            GameObject container = new GameObject("Enemies");
            container.transform.SetParent( this.transform );
            _enemyParent = container.transform;
        }

        _defaultPool = new Queue<GameObject>( );
        _rangerPool = new Queue<GameObject>( );
    }
    public void SpawnEnemy()
    {
        int roll = Random.Range(0, 100);
        GameObject enemy = SpawnRanger();
        //if (roll <= 10)
        //{
        //    enemy = SpawnRanger( );
        //}
        //else
        //{
        //    enemy = SpawnDefault( );
        //}

        Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        pos.z = 0f;
        enemy.transform.position = pos;
        enemy.SetActive( true );
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
    GameObject SpawnDefault()
    {
        int roll = Random.Range(0, 4);

        GameObject enemy;
        if (0 < _defaultPool.Count)
        {
            enemy = _defaultPool.Dequeue( );
            
        }
        else
        {
            enemy = Instantiate( _enemyPrefabs[0], _enemyParent );
        }

        enemy.GetComponent<BaseEnemy>( ).SetEnemyData( _enemyDatas[roll] );
        return enemy;
    }
    GameObject SpawnRanger()
    {
        GameObject enemy;
        if (0 < _rangerPool.Count)
        {
            enemy = _rangerPool.Dequeue( );
        }
        else
        {
            enemy = Instantiate( _enemyPrefabs[1], _enemyParent );
        }

        enemy.GetComponent<BaseEnemy>( ).SetEnemyData( _enemyDatas[4] );
        return enemy;
    }
}

using UnityEngine;
using System.Collections.Generic;
using System;
using Unity.VisualScripting;
enum EnemyType { Default, Ranger, Tanker };
public class EnemyManager : MonoBehaviour
{
    
    public static EnemyManager                  Instance { get; private set; }
    [Header("프리팹 목록")]
    [SerializeField] private List<GameObject>   _enemyPrefabs;
    [SerializeField] private List<EnemyData>    _enemyDatas;

    private Transform                           _enemyParent;
    private Queue<GameObject>                   _defaultPool;
    private Queue<GameObject>                   _rangerPool;
    private Queue<GameObject>                   _tankerPool;

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

        _defaultPool    = new Queue<GameObject>( );
        _rangerPool     = new Queue<GameObject>( ) ;
        _tankerPool     = new Queue<GameObject>( ) ;    
    }
    public void SpawnEnemy()
    {
  
        var enemy = GetEnemy( GetRandomEnemyType() );
        Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        pos.z = 0f;
        enemy.transform.position = pos;
        enemy.GetComponent<BaseEnemy>().Init();
 
    }
    EnemyType GetRandomEnemyType()
    {
        EnemyType type = (EnemyType)UnityEngine.Random.Range( 0, (int)EnemyType.Tanker + 1 );
        return type;
    }
    EnemyData GetEnemyData(EnemyType type)
    {
        switch (type) 
        {
            case EnemyType.Tanker:
                return _enemyDatas[5];
            case EnemyType.Ranger:
                return _enemyDatas[4];
            default:
                int roll = UnityEngine.Random.Range(0, 3);
                return _enemyDatas[roll];
        }
    }
    GameObject GetEnemy(EnemyType type)
    {
        Queue<GameObject> pool = type switch
        {
            EnemyType.Default => _defaultPool,
            EnemyType.Ranger  => _rangerPool,
            EnemyType.Tanker  => _tankerPool,
            _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
        };

        GameObject enemy = null;
        if (0 < pool.Count)
        {
            enemy = pool.Dequeue( );
        }
        else
        {
            enemy = Instantiate( _enemyPrefabs[(int)type], _enemyParent );
            enemy.GetComponent<BaseEnemy>( ).SetEnemyData( GetEnemyData(type) );
        }
        return enemy;
    }

    public void ReturnEnemy(GameObject enemy)
    {
        if (enemy.TryGetComponent<BaseEnemy>( out var enemyComp ))
        {
            // 비활성화 하고
            enemy.SetActive( false );

            // 타입별로 분기
            switch (enemyComp)
            {
                case DefaultZombie dz:
                    _defaultPool.Enqueue( enemy );
                    // dz 추가 로직…
                    break;
                case RangerEnemy re:
                    _rangerPool.Enqueue( enemy );
                    // re 추가 로직…
                    break;
                case TankerEnemy te:
                    _tankerPool.Enqueue( enemy );
                    // te 추가 로직…
                    break;
                default:
                    Debug.LogWarning( $"알 수 없는 BaseEnemy 서브타입: {enemyComp.GetType( ).Name}" );
                    break;
            }
        }
        else
        {
            Debug.LogError( $"BaseEnemy 컴포넌트를 찾을 수 없습니다: {enemy.name}" );
        }
    }

}

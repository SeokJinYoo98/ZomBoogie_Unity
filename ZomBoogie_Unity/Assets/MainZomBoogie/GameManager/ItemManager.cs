using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType { Coin, Vaccine, Magnetic };
public class ItemManager : MonoBehaviour
{
    public static ItemManager Instance { get; private set; }

    [SerializeField] private List<GameObject>       _itemPrefabs;

    private EnemyManager                            _enemyManager;
    private Dictionary<ItemType, Queue<GameObject>> _itemPools;
    private Transform                               _itemParent;


    private void Awake()
    {
        if (Instance)
        {
            Destroy( gameObject );
            return;
        }
        Instance = this;

        if (_itemParent == null)
        {
            GameObject container = new GameObject("Items");
            container.transform.SetParent( this.transform );
            _itemParent = container.transform;
        }

        _enemyManager = GetComponent<EnemyManager>( );

        _itemPools = new Dictionary<ItemType, Queue<GameObject>>();
        _itemPools[ItemType.Vaccine]    = new Queue<GameObject>();
        _itemPools[ItemType.Coin]       = new Queue<GameObject>();    
        _itemPools[ItemType.Magnetic]   = new Queue<GameObject>();
    }
    private void OnEnable()
    {
        if (_enemyManager)
            _enemyManager.OnEnemyReturned += HandleEnemyReturned;
    }
    private void OnDisable()
    {
        if (_enemyManager)
            _enemyManager.OnEnemyReturned -= HandleEnemyReturned;
    }

    void HandleEnemyReturned(Vector3 pos)
    {
        var item = GetItem( GetRandomType( ) );
        item.transform.position = pos;
    }

    GameObject GetItem(ItemType type)
    {
        Queue<GameObject> pool;
        GameObject prefab = null;
        switch (type)
        {
            case ItemType.Vaccine:
                prefab = _itemPrefabs[2];
                pool = _itemPools[ItemType.Vaccine];
                break;
            case ItemType.Magnetic:
                prefab = _itemPrefabs[1];
                pool = _itemPools[ItemType.Magnetic];
                break;
            default:
                prefab = _itemPrefabs[0];
                pool = _itemPools[ItemType.Coin];
                break;
        }

        if (0 < pool.Count)
        {
            return pool.Dequeue( );
        }
        Debug.Log( (int)type );
        return Instantiate( _itemPrefabs[(int)type], _itemParent );
    }

    ItemType GetRandomType()
    {
        int roll = Random.Range(0, 100 + 1);
        if (roll <= 5)
            return ItemType.Vaccine;
        else if (roll <= 95)
            return ItemType.Coin;
        return ItemType.Magnetic;
    }
}

using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Tilemaps;
using Unity.VisualScripting;

public class TileGenerator : MonoBehaviour
{
    [SerializeField] private List<Tile>         mTiles;

    public GameObject GenTilemapObj(int MaxTiles, string mapName, Transform parent)
    {
        GameObject      tilemapObject   = new GameObject(mapName);
        Tilemap         newTilemap      = tilemapObject.AddComponent<Tilemap>();
        TilemapRenderer newRender       = tilemapObject.AddComponent<TilemapRenderer>();
        tilemapObject.transform.parent = parent;

        int halfTile = Mathf.RoundToInt(MaxTiles / 2);
        for (int y = 0; y < MaxTiles; ++y)
        {
            for (int x = 0; x < MaxTiles; ++x)
            {
                newTilemap.SetTile(new Vector3Int(x - halfTile,  y - halfTile, 0), GetRandomTile());
            }
        }
        return tilemapObject;
    }

    private Tile GetRandomTile()
    {
        int tileType = UnityEngine.Random.Range(0, 20);
        if (5 < tileType)
        {
            tileType = 0;
        }
        return mTiles[tileType];
    }
}

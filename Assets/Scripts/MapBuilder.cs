using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapBuilder : MonoBehaviour
{
    [SerializeField]
    private GameObject TilePrefab;

    // Start is called before the first frame update
    public List<GameObject> BuildWorldMap(MapDataSO mapData, ITopHudPanel topHudPanel, IPCControler pCControler)
    {
        List<GameObject> worldTiles = new List<GameObject>();
        for (int i = 0; i < mapData.WorldWidth; i++)
        {
            for (int j = 0; j < mapData.WorldHeight; j++)
            {
                GameObject tile = Instantiate(TilePrefab);
                tile.transform.SetParent(transform);
                tile.transform.localPosition = new Vector3(1 * i, 0, 1 * j);
                tile.transform.localScale = new Vector3(1, 0.5f, 1);
                worldTiles.Add(tile);
                tile.GetComponent<MapTile>().InitTileData(topHudPanel, pCControler, i, j, mapData.ObstacleMap[i* mapData.WorldWidth + j]);
            }
        }

        return worldTiles;
    }
}

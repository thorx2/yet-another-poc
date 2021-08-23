using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Custom Game/Map data")]
public class MapDataSO : ScriptableObject
{
    public int WorldWidth;
    public int WorldHeight;

    public List<bool> ObstacleMap;

    public MapDataSO()
    {
        ObstacleMap = new List<bool>();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(MapDataSO))]
public class MapDataSOEditor : Editor
{
    private MapDataSO mapDataSO;

    public void OnEnable()
    {
        mapDataSO = (MapDataSO)target;
    }

    public override void OnInspectorGUI()
    {
        mapDataSO.WorldWidth = EditorGUILayout.IntField("Set world width", mapDataSO.WorldWidth);
        mapDataSO.WorldHeight = EditorGUILayout.IntField("Set world Height", mapDataSO.WorldHeight);

        while (mapDataSO.ObstacleMap.Count < mapDataSO.WorldWidth * mapDataSO.WorldHeight)
        {
            mapDataSO.ObstacleMap.Add(false);
        }
        EditorGUILayout.HelpBox("Set tile obstacle data below, green is clear and red is blocked", MessageType.Info);

        EditorGUILayout.BeginVertical(EditorStyles.helpBox);
        var standardBGColour = GUI.backgroundColor;
        for (int x = 0; x < mapDataSO.WorldWidth; x++)
        {
            EditorGUILayout.Space(2);
            EditorGUILayout.BeginHorizontal();
            for (int y = 0; y < mapDataSO.WorldHeight; y++)
            {
                GUI.backgroundColor = mapDataSO.ObstacleMap[x * mapDataSO.WorldWidth + y] ? Color.green : Color.red;
                if (GUILayout.Button("Block cell"))
                {
                    mapDataSO.ObstacleMap[x * mapDataSO.WorldWidth + y] = !mapDataSO.ObstacleMap[x * mapDataSO.WorldWidth + y];
                }
            }
            EditorGUILayout.EndHorizontal();
        }
        EditorGUILayout.EndVertical();

        GUI.backgroundColor = standardBGColour;

        EditorUtility.SetDirty(mapDataSO);
    }
}

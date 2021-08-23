using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class GameManager : MonoBehaviour, IPCControler
{
    [SerializeField]
    private MapDataSO mapData;

    [SerializeField]
    private MapBuilder mapBuilder;

    [SerializeField]
    private Vector2 playerPosition;

    [SerializeField]
    private HudManager TopPanelRef;

    [SerializeField]
    private GameObject playerPrefab;

    private GameObject playerRef;

    private List<GameObject> mapTiles;

    public ITopHudPanel GetTopPanelRef
    {
        get => TopPanelRef;
    }

    private static GameManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        if (instance != this)
        {
            DestroyImmediate(this);
        }
    }

    public GameManager GetInstance
    {
        get => instance;
    }

    private void Start()
    {
        // This is bad? needs rework the map should not be here I guess...
        mapTiles = mapBuilder.BuildWorldMap(mapData, TopPanelRef, this);

        Astar.GetInstance.GenerateWorldNavigationData(mapData);

        playerRef = Instantiate(playerPrefab);
        int index = (int)(playerPosition.x * mapData.WorldWidth + playerPosition.y);
        playerRef.transform.position = mapTiles[index].transform.position;
        playerRef.transform.localScale = Vector3.one * 0.5f;
        playerRef.transform.DOMoveY(1.0f, 0.1f);
    }

    public IEnumerator MovePlayerToCell(int x, int y)
    {
        var path = Astar.GetInstance.FindPathBetween((int)playerPosition.x, (int)playerPosition.y, x, y);

        foreach (int tile in path)
        {
            var mySequence = DOTween.Sequence();
            Vector3 pathPos = mapTiles[tile].transform.position;
            pathPos.y = 1.0f;
            yield return mySequence.Append(playerRef.transform.DOMove(pathPos, 0.5f)).OnComplete(() =>
            {
                playerRef.transform.position = pathPos;
            }).WaitForCompletion();
        }

        if (path.Count > 0)
        {
            playerPosition.x = x;
            playerPosition.y = y;
        }
    }
}
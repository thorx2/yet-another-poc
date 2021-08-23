using System.Collections.Generic;
using UnityEngine;

public class Astar
{
    private static Astar instance;

    private Astar()
    {
        worldNavNodeList = new List<NavNodeData>();
    }

    private List<NavNodeData> worldNavNodeList;

    private MapDataSO mapdataSO;

    public static Astar GetInstance
    {
        get
        {
            if (instance == null)
            {
                instance = new Astar();
            }

            return instance;
        }
    }

    public void GenerateWorldNavigationData(MapDataSO mapdataSO)
    {
        this.mapdataSO = mapdataSO;
        for (int x = 0; x < mapdataSO.WorldWidth; x++)
        {
            for (int y = 0; y < mapdataSO.WorldHeight; y++)
            {
                bool walkable = mapdataSO.ObstacleMap[x * mapdataSO.WorldWidth + y];
                worldNavNodeList.Add(new NavNodeData(x, y, mapdataSO.WorldWidth, mapdataSO.WorldHeight, walkable));
            }
        }
    }

    public List<int> GenerateNavPathToCell(int x, int y)
    {
        List<int> cellIndex = new List<int>();

        return cellIndex;
    }

    // Ugly, to rewrite... to be optimized.... with fancy DS...
    public List<int> FindPathBetween(int startX, int startY, int endX, int endY)
    {
        NavNodeData startNode = worldNavNodeList[startX * mapdataSO.WorldWidth + startY];
        NavNodeData endNode = worldNavNodeList[endX * mapdataSO.WorldWidth + endY];

        List<NavNodeData> openPaths = new List<NavNodeData>();

        HashSet<NavNodeData> closedPath = new HashSet<NavNodeData>();

        openPaths.Add(startNode);

        while (openPaths.Count > 0)
        {
            NavNodeData currentCell = openPaths[0];
            for (int i = 1; i < openPaths.Count; i++)
            {
                if (openPaths[i].FCost < currentCell.FCost)
                {
                    currentCell = openPaths[i];
                }
                else if (openPaths[i].FCost == currentCell.FCost)
                {
                    if (openPaths[i].HCost < currentCell.HCost)
                    {
                        currentCell = openPaths[i];
                    }
                }
            }

            openPaths.Remove(currentCell);
            closedPath.Add(currentCell);

            if (currentCell.NodeIndex == endNode.NodeIndex)
            {
                List<int> returnPath = new List<int>();
                NavNodeData path = endNode;
                while (path != startNode)
                {
                    returnPath.Add(path.NodeIndex);
                    path = path.FromNode;
                }
                returnPath.Reverse();
                return returnPath;
            }

            foreach (int index in currentCell.GetNeighboursList())
            {
                if (!worldNavNodeList[index].Walkable || closedPath.Contains(worldNavNodeList[index]))
                {
                    continue;
                }

                int newMoveCost = currentCell.GCost + getDistance(currentCell, worldNavNodeList[index]);

                if (worldNavNodeList[index].GCost < newMoveCost || !closedPath.Contains(worldNavNodeList[index]))
                {
                    var neighbour = worldNavNodeList[index];
                    neighbour.GCost = newMoveCost;
                    neighbour.HCost = getDistance(worldNavNodeList[index], endNode);
                    neighbour.FromNode = currentCell;

                    if (!openPaths.Contains(neighbour))
                    {
                        openPaths.Add(neighbour);
                    }
                }
            }
        }

        return new List<int>();
    }

    // Src: http://theory.stanford.edu/~amitp/GameProgramming/Heuristics.html
    // infact src for everything done in this class based off psudocode...
    private int getDistance(NavNodeData start, NavNodeData end)
    {
        int dX = Mathf.Abs(start.PosX - end.PosX);
        int dY = Mathf.Abs(start.PosY - end.PosY);

        if (dX > dY)
        {
            return 14 * dY + 10 * (dX - dY);
        }
        return 14 * dX + 10 * (dY - dX);
    }
}
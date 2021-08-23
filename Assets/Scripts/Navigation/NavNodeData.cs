using System.Collections.Generic;

public class NavNodeData
{

    private int worldWidth, worldHeight;

    public int PosX { get; private set; }
    public int PosY { get; private set; }
    public int HCost { get; set; }
    public int GCost { get; set; }

    public NavNodeData FromNode;

    public bool Walkable
    {
        get; private set;
    }

    public int FCost
    {
        get => HCost + GCost;
    }

    public int NodeIndex
    {
        get => PosX * worldWidth + PosY;
    }

    public NavNodeData(int posx, int posy, int width, int height, bool walkable)
    {
        PosX = posx;
        PosY = posy;
        worldWidth = width;
        worldHeight = height;
        HCost = 0;
        GCost = 0;
        Walkable = walkable;
    }

    public int[] GetNeighboursList()
    {
        List<int> neighbours = new List<int>();

        for (int i = -1; i <= 1; i++)
        {
            for (int j = -1; j <= 1; j++)
            {
                if (i == 0 && j == 0) continue;

                int cX = PosX + i;
                int cY = PosY + j;

                if (cX >= 0 && cX < worldWidth && cY >= 0 && cY < worldHeight)
                {
                    neighbours.Add(cX * worldWidth + cY);
                }
            }
        }

        return neighbours.ToArray();
    }
}
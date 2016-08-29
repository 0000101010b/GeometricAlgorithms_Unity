using UnityEngine;
using System.Collections;

public class CellurAutomata : MonoBehaviour {


    public int width;
    public int height;
    public int numberOfSteps;
    public float chanceToStartAlive;
    public int deathLimit;
    public int birthLimit;

    bool[,] cellmap;

    void OnDrawGizmos()
    {
        for(int i=0;i<width;i++)
        {
            for (int j = 0; j < height; j++)
            {
                if (cellmap[i, j])
                    Gizmos.DrawCube(new Vector2(i, j), new Vector3(1, 1, 1));
            }

        }
    }


    // Use this for initialization
    void Start() {
        cellmap = generateMap();


    }
    public bool[,] generateMap()
    {
        cellmap = new bool[width, height];
        initMap(cellmap);
        //And now run the simulation for a set number of steps
        for (int i = 0; i < numberOfSteps; i++)
        {
            cellmap = Step(cellmap);
        }
        return cellmap;
    }

    public bool[,] initMap(bool[,] map)
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (Random.Range(0.0f,1.0f) < chanceToStartAlive)
                {
                    map[x,y] = true;
                }
            }
        }
        return map;
    }

    public bool[,] Step(bool[,] oldMap)
    {
        bool[,] newMap = new bool[width, height];
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                int nbs = countAliveNeighbours(oldMap, x, y);
                //The new value is based on our simulation rules
                //First, if a cell is alive but has too few neighbours, kill it.
                if (oldMap[x,y])
                {
                    if (nbs < deathLimit)
                    {
                        newMap[x,y] = false;
                    }
                    else
                    {
                        newMap[x,y] = true;
                    }
                } //Otherwise, if the cell is dead now, check if it has the right number of neighbours to be 'born'
                else
                {
                    if (nbs > birthLimit)
                    {
                        newMap[x,y] = true;
                    }
                    else
                    {
                        newMap[x,y] = false;
                    }
                }
            }
        }
        return newMap;

    }

    public int countAliveNeighbours(bool[,] map, int x, int y)
    {
        int count = 0;
        for (int i = -1; i < 2; i++)
        {
            for (int j = -1; j < 2; j++)
            {
                int neighbour_x = x + i;
                int neighbour_y = y + j;
                //If we're looking at the middle point
                if (i == 0 && j == 0)
                {
                    //Do nothing, we don't want to add ourselves in!
                }
                //In case the index we're looking at it off the edge of the map
                else if (neighbour_x < 0 || neighbour_y < 0 || neighbour_x >= width || neighbour_y >= height)
                {
                    count = count + 1;
                }
                //Otherwise, a normal check of the neighbour
                else if (map[neighbour_x,neighbour_y])
                {
                    count = count + 1;
                }
            }
        }
        return count;
    }
}

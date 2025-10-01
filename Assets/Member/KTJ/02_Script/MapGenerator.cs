using UnityEngine;
using System.Collections.Generic;

public class MapGenerator : MonoBehaviour
{
    [Header("Map Size")]
    public int mapWidth = 100;
    public int mapHeight = 100;

    [Header("Road")]
    public int minRoadGap = 10;
    public int maxRoadGap = 10;
    public int roadWidth = 2;

    private int[,] mapData;

    private void Start()
    {
        mapData = new int[mapWidth, mapHeight];
        GenerateRoads();
        FillBlocks();

        PrintMapToConsole();
    }

    private void PrintMapToConsole()
    {

    }

    private void FillBlocks()
    {
    }

    private void GenerateRoads()
    {
        int xPos = 0; // °¡·Î
        while (xPos < mapWidth)
        {
            for (int dx = 0; dx < roadWidth && xPos + dx < mapWidth; dx++)
            {
                for (int y = 0; y < mapHeight; y++)
                {
                    mapData[xPos + dx, y] = 1;
                }
            }
            xPos += Random.Range(minRoadGap, maxRoadGap);
        }

        int yPos = 0;
        while (yPos < mapHeight)
        {
            for (int dy = 0; dy < roadWidth && yPos + dy < mapHeight; dy++)
            {
                for (int x = 0; x < mapWidth; x++)
                {
                    mapData[x, yPos + dy] = 1;
                }
            }

            yPos += Random.Range(minRoadGap, maxRoadGap);
        }

    }
}

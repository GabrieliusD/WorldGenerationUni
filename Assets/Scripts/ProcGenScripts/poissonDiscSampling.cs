﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class poissonDiscSampling
{
    public static List<Vector2> GeneratePoints(float radius, Vector2 sampleRegionSize, int numSamples = 30)
    {
        Random.InitState(123);
        float cellSize = radius / Mathf.Sqrt(2);

        int[,] grid = new int[Mathf.CeilToInt(sampleRegionSize.x / cellSize), Mathf.CeilToInt(sampleRegionSize.y / cellSize)];
        List<Vector2> points = new List<Vector2>();
        List<Vector2> spawnPoints = new List<Vector2>();

        spawnPoints.Add(sampleRegionSize / 2);
        while(spawnPoints.Count > 0)
        {
            
            int spawnIndex = Random.Range(0, spawnPoints.Count);
            Vector2 spawnCentre = spawnPoints[spawnIndex];
            bool candidateAccepter = false;
            for (int i = 0; i < numSamples; i++)
            {
                float angle = Random.value * Mathf.PI * 2;
                Vector2 dir = new Vector2(Mathf.Sin(angle), Mathf.Cos(angle));
                Vector2 candidate = spawnCentre + dir * Random.Range(radius, 2 * radius);

                if (isValid(candidate,sampleRegionSize,cellSize,radius,points,grid))
                {

                    points.Add(candidate);           
                    spawnPoints.Add(candidate);
                    grid[(int)(candidate.x / cellSize), (int)(candidate.y / cellSize)] = points.Count;
                    candidateAccepter = true;
                    break;
                }
            }
            if(!candidateAccepter)
            {
                spawnPoints.RemoveAt(spawnIndex);
            }
        }
        return points;
    }

    static bool isValid(Vector2 candidate, Vector2 sampleRegionSize, float cellSize, float radius,List<Vector2> points, int[,] grid)
    {
        if(candidate.x >= 0 && candidate.x < sampleRegionSize.x && candidate.y >= 0 && candidate.y < sampleRegionSize.y)
        {
            int cellX = (int)(candidate.x / cellSize);
            int cellY = (int)(candidate.y / cellSize);

            int topX = Mathf.Max(0,cellX - 2);
            int EndX = Mathf.Min(cellX + 2, grid.GetLength(0) - 1);

            int topY = Mathf.Max(0, cellY - 2);
            int EndY = Mathf.Min(cellY + 2, grid.GetLength(1) - 1);


            for(int x = topX; x <= EndX; x++)
            {
                for (int y = topY; y <= EndY; y++)
                {
                    int pointIndex = grid[x, y] - 1;
                    if(pointIndex != -1)
                    {
                        float dst = (candidate - points[pointIndex]).sqrMagnitude;
                        if(dst < radius*radius)
                        {
                            return false;
                        }
                    }
                }
            }

            

            return true;
        }
        return false;
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoiseGenerator
{
    static FastNoiseLite noiseLite;
     
    // Start is called before the first frame update
    public static void Init()
    {
        noiseLite = new FastNoiseLite();
    }

    public static float[,] GenerateNoise(SettingsVariation noiseSettings, int seed, Vector2 offset)
    {
        Init();
        float halfX = noiseSettings.mapChunkSize-1;
        float halfY = noiseSettings.mapChunkSize-1;
        noiseLite.SetSeed(seed);
        noiseLite.SetNoiseType(noiseSettings.noiseType);
        noiseLite.SetFractalType(noiseSettings.fractalType);
        noiseLite.SetFractalLacunarity(noiseSettings.lacunarity);
        noiseLite.SetFractalOctaves(noiseSettings.octives);
        float[,] map = new float[noiseSettings.mapChunkSize, noiseSettings.mapChunkSize];
        for (int x = 0; x < noiseSettings.mapChunkSize; x++)
        {
            for (int y = 0; y < noiseSettings.mapChunkSize; y++)
            {
                float sampleX = (float)(x-halfX) / noiseSettings.scale + offset.x;
                float sampleY = (float)(y-halfY) / noiseSettings.scale + offset.y;
                map[x, y] = (noiseLite.GetNoise((float)(x+offset.x) / noiseSettings.scale, (float)(y -offset.y) / noiseSettings.scale) + 1) * 0.5f;
            }
        }



        return map;  
    }

}

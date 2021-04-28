using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MapType { None, Island, SquareIsland};

[System.Serializable]
[CreateAssetMenu()]
public class NoiseSettings : ScriptableObject
{
    public string terrainName;
    public float maxHeight;
    public int seed;
    public int mapChunkSize;

    public MapType mapType;
    public bool hasWater;

    public SettingsVariation[] SettingsVariations;
}
public enum NoiseMode { Add, Subtract, Multiply };
[System.Serializable]
public struct SettingsVariation
{
    public NoiseMode noiseMode;
    public int mapChunkSize;
    public bool enabled;
    public bool useLayerAsMask;
    public bool InverseLayer;
    public float sampleBelow;
    public float scale;
    public float lacunarity;
    public int octives;
    public float strength;

    public float minValue;
    public FastNoiseLite.NoiseType noiseType;
    public FastNoiseLite.FractalType fractalType;
}

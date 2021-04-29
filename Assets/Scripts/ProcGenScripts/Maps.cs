using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Maps : MonoBehaviour
{
    public ColorSettings colorSettings;
    public ColorGen colorGen = new ColorGen();
    


    public NoiseSettings noiseSettings;
    [Range(0,6)]
    public int levelOfDetail;
    public int mapChunkSize = 241;
    public int seed = 1337;
    public bool autoUpdate;
    public MinMaxTracker heightTracker;
    private float[,] finalMap;
    public float maskThreshold = 10.0f;

    private void Start()
    {
        //if(WorldSettings.Instance.getSeed() != null)
        seed = WorldSettings.Instance.getSeed();
        NoiseGenerator.Init();
        noiseSettings = WorldSettings.Instance.GetNoiseSettings();
        CreateIslandMask();
    }
    public void CreateIslandMask()
    {
        if (noiseSettings.mapType == MapType.Island)
            GradientMap.GenerateIslandMask((mapChunkSize - 1) * 3, (mapChunkSize - 1) * 3, maskThreshold);
        if (noiseSettings.mapType == MapType.SquareIsland)
            GradientMap.GenerateSquareIslandMask((mapChunkSize - 1) * 3, (mapChunkSize - 1) * 3, maskThreshold);
        
    }

    public MeshMatData GetGeneratedData(Vector2 centre, Vector2 coord)
    {
        heightTracker = new MinMaxTracker();

        float[,] map = new float[noiseSettings.mapChunkSize, noiseSettings.mapChunkSize];
        for (int i = 0; i < noiseSettings.SettingsVariations.Length; i++)
        {
            float[,] firstLayerValue = new float[noiseSettings.mapChunkSize, noiseSettings.mapChunkSize]; 
            if(noiseSettings.SettingsVariations.Length > 0)
            {
                firstLayerValue = NoiseGenerator.GenerateNoise(noiseSettings.SettingsVariations[0], seed, centre);
            }

            SettingsVariation sv = noiseSettings.SettingsVariations[i];
            if(sv.enabled)
            {
                float[,] newMap = NoiseGenerator.GenerateNoise(sv, seed, centre);
                for (int y = 0; y < newMap.GetLength(0); y++)
                {
                    for (int x = 0; x < newMap.GetLength(1); x++)
                    {
                        if (sv.noiseMode == NoiseMode.Add)
                        {
                            float mask = 1;

                            if(sv.useLayerAsMask)
                            {
                                mask = firstLayerValue[x,y];
                                if(sv.InverseLayer)
                                {
                                    mask = 1 - mask;
                                    if(mask <= sv.sampleBelow)
                                    {
                                        mask = 0;
                                    } 
                                }else{
                                    if (mask <= sv.sampleBelow)
                                    {
                                        mask = 0;
                                    }
                                }
                            }
                            newMap[x, y] = Mathf.Max(0, newMap[x,y] - sv.minValue);
                            map[x, y] += newMap[x, y] * sv.strength *mask;

                        }
                        else 
                        if (sv.noiseMode == NoiseMode.Multiply)
                        {
                            map[x, y] *= newMap[x, y];
                        } else 
                        if (sv.noiseMode == NoiseMode.Subtract)
                        {
                            float mask = 1;

                            if (sv.useLayerAsMask)
                            {
                                mask = firstLayerValue[x, y];
                                if (sv.InverseLayer)
                                {
                                    mask = 1 - mask;
                                    if (mask <= sv.sampleBelow)
                                    {
                                        mask = 0;
                                    }
                                }
                            }
                            newMap[x, y] += Mathf.Min(0,-newMap[x, y] * sv.minValue);
                            map[x, y] += newMap[x, y] * sv.strength * mask;

                        }
                    }
                }
            }
        }

        Debug.Log(centre);


        for (int x = 0; x < map.GetLength(0); x++)
        {
            for (int y = 0; y < map.GetLength(1); y++)
            {
                heightTracker.AddValue(map[x, y]);

            }
        }

        if(noiseSettings.mapType != MapType.None)
        for (int x = 0; x < map.GetLength(0) ; x++)
        {
            for (int y = 0; y < map.GetLength(1); y++)
            {
                float m1 = (map.GetLength(0)-1)*3f;
                float m2 = (map.GetLength(1)-1)*3f;
                float pointX = ((float)x+(centre.x)) /m1;
                float pointY = ((float)y+(-centre.y)) / m2;


                map[x,y] = map[x,y] * GradientMap.sampleGradient(pointY,pointX);
            }
        }

        //SmoothenTerrain(map,1,1);
        map = SmoothenTerrain(map);


        colorGen.UpdateSettings(colorSettings);
        colorGen.UpdateHeight(heightTracker);
        colorGen.UpdateColors();
        return new MeshMatData(map,colorSettings.terrainMaterial);
    }
    public float[,] GetMapData()
    {
        return finalMap;
    }
     private void OnValidate() {
        
    }

    public float[,] SmoothenTerrain(float [,] map)
    {
        float[,] newMap = new float[map.GetLength(0),map.GetLength(1)];
        for (int x = 0; x < map.GetLength(0); x++)
        {
            for (int y = 0; y < map.GetLength(1); y++)
            {
                //0,1,2    (x-1,y-1),(x,y-1), (x+1,y-1)
                //3,4.5     (x-1,y),(x,y),(x+1,y)
                //6,7,8     (x-1,y+1),(x,y+1),(x+1,y+1)
                if(x > 1 && x < map.GetLength(0)-1 && y > 1 && y < map.GetLength(1)-1)
                {
                    float total =   map[x-1,y-1] + map[x,y-1] + map[x+1,y-1] +
                                    map[x-1,y] + map[x,y] + map[x+1,y] +
                                    map[x-1,y+1] + map[x, y+1] + map[x+1,y+1];
                    float average = total / 9.0f;

                    newMap[x,y] = average;
                } else newMap[x,y] = map[x,y];
            }
        }

        return newMap;
    }

    public void SmoothenTerrain(float[,] map, int filterSize, int iterations)
    {
        int count = 0;
        float total = 0;
        int width = map.GetLength(0);
        int height = map.GetLength(1);


        for(int x = 0; x < width-1; x++)
        {
            for(int y = 0; y < height-1; y++)
            {
                count = 0;
                total = 0;

                for (int x0 = x-filterSize; x0 <=x+filterSize; x0++)
                {
                    if(x0 < 0 || x0 > width-1)
                    {
                        continue;
                    }
                    for(int y0 = y-filterSize; y0<=y+filterSize; y0++)
                    {
                        if(y0 < 0 || y0 > height-1)
                        {
                            continue;
                        }

                        total +=map[x0,y0];
                        count++;
                    }
                }
                if(count >= 9 && total != 0)
                    map[x,y] = total / (float)count;
            }
        }
    }



}
    public struct MeshMatData
    {
        public readonly float[,] map;
        public readonly Material mat;

        public MeshMatData(float[,] heightMap, Material material)
        {
            map = heightMap;
            mat = material;
        }
    }
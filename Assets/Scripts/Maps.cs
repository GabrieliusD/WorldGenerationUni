using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public struct NoiseSettings
{

    public enum NoiseMode { Add, Subtract, Multiply};
    public NoiseMode noiseMode;
    public int mapChunkSize;

    public float scale;
    public float lacunarity;
    public int octives;
    public float strength;
    public FastNoiseLite.NoiseType noiseType;
    public FastNoiseLite.FractalType fractalType;
}
public class Maps : MonoBehaviour
{
    public ColorSettings colorSettings;
    public ColorGen colorGen = new ColorGen();
    

    [SerializeField]
    NoiseSettings[] noiseSettings;
    [Range(0,6)]
    public int levelOfDetail;
    public int mapChunkSize = 241;
    public int seed = 1337;
    public bool autoUpdate;
    public float minValue;
    public MinMaxTracker heightTracker;

    private float[,] finalMap;

    public float maskThreshold = 10.0f;
    private void Start()
    {
        seed = WorldSettings.Instance.getSeed();
        NoiseGenerator.Init();
        if(WorldSettings.Instance.GetMapType() == "Island")
        GradientMap.GenerateIslandMask((mapChunkSize-1)*3, (mapChunkSize-1)*3, maskThreshold);
        if(WorldSettings.Instance.GetMapType() == "Island v2")
        GradientMap.GenerateSquareIslandMask((mapChunkSize-1)*3, (mapChunkSize-1)*3, maskThreshold);
        //GenerateMap();
    }
    public void GenerateMap()
    {
        heightTracker = new MinMaxTracker();
        float[,] map = new float[noiseSettings[0].mapChunkSize, noiseSettings[0].mapChunkSize];
        for (int i = 0; i < noiseSettings.Length; i++)
        {
            float[,] newMap = NoiseGenerator.GenerateNoise(noiseSettings[i], seed, Vector2.zero);
            for (int x = 0; x < newMap.GetLength(0); x++)
            {
                for (int y = 0; y < newMap.GetLength(1); y++)
                {
                    if (noiseSettings[i].noiseMode == NoiseSettings.NoiseMode.Add)
                    {
                        map[x, y] += (newMap[x, y] * noiseSettings[i].strength);
                        map[x, y] = Mathf.Max(0, map[x, y] * noiseSettings[i].strength - minValue);

                    }
                    else if (noiseSettings[i].noiseMode == NoiseSettings.NoiseMode.Multiply)
                    {
                        map[x, y] *= (newMap[x, y] * noiseSettings[i].strength);
                    } else if (noiseSettings[i].noiseMode == NoiseSettings.NoiseMode.Subtract && map[x, y] == 0)
                    {
                        map[x, y] += Mathf.Min(0,-newMap[x, y] * noiseSettings[i].strength);
                    }
                }
            }
        }
        //finalMap = new int[map.GetLength(0), map.GetLength(1)];
        finalMap = map;
        for (int x = 0; x < map.GetLength(0); x++)
        {
            for (int y = 0; y < map.GetLength(1); y++)
            {
                heightTracker.AddValue(map[x, y]);

            }
        }




        Display display = FindObjectOfType<Display>();
        display.DrawNoiseMap(map);

        display.DrawMesh(MeshGen.GenerateTerrainMesh(map,levelOfDetail),heightTracker,colorSettings);

    }

    public MeshMatData GetGeneratedData(Vector2 centre, Vector2 coord)
    {
        heightTracker = new MinMaxTracker();

        float[,] map = new float[noiseSettings[0].mapChunkSize, noiseSettings[0].mapChunkSize];
        for (int i = 0; i < noiseSettings.Length; i++)
        {
            float[,] newMap = NoiseGenerator.GenerateNoise(noiseSettings[i], seed, centre);
            for (int y = 0; y < newMap.GetLength(0); y++)
            {
                for (int x = 0; x < newMap.GetLength(1); x++)
                {
                    if (noiseSettings[i].noiseMode == NoiseSettings.NoiseMode.Add)
                    {
                        map[x, y] += (newMap[x, y] * noiseSettings[i].strength);
                        map[x, y] = Mathf.Max(0, map[x, y] * noiseSettings[i].strength - minValue);

                    }
                    else if (noiseSettings[i].noiseMode == NoiseSettings.NoiseMode.Multiply)
                    {
                        map[x, y] *= (newMap[x, y] * noiseSettings[i].strength);
                    } else if (noiseSettings[i].noiseMode == NoiseSettings.NoiseMode.Subtract && map[x, y] == 0)
                    {
                        map[x, y] += Mathf.Min(0,-newMap[x, y] * noiseSettings[i].strength);
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
        if(WorldSettings.Instance.GetMapType() != "Continues")
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
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Display : MonoBehaviour
{
    public Renderer textureRen;
    public MeshFilter meshFilter;
    public MeshRenderer meshRenderer;
    public ColorGen colorGen = new ColorGen();
    public void Awake()
    {
        meshFilter.gameObject.AddComponent<MeshCollider>();

    }
    public void DrawNoiseMap(float[,] noise)
    {
        int width = noise.GetLength(0);
        int height = noise.GetLength(1);

        Texture2D texture = new Texture2D(width, height);

        Color[] color = new Color[width * height];

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                color[y * width + x] = Color.Lerp(Color.black, Color.white, noise[x,y]);
            }
        }
        texture.SetPixels(color);
        texture.Apply();

        textureRen.sharedMaterial.mainTexture = texture;
        textureRen.transform.localScale = new Vector3(width, 1, height);
    }

    public void DrawMesh(MeshData meshData, MinMaxTracker heightTracker, ColorSettings colorSettings)
    {
        colorGen.UpdateSettings(colorSettings);
        colorGen.UpdateHeight(heightTracker);
        colorGen.UpdateColors();

        meshFilter.sharedMesh = meshData.CreateMesh();
        meshFilter.GetComponent<MeshRenderer>().sharedMaterial = colorSettings.terrainMaterial;
    }
}

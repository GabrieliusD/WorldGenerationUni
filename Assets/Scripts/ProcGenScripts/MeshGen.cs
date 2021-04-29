using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MeshGen 
{
    public static MeshData GenerateTerrainMesh(float[,] map, int levelOfDetail)
    {
        int width = map.GetLength(0);
        int height = map.GetLength(1);

        float topLeftX = (width - 1) / -2f;
        float topLeftZ = (height - 1) / 2f;

        int increment = (levelOfDetail == 0)?1:levelOfDetail *2;
        int verticesPerLine = (width-1)/increment+1;
        MeshData mesh = new MeshData(verticesPerLine, verticesPerLine);
        int vertexIndex = 0;
        for (int y = 0; y < height; y+=increment)
        {
            for (int x = 0; x < width; x+=increment)
            {
                mesh.vertices[vertexIndex] = new Vector3(topLeftX + x, (map[x, y]), topLeftZ - y);
                mesh.uvs[vertexIndex] = new Vector2(x / (float)width, y / (float)height);
                if(x<width-1 && y < height - 1)
                {
                    mesh.Addtriangle(vertexIndex, vertexIndex + verticesPerLine + 1, vertexIndex + verticesPerLine);
                    mesh.Addtriangle(vertexIndex + verticesPerLine + 1, vertexIndex, vertexIndex + 1);
                }
                vertexIndex++;
            }
        }
        return mesh;
    }

    
}

public class MeshData
{
    public Vector3[] vertices;
    public int[] triangles;
    public Vector2[] uvs;
    int triangleIndex;
    public MeshData(int meshWidth, int meshHeight)
    {
        vertices = new Vector3[meshWidth * meshHeight];
        uvs = new Vector2[meshWidth * meshHeight];
        triangles = new int[(meshWidth - 1) * (meshHeight - 1) * 6];
    }

    public void Addtriangle(int a, int b, int c)
    {
        triangles[triangleIndex] = a;
        triangles[triangleIndex + 1] = b;
        triangles[triangleIndex + 2] = c;
        triangleIndex += 3;
    }

    public Mesh CreateMesh()
    {
        Mesh mesh = new Mesh();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.uv = uvs;
        mesh.RecalculateNormals();
        return mesh;
    }
}

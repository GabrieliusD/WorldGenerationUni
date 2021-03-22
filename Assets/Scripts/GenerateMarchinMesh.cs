using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateMarchinMesh : MonoBehaviour
{
    // Start is called before the first frame update
    public Vector3 marchinCubeSize;
    public float scale;
    MarchingCubes mc = new MarchingCubes();
    [SerializeField]
    public GridCell grid;
    public float isoLevel = 5;
    public MeshFilter meshFilter;

    Triangles[] triangles;
    void Start()
    {
        List<Vector3> verticesList = new List<Vector3>();
        Vector3[] vertices = new Vector3[30];
        Mesh mesh = new Mesh();
        FastNoiseLite noise  = new FastNoiseLite();
        grid.p[0] = new Vector3(0, 0, 0);
        grid.p[1] = new Vector3(1, 0, 0);
        grid.p[2] = new Vector3(1, 1, 0);
        grid.p[3] = new Vector3(0, 1, 0);
        grid.p[4] = new Vector3(0, 1, 1);
        grid.p[5] = new Vector3(1, 1, 1);
        grid.p[6] = new Vector3(1, 0, 1);
        grid.p[7] = new Vector3(0, 0, 1);

        int counter = 0;
        int numVert = 0;
        int indiceCounter = 0;
        for (int x = 0; x < marchinCubeSize.x; x++)
        {
            for (int y = 0; y < marchinCubeSize.y; y++)
            {
                for (int z = 0; z < marchinCubeSize.z; z++)
                {

                    //grid.p[0] = new Vector3(x, y, z);
                    //grid.p[1] = new Vector3(x+1, y, z);
                    //grid.p[2] = new Vector3(x+1,y+1, z);
                    //grid.p[3] = new Vector3(x, y+1, z);
                    //grid.p[4] = new Vector3(x, y+1, z+1);
                    //grid.p[5] = new Vector3(x+1, y+1, z+1);
                    //grid.p[6] = new Vector3(x+1, y, z+1);
                    //grid.p[7] = new Vector3(x, y, z+1);

                    grid.p[0] = new Vector3(x, y, z);
                    grid.p[1] = new Vector3(x + 1, y, z);
                    grid.p[2] = new Vector3(x + 1, y, z+1);
                    grid.p[3] = new Vector3(x, y,z+1);
                    grid.p[4] = new Vector3(x, y + 1, z);
                    grid.p[5] = new Vector3(x + 1, y + 1, z);
                    grid.p[6] = new Vector3(x + 1, y+1, z + 1);
                    grid.p[7] = new Vector3(x, y+1, z + 1);

                    for (int i = 0; i < grid.val.Length; i++)
                    {
                        Vector3 val = grid.p[i];
                        grid.val[i] = noise.GetNoise(val.x/scale, val.y/scale, val.z/scale);

                    }
                    if(y == marchinCubeSize.y-1)
                    {
                        //grid.val[0]=-1;
                        //grid.val[1]=-1;
                        //grid.val[2]=-1;
                        //grid.val[3]=-1;
                        grid.val[4]=5;
                        grid.val[5]=5;
                        grid.val[6]=5;
                        grid.val[7]=5;

                    }


                    List<Triangles> numOfTriangles = mc.polygonise(grid, isoLevel);
                    if(numOfTriangles !=null)
                    for (int i = 0; i != numOfTriangles.Count; i++)
                    {
                            Vector3 p0 = numOfTriangles[i].p[0];
                            Vector3 p1 = numOfTriangles[i].p[1];
                            Vector3 p2 = numOfTriangles[i].p[2];
                            verticesList.Add(p0); verticesList.Add(p1); verticesList.Add(p2);

                            //vertices[numVert] = p0;
                            //vertices[numVert+1] = p1;
                            //vertices[numVert+2] = p2;
                            indiceCounter += 3;

                            numVert += 3;
                            Debug.Log(counter + " in " + p0);
                            Debug.Log(counter + " in " + p1);
                            Debug.Log(counter + " in " + p2);

                        }
                    counter++;
                }
            }
        }
        
        int[] indices = new int[verticesList.Count];
        for (int i = 0; i < verticesList.Count; i++)
        {
            indices[i] = i;
        }
        Debug.Log(vertices.Length);
        mesh.indexFormat = UnityEngine.Rendering.IndexFormat.UInt32;
        mesh.vertices = verticesList.ToArray();
        mesh.triangles = indices;
        mesh.RecalculateNormals();
        Debug.Log("Triangles: "+ indiceCounter);
        meshFilter.sharedMesh = mesh;
        //meshFilter.GetComponent<MeshRenderer>().sharedMaterial = new Material(Shader.Find("Diffuse"));


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

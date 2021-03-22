using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateSpawn
{
    bool pathFound = false;
    Vector3 playerLoc;
    Vector3 enemyLoc;
    public void Create(Dictionary<Vector2,TerrainChunk> terrainChunkDictionary, int numVisible, int chunkSize)
    {
        int sizeX = 30;
        int sizeY = 30;
        Rect rect = new Rect();
        float waterLevel = WaterLevel.Instance.GetWaterLevel();
        for(int yOffset = -numVisible; yOffset <= numVisible; yOffset++)
        {
            for(int xOffset = -numVisible; xOffset <= numVisible; xOffset++)
            {
                TerrainChunk terrainChunk = terrainChunkDictionary[new Vector2(xOffset, yOffset)];
                Mesh mesh = terrainChunk.GetMesh();
                List<Vector3> vertices = new List<Vector3>();
                mesh.GetVertices(vertices);

                List<Vector3> walkableVertices = new List<Vector3>();
                foreach (var v in vertices)
                {
                    if(v.y > waterLevel && v.y < 20)
                    {
                        Vector3 pos = new Vector3(xOffset * chunkSize + v.x, v.y, yOffset*chunkSize + v.z);
                        Vector3 up = Vector3.up * 20 + pos;
                        walkableVertices.Add(pos);
                        //Debug.DrawLine(pos, up, Color.red, Mathf.Infinity);
                        
                    }
                }
                rect = new Rect(walkableVertices[0].x, walkableVertices[0].z, sizeX, sizeY);
                int walkableCounter = 0;
                foreach (var walkable in walkableVertices)
                    {
                        if(rect.Contains(new Vector2(walkable.x, walkable.z)))
                        {
                            walkableCounter++;
                        }
                    }
                Debug.Log("coord: " + yOffset + ", " + xOffset + "Contains: " + walkableCounter );
                
            }
        }
    }
    List<Vector3> FindVerticesInRange(List<Vector3> vertices)
    {
        List<Vector3> walkableVertices = new List<Vector3>();
        float waterLevel = WaterLevel.Instance.GetWaterLevel();
            foreach (var v in vertices)
                {
                    if(v.y > waterLevel && v.y < 15)
                    {

                        walkableVertices.Add(v);
                        //Debug.DrawLine(pos, up, Color.red, Mathf.Infinity);
                        
                    }
                }

                return walkableVertices;
    }
    public void SpawnRandomAndFindPath(Dictionary<Vector2,TerrainChunk> terrainChunkDictionary, int numVisible, int chunkSize)
    {
        for(int yOffset = -numVisible; yOffset <= numVisible; yOffset++)
        {
            int xOffset = -2;
            Vector3 pos = new Vector3(xOffset * chunkSize, 0,yOffset * chunkSize);
            Debug.DrawLine(pos,pos + Vector3.up * 30,Color.red, Mathf.Infinity);
            TerrainChunk tc1 = terrainChunkDictionary[new Vector2(xOffset,yOffset)];
            for(int yOffset2 = -numVisible; yOffset2 <= numVisible; yOffset2++)
            {
                int xOffset2 = 2;
                Vector3 pos2 = new Vector3(xOffset2 * chunkSize, 0,yOffset2 * chunkSize);
                Debug.DrawLine(pos2,pos2 + Vector3.up * 30,Color.red, Mathf.Infinity);
                TerrainChunk tc2 = terrainChunkDictionary[new Vector2(xOffset2,yOffset2)];
                List<Vector3> v1 = new List<Vector3>();
                List<Vector3> v2 = new List<Vector3>();
                tc1.GetMesh().GetVertices(v1);
                tc2.GetMesh().GetVertices(v2);

                v1 = FindVerticesInRange(v1);
                v2 = FindVerticesInRange(v2);
                
                int r1 = Random.Range(0, v1.Count);
                int r2 = Random.Range(0, v2.Count);
                if(v1.Count == 0 || v2.Count == 0) continue;
                //Vector3 pos = new Vector3(xOffset * chunkSize + v.x, v.y, yOffset*chunkSize + v.z);
                Vector3 worldPos1 = new Vector3(xOffset*chunkSize + v1[r1].x, v1[r1].y, yOffset*chunkSize + v1[r1].z);
                Vector3 worldPos2 = new Vector3(xOffset2*chunkSize + v2[r2].x, v2[r2].y, yOffset2*chunkSize + v2[r2].z);
                //GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                //GameObject.Instantiate(cube, worldPos1, Quaternion.identity);
                //GameObject.Instantiate(cube, worldPos2, Quaternion.identity);
                checkIfPathExists(worldPos1,worldPos2);

                if(pathFound)
                {
                    playerLoc = worldPos1;
                    enemyLoc = worldPos2;
                    Debug.Log("Path found between" + worldPos1 + " and " + worldPos2);
                    return;
                }
            }
        }
    }

    public void checkIfPathExists(Vector3 pos1, Vector3 pos2)
    {
        pathFound = PathRequest.RequestNonThreadedPath(pos1,pos2);
    }

    public void playerSpawn(GameObject townHall)
    {
        
        GameObject.Instantiate(townHall, playerLoc, Quaternion.identity);
        Camera.main.transform.position = new Vector3(playerLoc.x, playerLoc.y + 20.0f, playerLoc.z - 20.0f);

    }
    public void enemySpawn(GameObject townHall)
    {
        GameObject.Instantiate(townHall, enemyLoc, Quaternion.identity);

    }

}

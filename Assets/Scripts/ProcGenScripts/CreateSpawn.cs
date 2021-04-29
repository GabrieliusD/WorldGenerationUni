using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateSpawn
{
    bool pathFound = false;
    Vector3 playerLoc;
    Vector3 enemyLoc;
    Grid grid;
    public CreateSpawn()
    {
        grid = GameObject.FindObjectOfType<Grid>();
    }
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
    List<Vector3> FindVerticesInRange(List<Vector3> vertices, float maxHeight)
    {
        List<Vector3> walkableVertices = new List<Vector3>();
        float waterLevel = -5;
        if(WorldSettings.Instance.GetNoiseSettings().hasWater)
            waterLevel = WaterLevel.Instance.GetWaterLevel();
            foreach (var v in vertices)
                {
                    if(v.y > waterLevel+1 && v.y < maxHeight)
                    {

                        walkableVertices.Add(v);
                        //Debug.DrawLine(pos, up, Color.red, Mathf.Infinity);
                        
                    }
                }

                return walkableVertices;
    }

    Vector3 FindLocationWithSpace(Mesh mesh,int xOffset, int yOffset, int chunkSize, float maxHeight, int attempts)
    {        
        System.Random random = new System.Random(1);

        for (int i = 0; i < attempts; i++)
        {
            List<Vector3> v = new List<Vector3>();
            mesh.GetVertices(v);
            v = FindVerticesInRange(v, maxHeight);
            int r = random.Next(0, v.Count);

            Vector3 worldPos = new Vector3(xOffset * chunkSize + v[r].x, v[r].y, yOffset * chunkSize + v[r].z);

            if(grid.checkNodesAreEmpty(worldPos,8)) return worldPos; 
            
        }

        return Vector3.zero;

    }
    public bool SpawnRandomAndFindPath(Dictionary<Vector2,TerrainChunk> terrainChunkDictionary, int numVisible, int chunkSize, float maxHeight)
    {
        numVisible = 3;
        System.Random random = new System.Random(1);
        for(int yOffset = 0; yOffset > -numVisible; yOffset--)
        {
            int xOffset = 0;
            Vector3 pos = new Vector3(xOffset * chunkSize, 0,yOffset * chunkSize);
            Debug.DrawLine(pos,pos + Vector3.up * 30,Color.red, Mathf.Infinity);
            TerrainChunk tc1 = terrainChunkDictionary[new Vector2(xOffset,yOffset)];
            for(int yOffset2 = -2; yOffset2 < 0; yOffset2++)
            {
                int xOffset2 = 2;
                Vector3 pos2 = new Vector3(xOffset2 * chunkSize, 0,yOffset2 * chunkSize);
                Debug.DrawLine(pos2,pos2 + Vector3.up * 30,Color.red, Mathf.Infinity);
                TerrainChunk tc2 = terrainChunkDictionary[new Vector2(xOffset2,yOffset2)];

                Vector3 worldPos1 = FindLocationWithSpace(tc1.GetMesh(),xOffset,yOffset,chunkSize,maxHeight,10);
                Vector3 worldPos2 = FindLocationWithSpace(tc2.GetMesh(),xOffset2,yOffset2,chunkSize,maxHeight,10);
                if(worldPos1 == Vector3.zero || worldPos2 == Vector3.zero) continue;
                checkIfPathExists(worldPos1,worldPos2);

                if(pathFound)
                {
                    playerLoc = worldPos1;
                    enemyLoc = worldPos2;
                    Debug.Log("Path found between" + worldPos1 + " and " + worldPos2);
                    return true;
                }
            }
        }
        return false;
    }

    public void checkIfPathExists(Vector3 pos1, Vector3 pos2)
    {
        pathFound = PathRequest.RequestNonThreadedPath(pos1,pos2);
    }

    public GameObject playerSpawn(GameObject townHall)
    {
        
        GameObject go = GameObject.Instantiate(townHall, playerLoc, Quaternion.identity);
        Camera.main.transform.position = new Vector3(playerLoc.x, playerLoc.y + 20.0f, playerLoc.z - 20.0f);
        return go;

    }
    public GameObject enemySpawn(GameObject townHall)
    {
       return GameObject.Instantiate(townHall, enemyLoc, Quaternion.identity);

    }

}

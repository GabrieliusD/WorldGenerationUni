﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class chunks : MonoBehaviour
{
    public GameObject townHall;
    public GameObject enemyTownHall;
    public const float distance = 600;
    Grid grid;
    spawnObject spawn;
    Vector3 worldCentre = Vector3.zero;

    public static Vector2 cameraPosition;
    int chunkSize;
    int numVisible;
    public static Maps mapGen;
    Dictionary<Vector2,TerrainChunk> terrainChunkDictionary = new Dictionary<Vector2, TerrainChunk>();
    List<TerrainChunk> terrainChunksVisible = new List<TerrainChunk>();

    int num = 0;
    void Start() {
        grid = FindObjectOfType<Grid>();
        mapGen = FindObjectOfType<Maps>();
        spawn = FindObjectOfType<spawnObject>();
        chunkSize = Maps.mapChunkSize -1 ;
        numVisible = Mathf.RoundToInt(distance/chunkSize);
        cameraPosition = new Vector2(worldCentre.x, worldCentre.z);

        UpdateVisibleChunks();
        
    }

    void Update() {

    }
    void UpdateVisibleChunks()
    {
        for(int yOffset = -numVisible; yOffset <= numVisible; yOffset++)
        {
            for(int xOffset = -numVisible; xOffset <= numVisible; xOffset++)
            {
                Vector2 viewedChunkCoord = new Vector2(xOffset,yOffset);
                
                terrainChunkDictionary.Add(viewedChunkCoord, new TerrainChunk(viewedChunkCoord, chunkSize, transform));
                
            }
        }

        grid.CreateGrid();
        CreateSpawn  c = new CreateSpawn();
        c.SpawnRandomAndFindPath(terrainChunkDictionary, numVisible, chunkSize);
        c.enemySpawn(enemyTownHall);
        c.playerSpawn(townHall);
        spawn.spawnTrees();


    }
}
    public class TerrainChunk
    {
        GameObject meshObject;
        Vector2 position;
        MeshRenderer MeshRenderer;
        MeshFilter meshFilter;
        Bounds bounds;
        public TerrainChunk(Vector2 coord, int size, Transform parent ) 
        {
            position = coord * size;
            bounds = new Bounds(position, Vector2.one * size);
            Vector3 positionV3 = new Vector3(position.x, 0, position.y);
            meshObject = new GameObject("TerrainChunk");
            MeshRenderer = meshObject.AddComponent<MeshRenderer>();
            meshFilter = meshObject.AddComponent<MeshFilter>();
            meshObject.transform.position = positionV3;
            meshObject.transform.SetParent(parent);
            SetVisible(true);
            MeshMatData data = chunks.mapGen.GetGeneratedData(position);
            meshFilter.mesh = MeshGen.GenerateTerrainMesh(data.map, 1).CreateMesh();
            MeshRenderer.material = data.mat;
            meshObject.AddComponent<MeshCollider>();
        }

        public void SetVisible(bool visible)
        {
            meshObject.SetActive(visible);
        }

        public bool isVisible()
        {
            return meshObject.activeSelf;
        }

        public Mesh GetMesh()
        {
            return meshFilter.mesh;
        }
        
    }
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class chunks : MonoBehaviour
{
    public GameObject townHall;
    WorldSettings worldSettings;
    public GameObject enemyTownHall;
    public const float distance = 600;
    Grid grid;
    spawnObject spawn;
    Vector3 worldCentre = Vector3.zero;

    public static Vector2 cameraPosition;
    int chunkSize = 120;
    public int numVisible;
    public static Maps mapGen;
    Dictionary<Vector2,TerrainChunk> terrainChunkDictionary = new Dictionary<Vector2, TerrainChunk>();
    List<TerrainChunk> terrainChunksVisible = new List<TerrainChunk>();

    int num = 0;
    void Start() {
        worldSettings = WorldSettings.Instance;
        grid = FindObjectOfType<Grid>();
        mapGen = FindObjectOfType<Maps>();
        spawn = FindObjectOfType<spawnObject>();
        chunkSize = mapGen.mapChunkSize -1 ;
        //numVisible = Mathf.RoundToInt(distance/chunkSize);
        cameraPosition = new Vector2(worldCentre.x, worldCentre.z);

        UpdateVisibleChunks();
        
    }

    void Update() {

    }

    public void createChunks()
    {
        ResourceManager rm = new ResourceManager();
        rm.ConfigureResourceSettings();
        if(mapGen == null) mapGen = FindObjectOfType<Maps>();
        if(chunkSize == 0) chunkSize =121;
        if(chunkSize == 121) chunkSize = chunkSize-1;
        mapGen.CreateIslandMask();
        destroyOldChunks();
        for (int yOffset = 0; yOffset < 3; yOffset++)
        {
            for (int xOffset = 0; xOffset < 3; xOffset++)
            {
                Vector2 viewedChunkCoord = new Vector2(xOffset, -yOffset);
                terrainChunkDictionary.Add(viewedChunkCoord, new TerrainChunk(viewedChunkCoord, chunkSize, transform, gameObject.layer));
                

            }
        }
    }

    public void destroyOldChunks()
    {
        if(terrainChunkDictionary.Count > 0)
        {
            TerrainChunk[] terrain = new TerrainChunk[terrainChunkDictionary.Count];
            terrainChunkDictionary.Values.CopyTo(terrain, 0);
            foreach (TerrainChunk item in terrain)
            {
                DestroyImmediate(item.destroyObject());
            }
        }
        terrainChunkDictionary.Clear();


    }
    void UpdateVisibleChunks()
    {
        bool pathFound = false;
        CreateSpawn  c = new CreateSpawn();
        float maxHeight = mapGen.noiseSettings.maxHeight;
        WaterLevel.Instance.HasWater(WorldSettings.Instance.GetNoiseSettings().hasWater);
        int numTerrainsCreated = 0;
        while(!pathFound)
        {
            numTerrainsCreated++;
            //if(Input.GetKey(KeyCode.Escape)) break;
            createChunks();
            grid.CreateGrid(maxHeight);
            pathFound = c.SpawnRandomAndFindPath(terrainChunkDictionary, numVisible, chunkSize, maxHeight);
            if(!pathFound)
                mapGen.seed+=1;
        }
        GameObject th= c.playerSpawn(townHall);
        GameObject eth = c.enemySpawn(enemyTownHall);
        spawn.SetBuildings(th, eth);
        grid.SetNodeUnwakable(th);
        grid.SetNodeUnwakable(eth);
        spawn.spawnTrees(maxHeight);
        Debug.Log("Terrins Created: " + numTerrainsCreated);

    }
}
    public class TerrainChunk
    {
        GameObject meshObject;
        Vector2 position;
        MeshRenderer MeshRenderer;
        MeshFilter meshFilter;
        Bounds bounds;
        public TerrainChunk(Vector2 coord, int size, Transform parent, int layer) 
        {
            position = coord * size;
            bounds = new Bounds(position, Vector2.one * size);
            Vector3 positionV3 = new Vector3(position.x, 0, position.y);
            meshObject = new GameObject("TerrainChunk");
            meshObject.layer = layer;
            MeshRenderer = meshObject.AddComponent<MeshRenderer>();
            meshFilter = meshObject.AddComponent<MeshFilter>();
            meshObject.transform.position = positionV3;
            meshObject.transform.SetParent(parent);
            meshObject.tag = "Ground";
            SetVisible(true);
            MeshMatData data = chunks.mapGen.GetGeneratedData(position, coord);
            meshFilter.mesh = MeshGen.GenerateTerrainMesh(data.map, 0).CreateMesh();
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

        public GameObject destroyObject()
        {
            return meshObject;
        }
        
    }

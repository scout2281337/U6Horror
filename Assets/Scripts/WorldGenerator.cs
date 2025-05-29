using UnityEngine;

public class WorldGenerator : MonoBehaviour
{
    [SerializeField]private GameObject ChunkGenerator;
    [SerializeField] private int chunksValue;
    private int cellSize = 16;

    private void Awake()
    {
        GenerateChunks();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void GenerateChunks() 
    {
        for (int x = 0; x < chunksValue; x++) 
        {
            for (int z = 0; z < chunksValue; z++) 
            {
                Vector3 ChunkPos = new Vector3(x * cellSize, 0, z * cellSize);
                GameObject GO = Instantiate(ChunkGenerator, ChunkPos, Quaternion.identity);
                var chunk = GO.GetComponent<ChunkGenerator>();
                chunk.ChunkWorldPosition = ChunkPos;
                chunk.GenerateChunk();
            }
        }
        
    }
}

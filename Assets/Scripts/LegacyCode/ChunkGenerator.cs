using UnityEngine;

public class ChunkGenerator : MonoBehaviour
{
    [Header("Block Prefabs")]
    public GameObject grassPrefab;
    public GameObject dirtPrefab;
    public GameObject stonePrefab;
    public GameObject bedrockPrefab;

    [Header("Chunk Settings")]
    public int chunkSize = 25;
    public int maxHeight = 6;
    public float noiseScale = 0.1f;
    public Vector3 ChunkWorldPosition;

    [Header("Seed Settings")]
    public int seed = 0;

    private void Awake()
    {
        // ��������� ���������� seed, ���� �� �����
        if (seed == 0)
            seed = Random.Range(1, 100000);

        // ����� ������ ����������� ������� ����, ���� ����� ������������
        // noiseScale += Random.Range(-0.01f, 0.01f);
    }
    private void Start()
    {
        StaticBatchingUtility.Combine(gameObject);
    }
    public void GenerateChunk()
    {
        for (int x = 0; x < chunkSize; x++)
        {
            for (int z = 0; z < chunkSize; z++)
            {
                // ��������� ��������� ���������� ����� � �������
                float worldX = x + ChunkWorldPosition.x;
                float worldZ = z + ChunkWorldPosition.z;

                // ��������� ������ ����� Perlin Noise � ������ seed
                float noiseX = (worldX + seed) * noiseScale;
                float noiseZ = (worldZ + seed) * noiseScale;
                int height = Mathf.FloorToInt(Mathf.PerlinNoise(noiseX, noiseZ) * maxHeight);

                // ������ ������� ������
                for (int y = 0; y <= height; y++)
                {
                    GameObject blockToSpawn;

                    if (y == height)
                        blockToSpawn = grassPrefab;         // ������� ���� � �����
                    else if (y == 0 ) 
                        blockToSpawn = bedrockPrefab;
                    else if (y > height - 3)
                        blockToSpawn = dirtPrefab;          // �������� � �����
                    else
                        blockToSpawn = stonePrefab;         // ������ � ������

                    // ������������� ���� � ������� ����������
                    Vector3 spawnPos = new Vector3(worldX, y, worldZ);
                    Instantiate(blockToSpawn, spawnPos, Quaternion.identity, transform);
                    
                }
            }
        }
    }
}

using UnityEngine;
using System.Collections.Generic;

public class ChunkGeneratorPRO : MonoBehaviour
{
    public int chunkSize = 16;
    //public int chunkHeight = 10;
    public float noiseScale = 0.1f;
    public Vector3 chunkWorldPosition;
    public Material material;

    private int[,,] blocks;

    void Start()
    {
        GenerateBlockData();
        GenerateMesh();
    }

    void GenerateBlockData()
    {
        blocks = new int[chunkSize, chunkSize, chunkSize];

        for (int x = 0; x < chunkSize; x++)
        {
            for (int z = 0; z < chunkSize; z++)
            {
                float height = Mathf.PerlinNoise((x + chunkWorldPosition.x) * noiseScale, (z + chunkWorldPosition.z) * noiseScale) * chunkSize;
                int h = Mathf.FloorToInt(height);

                for (int y = 0; y < h && y < chunkSize; y++)
                {
                    blocks[x, y, z] = 1;
                }
            }
        }
    }

    void GenerateMesh()
    {
        List<Vector3> vertices = new List<Vector3>();
        List<int> triangles = new List<int>();
        List<Vector2> uvs = new List<Vector2>();

        for (int x = 0; x < chunkSize; x++)
        {
            for (int y = 0; y < chunkSize; y++)
            {
                for (int z = 0; z < chunkSize; z++)
                {
                    if (blocks[x, y, z] == 1)
                    {
                        Vector3 blockPos = new Vector3(x, y, z);
                        AddCube(blockPos, vertices, triangles);
                    }
                }
            }
        }

        Mesh mesh = new Mesh();
        mesh.vertices = vertices.ToArray();
        mesh.triangles = triangles.ToArray();
        mesh.RecalculateNormals();

        MeshFilter mf = gameObject.AddComponent<MeshFilter>();
        MeshRenderer mr = gameObject.AddComponent<MeshRenderer>();
        mf.mesh = mesh;
        mr.material = material;
    }

    void AddCube(Vector3 pos, List<Vector3> verts, List<int> tris)
    {
        int vertexIndex = verts.Count;

        // 6 граней куба
        Vector3[] cubeVerts = new Vector3[]
        {
            pos + new Vector3(0, 0, 0),
            pos + new Vector3(1, 0, 0),
            pos + new Vector3(1, 1, 0),
            pos + new Vector3(0, 1, 0),

            pos + new Vector3(0, 0, 1),
            pos + new Vector3(1, 0, 1),
            pos + new Vector3(1, 1, 1),
            pos + new Vector3(0, 1, 1)
        };

        int[] cubeTris = new int[]
        {
            // Front
            0, 2, 1, 0, 3, 2,
            // Back
            5, 6, 4, 6, 7, 4,
            // Left
            4, 7, 0, 7, 3, 0,
            // Right
            1, 2, 5, 2, 6, 5,
            // Top
            2, 3, 6, 3, 7, 6,
            // Bottom
            4, 0, 5, 0, 1, 5
        };

        Vector2[] faceUVs = new Vector2[]
        {
            new Vector2(0, 0),
            new Vector2(1, 0),
            new Vector2(1, 1),
            new Vector2(0, 1)
        };

        //for (int i = 0; i < 6; i++) // 6 граней
        //{
        //    for (int j = 0; j < 4; j++)
        //        //uvs.Add(faceUVs[j]);
        //}


        foreach (var v in cubeVerts) verts.Add(v);
        foreach (var t in cubeTris) tris.Add(vertexIndex + t);
    }
}

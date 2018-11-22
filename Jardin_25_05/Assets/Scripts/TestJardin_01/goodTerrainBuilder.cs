using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class goodTerrainBuilder : MonoBehaviour
{
    float m_Length = 1;
    float m_Width = 1;
    float m_Height = 2;
    int m_SegmentCount = 10;
    int m_StepCount = 5;

    // Use this for initialization
    void Start ()
    {
        MeshBuilder meshBuilder = new MeshBuilder();

        // setup the grid of quads
        for (int i = 0; i <= m_SegmentCount; i++)
        {
            float z = m_Length * i;
            float v = (1.0f / m_SegmentCount) * i;

            for (int j = 0; j <= m_SegmentCount; j++)
            {
                float x = m_Width * j;
                float u = (1.0f / m_SegmentCount) * j;

                Vector3 offset = new Vector3(x, Random.Range(0.0f, m_Height), z);
                Vector2 uv = new Vector2(u, v);
                bool buildTriangles = i > 0 && j > 0;

                BuildQuadForGrid(meshBuilder, offset, uv, buildTriangles, m_StepCount + 1);
            }

        }

        // Create Mesh
        Mesh mesh = meshBuilder.CreateMesh();
        mesh.RecalculateNormals();

        // Create Mesh
        MeshFilter filter = GetComponent<MeshFilter>();

        if (filter != null)
        {
            filter.sharedMesh = meshBuilder.CreateMesh();
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    void BuildQuadForGrid (MeshBuilder meshBuilder, Vector3 position, Vector2 uv, bool buildTriangles, int vertsPerRow)
    {
        meshBuilder.Vertices.Add(position);
        meshBuilder.UVs.Add(uv);

        if (buildTriangles)
        {
            int baseIndex = meshBuilder.Vertices.Count - 1;
            int index0 = baseIndex;
            int index1 = baseIndex - 1;
            int index2 = baseIndex - vertsPerRow;
            int index3 = baseIndex - vertsPerRow - 1;

            meshBuilder.AddTriangle(index0, index2, index1);
            meshBuilder.AddTriangle(index2, index3, index1);

        }
    }
}

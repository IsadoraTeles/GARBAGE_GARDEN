﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class terrainBuilder : MonoBehaviour
{
    float m_Length = 1;
    float m_Width = 1;
    float m_Height = 1;
    int m_SegmentCount = 10;

    // Use this for initialization
    void Start()
    {
        MeshBuilder meshBuilder = new MeshBuilder();

        // setup the grid of quads
        for(int i = 0; i < m_SegmentCount; i++)
        {
            float z = m_Length * i;
            for(int j = 0; j < m_SegmentCount; j++)
            {
                float x = m_Width * j;
                Vector3 offset = new Vector3(x, Random.Range(0.0f, m_Height), z);

                BuildQuad(meshBuilder, offset);
            }

        }

        // Create Mesh
        MeshFilter filter = GetComponent<MeshFilter>();

        if (filter != null)
        {
            filter.sharedMesh = meshBuilder.CreateMesh();
        }

    }

    // Update is called once per frame
    void Update()
    {

    }

    void BuildQuad(MeshBuilder meshBuilder, Vector3 offset)
    {
        // setup the vertices and triangles for the quad
        meshBuilder.Vertices.Add(new Vector3 (0.0f, 0.0f, 0.0f) + offset);
        meshBuilder.UVs.Add(new Vector2(0.0f, 0.0f));
        meshBuilder.Normals.Add(Vector3.up);

        meshBuilder.Vertices.Add(new Vector3(0.0f, 0.0f, m_Length) + offset);
        meshBuilder.UVs.Add(new Vector2(0.0f, 1.0f));
        meshBuilder.Normals.Add(Vector3.up);

        meshBuilder.Vertices.Add(new Vector3(m_Width, 0.0f, m_Length) + offset);
        meshBuilder.UVs.Add(new Vector2(1.0f, 1.0f));
        meshBuilder.Normals.Add(Vector3.up);

        meshBuilder.Vertices.Add(new Vector3(m_Width, 0.0f, 0.0f) + offset);
        meshBuilder.UVs.Add(new Vector2(1.0f, 0.0f));
        meshBuilder.Normals.Add(Vector3.up);

        int baseIndex = meshBuilder.Vertices.Count - 4;

        // setup the quad
        meshBuilder.AddTriangle(baseIndex, baseIndex + 1, baseIndex + 2);
        meshBuilder.AddTriangle(baseIndex, baseIndex + 2, baseIndex + 3);

    }
}

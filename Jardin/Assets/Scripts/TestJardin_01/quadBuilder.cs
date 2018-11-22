using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class quadBuilder : MonoBehaviour
{
    

	// Use this for initialization
	void Start ()
    {
        MeshBuilder meshBuilder = new MeshBuilder();
        float m_Length = 10;
        float m_Width = 10;

        // setup the vertices and triangles
        meshBuilder.Vertices.Add(new Vector3(0.0f, 0.0f, 0.0f));
        meshBuilder.UVs.Add(new Vector2(0.0f, 0.0f));
        meshBuilder.Normals.Add(Vector3.up);

        meshBuilder.Vertices.Add(new Vector3(0.0f, 0.0f, m_Length));
        meshBuilder.UVs.Add(new Vector2(0.0f, 1.0f));
        meshBuilder.Normals.Add(Vector3.up);

        meshBuilder.Vertices.Add(new Vector3(m_Width, 0.0f, m_Length));
        meshBuilder.UVs.Add(new Vector2(1.0f, 1.0f));
        meshBuilder.Normals.Add(Vector3.up);

        meshBuilder.Vertices.Add(new Vector3(m_Width, 0.0f, 0.0f));
        meshBuilder.UVs.Add(new Vector2(1.0f, 0.0f));
        meshBuilder.Normals.Add(Vector3.up);

        meshBuilder.AddTriangle(0, 1, 2);
        meshBuilder.AddTriangle(0, 2, 3);

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

}

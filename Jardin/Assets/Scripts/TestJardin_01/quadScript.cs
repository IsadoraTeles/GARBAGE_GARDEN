using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class quadScript : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        float m_Width = 10;
        float m_Length = 10;

        Vector3[] vertices = new Vector3[4];
        Vector3[] normals = new Vector3[4];
        Vector2[] uv = new Vector2[4];

        int[] indices = new int[6]; // 2 triangles, 3 indices each

        Mesh mesh = new Mesh();

        vertices [0] = new Vector3 (0.0f, 0.0f, 0.0f);
        uv [0] = new Vector2(0.0f, 0.0f);
        normals[0] = Vector3.up;

        vertices[1] = new Vector3(0.0f, 0.0f, m_Length);
        uv[1] = new Vector2(0.0f, 1.0f);
        normals[1] = Vector3.up;

        vertices[2] = new Vector3(m_Width, 0.0f, m_Length);
        uv[2] = new Vector2(1.0f, 1.0f);
        normals[2] = Vector3.up;

        vertices[3] = new Vector3(m_Width, 0.0f, 0.0f);
        uv[3] = new Vector2(1.0f, 0.0f);
        normals[3] = Vector3.up;

        indices[0] = 0;
        indices[1] = 1;
        indices[2] = 2;

        indices[3] = 0;
        indices[4] = 2;
        indices[5] = 3;

        mesh.vertices = vertices;
        mesh.normals = normals;
        mesh.uv = uv;
        mesh.triangles = indices;
        mesh.RecalculateBounds(); // for render culling

        MeshFilter filter = GetComponent<MeshFilter>();

        if(filter != null)
        {
            filter.sharedMesh = mesh;
        }
 
    }

    // Update is called once per frame
    void Update ()
    {
		
	}
}

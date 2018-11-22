using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class goodQuadBuilder : MonoBehaviour
{
    float m_Length = 5;
    float m_Width = 5;
    float m_Height = 5;

	// Use this for initialization
	void Start ()
    {
        MeshBuilder meshBuilder = new MeshBuilder();

        Vector3 upDir = Vector3.up * m_Height;
        Vector3 rightDir = Vector3.right * m_Width;
        Vector3 forwardDir = Vector3.forward * m_Length;

        // pivot at origin
        Vector3 farCorner = (upDir + rightDir + forwardDir) / 2;
        Vector3 nearCorner = -farCorner;

        BuildQuad(meshBuilder, nearCorner, forwardDir, rightDir);
        BuildQuad(meshBuilder, nearCorner, rightDir, upDir);
        BuildQuad(meshBuilder, nearCorner, upDir, forwardDir);

        BuildQuad(meshBuilder, farCorner, -rightDir, -forwardDir);
        BuildQuad(meshBuilder, farCorner, -upDir, -rightDir);
        BuildQuad(meshBuilder, farCorner, -forwardDir, -upDir);

        // Create Mesh
        //Mesh mesh = meshBuilder.CreateMesh();
        //mesh.RecalculateNormals();

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

    // alternatively you could write a BuildQuad() function that directly takes the POSITION of each of the four corners and uses them
    void BuildQuad (MeshBuilder meshBuilder, Vector3 offset, Vector3 widthDir, Vector3 lengthDir)
    {
        Vector3 normal = Vector3.Cross(lengthDir, widthDir).normalized;

        meshBuilder.Vertices.Add(offset);
        meshBuilder.UVs.Add(new Vector2(0.0f, 0.0f));
        meshBuilder.Normals.Add(normal);

        meshBuilder.Vertices.Add(offset + lengthDir);
        meshBuilder.UVs.Add(new Vector2(0.0f, 1.0f));
        meshBuilder.Normals.Add(normal);

        meshBuilder.Vertices.Add(offset + lengthDir + widthDir);
        meshBuilder.UVs.Add(new Vector2(1.0f, 1.0f));
        meshBuilder.Normals.Add(normal);

        meshBuilder.Vertices.Add(offset + widthDir);
        meshBuilder.UVs.Add(new Vector2(1.0f, 0.0f));
        meshBuilder.Normals.Add(normal);

        int baseIndex = meshBuilder.Vertices.Count - 4;

        meshBuilder.AddTriangle(baseIndex, baseIndex + 1, baseIndex + 2);
        meshBuilder.AddTriangle(baseIndex, baseIndex + 2, baseIndex + 3);


    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class meshFollowSpline : MonoBehaviour
{
    // Instances
    public Transform[] items;
    public BezierSpline spline;
    public int frequency;

    // Mesh
    float m_Length = 5;
    float m_Width = 5;
    float m_Height = 5;


    // Use this for initialization
    void Start ()
    {
        MeshBuilder meshBuilder = new MeshBuilder();
        float m_Length = 10;
        float m_Width = 10;

        buildQuad(meshBuilder);

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
        updateInstances();
        
        // get vertices
        Mesh m = GetComponent<MeshFilter>().mesh;
        Vector3[] vertices = m.vertices;
        Vector3[] normals = m.normals;

        // modify them
        int i = 0;
        while (i < vertices.Length)
        {
            vertices[i] += normals[i] * Mathf.Sin(Time.time);
            i++;
        }
        // assign them back to the mesh
        m.vertices = vertices;
    }

    void updateInstances()
    {
        if (frequency <= 0 || items == null || items.Length == 0)
        {
            return;
        }

        float stepSize = 1f / (frequency * items.Length);

        for (int p = 0, f = 0; f < frequency; f++)
        {
            for (int i = 0; i < items.Length; i++, p++) //gets the array length of custom mesh
            {   
                // instantiates item as transform
                Transform item = Instantiate(items[i]) as Transform;
                // references my curve point and creates position vector each update
                Vector3 position = spline.GetPointCubic(p * stepSize);
                //transforms instantiate item to curve point position
                item.transform.localPosition = position; 
            }
        }
    }

    void buildQuad (MeshBuilder mesh)
    {
        // setup the vertices and triangles
        mesh.Vertices.Add(new Vector3(0.0f, 0.0f, 0.0f));
        mesh.UVs.Add(new Vector2(0.0f, 0.0f));
        mesh.Normals.Add(Vector3.up);

        mesh.Vertices.Add(new Vector3(0.0f, 0.0f, m_Length));
        mesh.UVs.Add(new Vector2(0.0f, 1.0f));
        mesh.Normals.Add(Vector3.up);

        mesh.Vertices.Add(new Vector3(m_Width, 0.0f, m_Length));
        mesh.UVs.Add(new Vector2(1.0f, 1.0f));
        mesh.Normals.Add(Vector3.up);

        mesh.Vertices.Add(new Vector3(m_Width, 0.0f, 0.0f));
        mesh.UVs.Add(new Vector2(1.0f, 0.0f));
        mesh.Normals.Add(Vector3.up);

        mesh.AddTriangle(0, 1, 2);
        mesh.AddTriangle(0, 2, 3);
    }
}

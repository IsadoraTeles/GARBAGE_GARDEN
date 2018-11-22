using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode, RequireComponent(typeof(LineRenderer))]
public class Hermite_Spline : MonoBehaviour
{
    public List<GameObject> controlPoints = new List<GameObject>();
    public Color startColor = Color.white;
    public Color endColor = Color.white;
    public float startWidth = 0.2f;
    public float endtWidth = 0.2f;
    public int numberOfPoints = 20;
    LineRenderer lineRenderer;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.useWorldSpace = true;
        lineRenderer.material = new Material(Shader.Find("Particles/Additive"));
    }

    void Update()
    {
        if (null == lineRenderer || controlPoints == null || controlPoints.Count < 2)
        {
            return; // not enough points specified
        }

        // update line renderer
        lineRenderer.startColor = startColor;
        lineRenderer.endColor = endColor;
        lineRenderer.startWidth = startWidth;
        lineRenderer.endWidth = endtWidth;
        if (numberOfPoints < 2)
        {
            numberOfPoints = 2;
        }

        float timer = Time.time / 10.0f;
        if (timer >= 1) { timer = 1.0f; }

        lineRenderer.positionCount = (int)((numberOfPoints * (controlPoints.Count - 1)) * timer);

        // loop over segments of spline
        Vector3 p0, p1, m0, m1;

        for (int j = 0; j < controlPoints.Count - 1; j++)
        {
            // check control points
            if (controlPoints[j] == null || controlPoints[j + 1] == null ||
                (j > 0 && controlPoints[j - 1] == null) ||
                (j < controlPoints.Count - 2 && controlPoints[j + 2] == null))
            {
                return;
            }
            // determine control points of segment
            p0 = controlPoints[j].transform.position;
            p1 = controlPoints[j + 1].transform.position;

            if (j > 0)
            {
                m0 = 0.3f * (controlPoints[j + 1].transform.position - controlPoints[j - 1].transform.position) * j;
            }
            else
            {
                m0 = (controlPoints[j + 1].transform.position - controlPoints[j].transform.position);
            }
            if (j < controlPoints.Count - 2)
            {
                m1 = 1.3f * (controlPoints[j + 2].transform.position - controlPoints[j].transform.position);
            }
            else
            {
                m1 = (controlPoints[j + 1].transform.position - controlPoints[j].transform.position);
            }

            // set points of Hermite curve
            Vector3 position;
            float t;
            float pointStep = 1.0f / numberOfPoints;

            if (j == controlPoints.Count - 2)
            {
                pointStep = 1.0f / (numberOfPoints - 1.0f);
                // last point of last segment should reach p1
            }
            for (int i = 0; i < numberOfPoints; i++)
            {
                t = (i * pointStep);
                position = (2.0f * t * t * t - 3.0f * t * t + 1.0f) * p0
                    + (t * t * t - 2.0f * t * t + t) * m0
                    + (-2.0f * t * t * t + 3.0f * t * t) * p1
                    + (t * t * t - t * t) * m1;
                float arrayPosition = i + j * numberOfPoints;
                if (arrayPosition < lineRenderer.positionCount)
                {
                    lineRenderer.SetPosition((i + j * numberOfPoints), position);
                }
            }
        }
    }
}

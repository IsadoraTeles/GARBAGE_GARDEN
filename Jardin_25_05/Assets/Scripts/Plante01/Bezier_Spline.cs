using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode, RequireComponent(typeof(LineRenderer))]
public class Bezier_Spline : MonoBehaviour
{
    public List<GameObject> controlPoints = new List<GameObject>();
    public Color startColor = Color.white;
    public Color endColor = Color.white;

    public float startWidth = 0.2f;
    public float endtWidth = 0.2f;

    public int numberOfPoints = 20;
    LineRenderer lineRenderer;
    private int j = 0;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.useWorldSpace = true;
        lineRenderer.material = new Material(Shader.Find("Particles/Additive"));
    }


    void Update()
    {
        if (null == lineRenderer || controlPoints == null || controlPoints.Count < 3)
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

        lineRenderer.positionCount = (int)((numberOfPoints * (controlPoints.Count - 2)) * timer);
 
        Vector3 p0, p1, p2;
        for (int j = 0; j < controlPoints.Count - 2; j++)
        {

            // check control points
            if (controlPoints[j] == null || controlPoints[j + 1] == null
            || controlPoints[j + 2] == null)
            {
                return;
            }
            // determine control points of segment
            p0 = 0.5f * (controlPoints[j].transform.position
            + controlPoints[j + 1].transform.position);
            p1 = controlPoints[j + 1].transform.position;
            p2 = 0.5f * (controlPoints[j + 1].transform.position
            + controlPoints[j + 2].transform.position);

            if (controlPoints[j] == controlPoints[0]) { p0 = controlPoints[j].transform.position; }
            int nbInteration = controlPoints.Count - 3;
            if (controlPoints[j] == controlPoints[nbInteration])
            { p2 = controlPoints[j + 2].transform.position; }

            // set points of quadratic Bezier curve
            Vector3 position;
            float t;
            float pointStep = (1.0f / numberOfPoints);
            if (j == controlPoints.Count - 3)
            {
                pointStep = (1.0f / (numberOfPoints - 1.0f));
                // last point of last segment should reach p2
            }
            
              
            for (int i = 0; i < numberOfPoints; i++)
            {                
                t = i * (pointStep);
                position = ((1.0f - t) * (1.0f - t) * p0
                + 2.0f * (1.0f - t) * t * p1 + t * t * p2);
                float arrayPosition = i + j * numberOfPoints;
                if (arrayPosition < lineRenderer.positionCount) { 
                    lineRenderer.SetPosition((i + j * numberOfPoints), position);
                }
            }

         }
    }
}
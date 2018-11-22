﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode, RequireComponent(typeof(LineRenderer))]
public class MakeBranch : MonoBehaviour
{
    private GameObject prefabFleur;
    private GameObject prefabFeuille;
    private GameObject clone;

    private List<Vector3> controlPoints = new List<Vector3>();
    private List<Vector3> allPoints;
    private List<int> fleursIndex;
    private List<int> feuillesIndex;
    public Color startColor;
    public Color endColor;
    public float startWidth;
    public float endtWidth;
    public int numberOfPoints;
    public int numberOfControlPoints;
    public int growingTime;
    public float growingMultiplierM0;
    public float growingMultiplierM1;
    public int numberOfAllPoints;
    public float timer = 0;
    public int indexOrigin;
    public bool begin;
    public int type;
    int indexSprite;

    public LineRenderer lineRenderermmm;

    public void setupPlant(List<Vector3> in_controlPoints, List<int> m_fleursIndex, List<int> m_feuillesIndex,Color in_startColor, Color in_endColor, float in_startWidth, float in_endWidth, int in_numberOfPoints, int in_numberOfControlPoints, int m_growingTime, float m_growingMultiplierM0, float m_growingMultiplierM1, LineRenderer in_lineRederer, bool in_begin, int m_type)
    {
        controlPoints = in_controlPoints;
        fleursIndex = m_fleursIndex;
        feuillesIndex = m_feuillesIndex;
        startColor = in_startColor;
        endColor = in_endColor;
        startWidth = in_startWidth;
        endtWidth = in_endWidth;
        numberOfPoints = in_numberOfPoints;
        numberOfControlPoints = in_numberOfControlPoints;
        growingTime = m_growingTime;
        growingMultiplierM0 = m_growingMultiplierM0;
        growingMultiplierM1 = m_growingMultiplierM1;
        lineRenderermmm = in_lineRederer;
        begin = in_begin;
        type = m_type;

        setupLineRenderer();
        //counter = 0;

        allPoints = new List<Vector3>();
        allPoints = loppOverSegmentsSetHermitePoints(controlPoints);
        numberOfAllPoints = allPoints.Count;

        prefabFleur = Resources.Load<GameObject>("prefabz/DeformSprite");
        prefabFeuille = Resources.Load<GameObject>("prefabz/feuille");
    }

    public List<Vector3> getAllPoints()
    {
        return allPoints;
    }

    public List<Vector3> getControlPoints()
    {
        return controlPoints;
    }

    public void updatePlant()
    {
        //allPoints = new List<Vector3>();
        allPoints = loppOverSegmentsSetHermitePoints(controlPoints);
        numberOfAllPoints = allPoints.Count;
        updateLineRenderer(allPoints);
    }

    public void setupLineRenderer()
    {
        lineRenderermmm = GetComponent<LineRenderer>();
        lineRenderermmm.useWorldSpace = true;
        lineRenderermmm.material = new Material(Shader.Find("Particles/Standard Unlit"));
    }

    public List<Vector3> loppOverSegmentsSetHermitePoints(List<Vector3> m_controlPoints)
    {
        // loop over segments of spline
        Vector3 p0, p1, m0, m1;
        controlPoints = m_controlPoints;
        //numberOfControlPoints = controlPoints.Count;
        List<Vector3> m_allPoints = new List<Vector3>();

        for (int j = 0; j < controlPoints.Count - 1; j++)
        {
            // check control points
            //if (controlPoints[j] == null || controlPoints[j + 1] == null ||
            //    (j > 0 && controlPoints[j - 1] == null) ||
            //    (j < controlPoints.Count - 2 && controlPoints[j + 2] == null))
            //{
            //    break;
            //}
            // determine control points of segment
            p0 = controlPoints[j];
            p1 = controlPoints[j + 1];

            if (j > 0)
            {
                m0 = growingMultiplierM0 * (controlPoints[j + 1]- controlPoints[j - 1]) * j;
            }
            else
            {
                m0 = (controlPoints[j + 1] - controlPoints[j]);
            }
            if (j < controlPoints.Count - 2)
            {
                m1 = growingMultiplierM1 * (controlPoints[j + 2] - controlPoints[j]);
            }
            else
            {
                m1 = (controlPoints[j + 1] - controlPoints[j]);
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

                m_allPoints.Add(position);
            }
        }

        return m_allPoints;

    }

    public void updateLineRenderer(List<Vector3> m_allPoints)
    {
        if (null == lineRenderermmm || controlPoints == null || controlPoints.Count < 2)
        {
            return; // not enough points specified
        }

        // update line renderer
        lineRenderermmm.startColor = startColor;
        lineRenderermmm.endColor = endColor;
        lineRenderermmm.startWidth = startWidth;
        lineRenderermmm.endWidth = endtWidth;
        if (numberOfPoints < 2)
        {
            numberOfPoints = 2;
        }

        if (timer >= 1) { timer = 1.0f; }

        lineRenderermmm.positionCount = (int)((numberOfPoints * (controlPoints.Count - 1)) * timer);
        
        for (int i = 0; i <= allPoints.Count; i++)
        {
            int nextFlowerIndex = 10000000;
            int nextFeuilleIndex = 10000000;
            if (fleursIndex.Count != 0) {nextFlowerIndex = (fleursIndex[0] * numberOfPoints) - 1;}
            if (feuillesIndex.Count != 0) { nextFeuilleIndex = (feuillesIndex[0] * numberOfPoints) - 1; }

            if (i < lineRenderermmm.positionCount)
            {
               // Debug.Log("Next Flower Index = " + nextFlowerIndex + " i = " + i + " Point position = " + m_allPoints[i]);
                lineRenderermmm.SetPosition(i, m_allPoints[i]);

                // FLEURS
                if ((nextFlowerIndex <= i))
                {                    
                    float angle = Random.Range(0.0f, 360.0f);
                    float size = Random.Range(5.0f, 13.0f);
                    if (type == 1 || type == 2 || type == 3)
                    {
                        indexSprite = 1;
                    }
                    else if (type == 4 || type == 5 || type == 6)
                    {
                        indexSprite = 2;
                    }
                    else if (type == 7)
                    {
                        indexSprite = 3;
                    }
                    Vector3 infos = new Vector3(angle, size, indexSprite);
                    createFlowers(m_allPoints[i], infos);
                    fleursIndex.RemoveAt(0);
                 }

                // FEUILLES
                if ((nextFeuilleIndex <= i))
                {
                    float angle = Random.Range(0.0f, 360.0f);
                    float size = Random.Range(10.0f, 20.0f);
                    if (type == 1 || type == 2 || type == 3)
                    {
                        indexSprite = 1;
                    }
                    else if (type == 4 || type == 5 || type == 6)
                    {
                        indexSprite = 1;
                    }
                    else if (type == 7)
                    {
                        indexSprite = 1;
                    }
                    Vector3 infos = new Vector3(angle, size, indexSprite);
                    createLeafs(m_allPoints[i], infos);
                    feuillesIndex.RemoveAt(0);
                }
            }
           }
    }

    public void createFlowers(Vector3 pos, Vector3 info) //in info x: angle, y: size, z: index
    {
        //info contains x: angleZ, y: size; z: indexSprite
            clone = (GameObject)Instantiate(prefabFleur);
            //transform
            clone.transform.parent = transform; //put clone as child
            clone.transform.position = new Vector3(pos.x, pos.y, pos.z - 1.0f);
            
            clone.transform.rotation = Quaternion.Euler(new Vector3(0.0f, 0.0f, info.x));
            clone.transform.localScale = clone.transform.localScale * info.y;
            clone.GetComponent<SpriteRenderer>().sortingOrder = Random.Range(16385, 32768); //To avoid overlaping
            Animator anim = clone.GetComponent<Animator>();
            anim.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("Textures/fleur" + (int)info.z);
            clone.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Textures/fleur" + (int)info.z);
        //Pour les sprites de plantes
        //clone.GetComponent<SpriteRenderer>().sprite = Resources.Load <Sprite> ("Textures/leaf" + (int)info.z);
    }

    public void createLeafs(Vector3 pos, Vector3 info) //in info x: angle, y: size, z: index
    {
        //info contains x: angleZ, y: size; z: indexSprite
        clone = (GameObject)Instantiate(prefabFeuille);
        //transform
        clone.transform.parent = transform; //put clone as child
        clone.transform.position = new Vector3(pos.x, pos.y, pos.z - 0.5f); ;
        clone.transform.rotation = Quaternion.Euler(new Vector3(0.0f, 0.0f, info.x));
        clone.transform.localScale = clone.transform.localScale * info.y;
        clone.GetComponent<SpriteRenderer>().sortingOrder = Random.Range(0, 16385); //To avoid overlaping
        clone.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Textures/feuille" + (int)info.z);
        //Pour les sprites de plantes
        //clone.GetComponent<SpriteRenderer>().sprite = Resources.Load <Sprite> ("Textures/leaf" + (int)info.z);
    }
}

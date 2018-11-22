//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//[ExecuteInEditMode, RequireComponent(typeof(LineRenderer))]
//public class Tige : MonoBehaviour
//{
//    //public List<GameObject> controlPoints = new List<GameObject>();
//    private List<Vector3> points = new List<Vector3>(); // tige
//    public int poinstCount;
//    public int indexPointsTige;

//    public bool have_feuilles;
//    private List<Vector3> feuilles = new List<Vector3>(); 
//    public int feuillesCount;
//    public int indexPointsFeuilles = 0;

//    public bool have_fleurs;
//    private List<Vector3> fleurs = new List<Vector3>(); 
//    public int fleursCount;
//    public int indexPointsFleurs = 0;

//    public bool have_children;
//    private List<Vector3> child = new List<Vector3>();
//    private List<int> childrenOriginsIndex = new List<int>();
//    public int childrenCount;
//    public int childCount;

//    public int indexPointsChildren = 0;

//    public Transform instance;

//    public bool have_mutation;

//    // LINE
//    public Vector3 origin;
//    public Color startColor = Color.white;
//    public Color endColor = Color.white;
//    public float startWidth = 0.2f;
//    public float endtWidth = 0.2f;
//    public int numberOfPoints = 20;
//    LineRenderer lineRenderer;
    
//    // CROISSANCE
//    public float tempsCroissance;
//    public float countCroissance;
//    public Vector3 positionActuelleCroissance;

//    public TigeControlHierarchie modeTige;
//    public ControlMutation modeMutation;

//    // Use this for initialization
//    void Start ()
//    {
//        //origin = new Vector3(0, 0, 0);
//        //initializeLineRenderer();
//        //// GENERATE ARRAY FOR MAIN BRANCH
//        //poinstCount = 6;
//        //points = generatePoints(origin, TigeControlHierarchie.Primaire, poinstCount);
//        //childrenCount = 3;
//        //// points for children : 
//        //for (int i = 0; i < childrenCount; i++)
//        //{
//        //    for (int j = 0; j < childCount; j++)
//        //    {
//        //        //children[0]
//        //    }
//        //}
//        ////children = generatePoints();

//    }

//    // Update is called once per frame
//    void Update ()
//    {
//        updateLineRenderer();
//        loopOverSpline();
//    }

//    public void initializeLineRenderer()
//    {
//        lineRenderer = GetComponent<LineRenderer>();
//        lineRenderer.useWorldSpace = true;
//        lineRenderer.material = new Material(Shader.Find("Particles/Additive"));
//    }

//    public List<Vector3> generatePoints (Vector3 m_origin, TigeControlHierarchie m_mode, int m_pointsCount)
//    {
//        Vector3 origin = m_origin;
//        List<Vector3> generatedPoints = new List<Vector3>();

//        generatedPoints.Add(origin);

//        if (m_mode == TigeControlHierarchie.Primaire)
//        {
//            for (int i = 1; i < m_pointsCount - 1; i++)
//            {
//                float x = generatedPoints[0].x + Random.Range(origin.x - 4, origin.x + 4);
//                float y = generatedPoints[0].y + Random.Range(origin.y + i * 3, origin.y + i * 5);
//                Vector3 temp = new Vector3(x, y, 0);
//                generatedPoints.Add(temp);
//            }
//        }

//        if (m_mode == TigeControlHierarchie.SecondaireDroite)
//        {
//            for (int i = 1; i < m_pointsCount - 1; i++)
//            {
//                float x = generatedPoints[0].x + Random.Range(origin.x + i * 3, origin.x + i * 8);
//                float y = generatedPoints[0].y + Random.Range(-2, 6);
//                Vector3 temp = new Vector3(x, y, 0);
//                generatedPoints.Add(temp);
//            }
//        }

//        else if (m_mode == TigeControlHierarchie.SecondaireGauche)
//        {
//            for (int i = 1; i < m_pointsCount - 1; i++)
//            {
//                float x = generatedPoints[0].x + Random.Range(origin.x - i * 2, origin.x - i * 3);
//                float y = generatedPoints[0].y + Random.Range(-2, 6);
//                Vector3 temp = new Vector3(x, y, 0);
//                generatedPoints.Add(temp);
//            }
//        }

//        else if (m_mode == TigeControlHierarchie.TerciaireBas)
//        {
//            for (int i = 1; i < m_pointsCount - 1; i++)
//            {
//                float x = generatedPoints[0].x + Random.Range(origin.x - i, origin.x + i + 2);
//                float y = generatedPoints[0].y + Random.Range(origin.y - i - 2, origin.y - i - 4);
//                Vector3 temp = new Vector3(x, y, 0);
//                generatedPoints.Add(temp);
//            }
//        }

//        return generatedPoints;
//    }

//    public void updateLineRenderer()
//    {
//        if (null == lineRenderer || generatedControlPoints == null || generatedControlPoints.Count < 2)
//        {
//            return; // not enough points specified
//        }

//        // update line renderer
//        lineRenderer.startColor = startColor;
//        lineRenderer.endColor = endColor;
//        lineRenderer.startWidth = startWidth;
//        lineRenderer.endWidth = endtWidth;

//        if (numberOfPoints < 2)
//        {
//            numberOfPoints = 2;
//        }

//        // ----* **IL MANQUE CONTEUR COUNTCROISSANCE
//        float timer = Time.time / 10.0f;
//        if (timer >= 1) { timer = 1.0f; }

//        lineRenderer.positionCount = (int)((numberOfPoints * (generatedControlPoints.Count - 1)) * timer);
//        //lineRenderer.positionCount = (int)((numberOfPoints * (generatedControlPoints.Count - 1)) * countCroissance);
//    }

//    public void loopOverSpline()
//    {
//        // loop over segments of spline
//        Vector3 p0, p1, m0, m1;

//        for (int j = 0; j < generatedControlPoints.Count - 1; j++)
//        {
//            // check control points
//            if (generatedControlPoints[j] == null || generatedControlPoints[j + 1] == null ||
//                (j > 0 && generatedControlPoints[j - 1] == null) ||
//                (j < generatedControlPoints.Count - 2 && generatedControlPoints[j + 2] == null))
//            {
//                return;
//            }
//            // determine control points of segment
//            p0 = generatedControlPoints[j];
//            p1 = generatedControlPoints[j + 1];

//            if (j > 0)
//            {
//                m0 = 0.3f * (generatedControlPoints[j + 1] - generatedControlPoints[j - 1]) * j;
//            }
//            else
//            {
//                m0 = (generatedControlPoints[j + 1] - generatedControlPoints[j]);
//            }
//            if (j < generatedControlPoints.Count - 2)
//            {
//                m1 = 1.3f * (generatedControlPoints[j + 2] - generatedControlPoints[j]);
//            }
//            else
//            {
//                m1 = (generatedControlPoints[j + 1] - generatedControlPoints[j]);
//            }

//            // set points of Hermite curve
//            Vector3 position;
//            float t;
//            float pointStep = 1.0f / numberOfPoints;

//            if (j == generatedControlPoints.Count - 2)
//            {
//                pointStep = 1.0f / (numberOfPoints - 1.0f);
//                // last point of last segment should reach p1
//            }
//            for (int i = 0; i < numberOfPoints; i++)
//            {
//                t = (i * pointStep);
//                position = (2.0f * t * t * t - 3.0f * t * t + 1.0f) * p0
//                    + (t * t * t - 2.0f * t * t + t) * m0
//                    + (-2.0f * t * t * t + 3.0f * t * t) * p1
//                    + (t * t * t - t * t) * m1;
//                float arrayPosition = i + j * numberOfPoints;
//                if (arrayPosition < lineRenderer.positionCount)
//                {
//                    lineRenderer.SetPosition((i + j * numberOfPoints), position);
//                }
//            }
//        }
//    }

    
//}

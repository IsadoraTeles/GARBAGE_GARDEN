using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class makeBrancheLsystem : MonoBehaviour
{
    public float lifeTime;

    // BRANCH
    MakeBranch branch;
    MakePointsforLsys lsysPoints;
    public LineRenderer lineRendererBranch;
    public float countBranch;

    // LSYS
    private float angle;
    private float length;
    private int iterations;
    private List<Vector3> points = new List<Vector3>();
    public Vector3 origin;

    // SPRITES
    private List<int> fleursIndex = new List<int>();
    private List<int> feuillesIndex = new List<int>();


    // Use this for initialization
    public void setupPlant (Vector3 m_origin, int m_type)
    {
        lifeTime = Random.Range(10.0f, 20.0f);

        // BRANCH
        branch = gameObject.AddComponent<MakeBranch>();
        lsysPoints = gameObject.AddComponent<MakePointsforLsys>();
        lineRendererBranch = gameObject.GetComponent<LineRenderer>();
        //float randXPos = Random.Range(-100, 100);
        //float randZPos = Random.Range(-100, 100);
        origin = m_origin;

        // LINE
        float tempStartWidth = Random.Range(2, 4);
        float tempEndWidth = Random.Range(0.5f, 1f);
        //float tempGrowingMultiplierM0 = Random.Range(0.1f, 0.2f);
        //float tempGrowingMultiplierM1 = Random.Range(0f, 0.2f);

        // LSYS
        angle = Random.Range(15, 80);
        length = Random.Range(2, 4);
        iterations = (int)(Random.Range(2, 5));
        int type = m_type;

        float redS = 0.5f + Random.Range(-0.3f, 0.4f);
        float greenS = 0.5f + Random.Range(-0.3f, 0.4f);
        float blueS = 0.0f + Random.Range(0.1f, 0.2f);
        float redE = 0.5f + Random.Range(-0.3f, 0.3f);
        float greenE = 0.5f + Random.Range(-0.3f, 0.4f);
        float blueE = 0.5f + Random.Range(-0.4f, 0.1f);

        Color startColor = new Color(redS, greenS, blueS, 255);
        Color endColor = new Color(redE, greenE, blueE, 255);

        lsysPoints.startLsys(angle, length, iterations, origin, type);
        points = lsysPoints.getPoints();
        fleursIndex = lsysPoints.getListFlower();
        feuillesIndex = lsysPoints.getListFeuilles();
        //Debug.Log(fleursIndex.Count);
        branch.setupPlant(points, fleursIndex, feuillesIndex, startColor, endColor, tempStartWidth, tempEndWidth, 1, points.Count, iterations * 4, 0.0f, 0.0f, lineRendererBranch, false, type);
    }
	
	// Update is called once per frame
	void Update ()
    {
        lifeTime -= 0.01f;

        if (this.isActiveAndEnabled)
        {
            if (branch.begin == false)
            {
                countBranch = Time.time;
                branch.begin = true;
            }
            if (branch.begin == true)
            {
                branch.timer = (Time.time - countBranch) / branch.growingTime;
                branch.updatePlant();
            }
        }
    }
}

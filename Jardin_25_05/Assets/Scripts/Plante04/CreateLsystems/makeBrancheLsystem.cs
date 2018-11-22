using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class makeBrancheLsystem : MonoBehaviour
{
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
        // BRANCH
        branch = gameObject.AddComponent<MakeBranch>();
        lsysPoints = gameObject.AddComponent<MakePointsforLsys>();
        lineRendererBranch = gameObject.GetComponent<LineRenderer>();
        float randXPos = Random.Range(-100, 100);
        float randZPos = Random.Range(-100, 100);
        origin = m_origin;

        // LINE
        float tempStartWidth = Random.Range(2, 3);
        float tempEndWidth = Random.Range(0.5f, 1f);
        float tempGrowingMultiplierM0 = Random.Range(0.1f, 0.2f);
        float tempGrowingMultiplierM1 = Random.Range(0f, 0.2f);

        // LSYS
        angle = Random.Range(15, 60);
        length = Random.Range(3, 7);
        iterations = (int)(Random.Range(3, 5));
        int type = m_type;

        lsysPoints.startLsys(angle, length, iterations, origin, type);
        points = lsysPoints.getPoints();
        fleursIndex = lsysPoints.getListFlower();
        feuillesIndex = lsysPoints.getListFeuilles();
        Debug.Log(fleursIndex.Count);
        branch.setupPlant(points, fleursIndex, feuillesIndex, Color.red, Color.blue, tempStartWidth, tempEndWidth, 2, points.Count, iterations * 4, 0.0f, 0.0f, lineRendererBranch, false, type);
    }
	
	// Update is called once per frame
	void Update ()
    {
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

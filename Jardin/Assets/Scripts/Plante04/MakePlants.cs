using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakePlants : MonoBehaviour
{

    // BRANCH
    MakeBranch branch;
    public LineRenderer lineRendererBranch;
    public float countBranch;

    // TIGE
    int nTiges = 8;
    List<MakeBranch> tiges = new List<MakeBranch>();
    public GameObject test;
    List<GameObject> tests = new List<GameObject>();
    MakeBranch tige;
    public LineRenderer lineRendererTige;
    public List<float> countTige = new List<float>();

    Vector3 tempOriginTige;
    public int indexBranch;

    // SPRITES
    //public int numFeuilles = 
    private List<Vector3> fleursPosPrincipale = new List<Vector3>();
    private List<Vector3> fleursInfosPrincipale = new List<Vector3>();

    private List<Vector3> fleursPosTiges = new List<Vector3>();
    private List<Vector3> fleursInfosTiges = new List<Vector3>();

    private List<Vector3> feuillesPos = new List<Vector3>();
    private List<Vector3> feuilleInfos = new List<Vector3>();
    //private List<int> feuillesPosIndexBranche = new List<int>();

    List<Vector3> tempControlPointsBranche = new List<Vector3>();
    List<Vector3> tempControlPointsTige = new List<Vector3>();

    void Start()
    {

        // BRANCH
        branch = gameObject.AddComponent<MakeBranch>();
        lineRendererBranch = gameObject.AddComponent<LineRenderer>();
        float randXPos = Random.Range(-150, 150);
        float randZPos = Random.Range(-150, 150);
        Vector3 tempOriginBranch = new Vector3(randXPos, 0, randZPos);
        int tempNumPoints = (int)Random.Range(10, 30);
        int tempNumControlPoints = (int)Random.Range(8, 20);

        float tempStartWidth = Random.Range(3, 1);
        float tempEndWidth = Random.Range(1, 2);
        float tempGrowingMultiplierM0 = Random.Range(0.1f, 0.5f);
        float tempGrowingMultiplierM1 = Random.Range(0.1f, 0.4f);

        TigeControlHierarchie modeBranch = TigeControlHierarchie.Primaire;

        tempControlPointsBranche = generateControlPoints(modeBranch, tempNumControlPoints, tempOriginBranch);
        branch.setupPlant(tempControlPointsBranche, Color.red, Color.magenta, tempStartWidth, tempEndWidth, tempNumPoints, tempNumControlPoints, tempNumControlPoints * 2, tempGrowingMultiplierM0, tempGrowingMultiplierM1, lineRendererBranch, false);

        // TIGE
        for (int i = 0; i < nTiges; i++)
        {
            countTige.Add(0.0f);

            test = new GameObject();

            test.transform.parent = transform;
            tige = test.AddComponent<MakeBranch>();

            lineRendererTige = test.AddComponent<LineRenderer>();
            indexBranch = Random.Range(15, branch.numberOfAllPoints);
            tempOriginTige = branch.getAllPoints()[indexBranch];
            int decideMode = Mathf.RoundToInt(Random.Range(0, 2));

            int tempNumPointsTige = (int)Random.Range(4, 6);
            int tempNumControlPointsTige = (int)Random.Range(3, 8);

            float tempStartWidthTige = Random.Range(2, 3);
            float tempEndWidthTige = Random.Range(1, 3);
            float tempGrowingMultiplierM0Tige = Random.Range(0.1f, 0.6f);
            float tempGrowingMultiplierM1Tige = Random.Range(0.1f, 0.6f);

            TigeControlHierarchie modeTige;

            if (decideMode == 0 || decideMode == 2)
            {
                modeTige = TigeControlHierarchie.SecondaireGauche;
                tempControlPointsTige = generateControlPoints(modeTige, tempNumControlPointsTige, tempOriginTige);
            }
            else if (decideMode == 1 )
            {
                modeTige = TigeControlHierarchie.SecondaireDroite;
                tempControlPointsTige = generateControlPoints(modeTige, tempNumControlPointsTige, tempOriginTige);
            }

            tige.setupPlant(tempControlPointsTige, Color.magenta, Color.green, tempStartWidthTige, tempEndWidthTige, tempNumPointsTige, tempNumControlPointsTige, tempNumControlPointsTige * 2, tempGrowingMultiplierM0Tige, tempGrowingMultiplierM1Tige, lineRendererTige, false);

            tige.indexOrigin = indexBranch;

            tests.Add(test);

            tiges.Add(tige);
        }
        
    }

    //// Update is called once per frame
    void Update()
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

            //////////////
            for (int i = 0; i < nTiges; i++)
            {
                if (branch.lineRenderermmm.positionCount >= tiges[i].indexOrigin)
                {
                    //test.SetActive(true);
                    //tiges[i].GetComponent<GameObject>.test;
                    tests[i].SetActive(true);

                    if (tiges[i].begin == false)
                    {
                        countTige[i] = Time.time;
                        tiges[i].begin = true;
                    }

                    if (tiges[i].begin == true)
                    {
                        if(tiges[i].timer >= 1)
                        {
                            tiges[i].timer = 1;
                        }
                        else
                        {
                            tiges[i].timer = (Time.time - countTige[i]) / tiges[i].growingTime;
                            tiges[i].updatePlant();
                        }
                        
                    }
                }
            }
                
            //else test.SetActive(false);
            Debug.Log(branch.lineRenderermmm.positionCount);
        }
    }

    public List<Vector3> generateControlPoints(TigeControlHierarchie m_mode, int m_numControlPoints, Vector3 m_origin)
    {
        List<Vector3> temp = new List<Vector3>();
        Vector3 tempOrigin = new Vector3();
        tempOrigin = m_origin;
        temp.Add(tempOrigin);

        if (m_mode == TigeControlHierarchie.Primaire)
        {
            Vector3 tempFirst = new Vector3();
            tempFirst = new Vector3(tempOrigin.x, tempOrigin.y + 3, tempOrigin.z);
            temp.Add(tempFirst);

            for (int i = 2; i < m_numControlPoints - 1; i++)
            {
                float tempX = tempOrigin.x + Random.Range(-10, 10);
                float tempY = tempOrigin.y + Random.Range((1/ m_numControlPoints) + i * 8, (1 / m_numControlPoints) + i * 10);
                Vector3 temptemp = new Vector3(tempX, tempY, tempOrigin.z);
                temp.Add(temptemp);
                if (i == m_numControlPoints-2)
                {
                    fleursInfosPrincipale.Add(temptemp);
                }
            }
        }
        else if (m_mode == TigeControlHierarchie.SecondaireDroite)
        {
            Vector3 tempFirst = new Vector3();
            tempFirst = new Vector3(tempOrigin.x + 2, tempOrigin.y + 1, tempOrigin.z);
            temp.Add(tempFirst);

            for (int i = 2; i < m_numControlPoints - 1; i++)
            {
                float tempX = tempOrigin.x + Random.Range(((1 / m_numControlPoints)) + i * 5, ((1 / m_numControlPoints)) + i *  9);
                float tempY = tempOrigin.y + Random.Range(-2, i *  5);
                Vector3 temptemp = new Vector3(tempX, tempY, tempOrigin.z);
                temp.Add(temptemp);
                if (i == m_numControlPoints - 2)
                {
                    fleursPosTiges.Add(temptemp);
                }
            }
        }

        else if (m_mode == TigeControlHierarchie.SecondaireGauche)
        {
            Vector3 tempFirst = new Vector3();
            tempFirst = new Vector3(tempOrigin.x - 2, tempOrigin.y + 1, tempOrigin.z);
            temp.Add(tempFirst);

            for (int i = 2; i < m_numControlPoints - 1; i++)
            {
                float tempX = tempOrigin.x - Random.Range(((1 / m_numControlPoints)) + i * 5, ((1 / m_numControlPoints)) + i * 9);
                float tempY = tempOrigin.y + Random.Range(-2, i * 5);
                Vector3 temptemp = new Vector3(tempX, tempY, tempOrigin.z);
                temp.Add(temptemp);
                if (i == m_numControlPoints - 2)
                {
                    fleursPosTiges.Add(temptemp);
                }
            }
        }
        
        return temp;
    }

    public List<Vector3> getFleursPosPrincipale()
    {
        return fleursPosPrincipale;
    }

    public List<Vector3> getFleursPosTige()
    {
        return fleursPosTiges;
    }

    public List<Vector3> getFeuillesPos()
    {
        feuillesPos.AddRange(tempControlPointsBranche);
        feuillesPos.AddRange(tempControlPointsTige);
        return feuillesPos;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakePlants : MonoBehaviour
{
    public float lifeTime;

    // BRANCH
    MakeBranch branch;
    public LineRenderer lineRendererBranch;
    public float countBranch;
    public Vector3 originBranch = new Vector3();
    int type;

    // TIGE
    int nTiges = 6;
    List<MakeBranch> tiges = new List<MakeBranch>();
    public GameObject obj;
    List<GameObject> objs = new List<GameObject>();
    MakeBranch tige;
    public LineRenderer lineRendererTige;
    public List<float> countTige = new List<float>();

    Vector3 tempOriginTige;
    public int indexBranch;

    // SPRITES
    private List<int> fleursIndexPrincipale = new List<int>();
    private List<int> fleursIndexTiges = new List<int>();
    private List<int> feuillesIndex = new List<int>();

    List<Vector3> tempControlPointsBranche = new List<Vector3>();
    List<Vector3> tempControlPointsTige = new List<Vector3>();

    public void setupPlant(Vector3 m_originBranch, int m_type)
    {
        lifeTime = Random.Range(8.0f, 20.0f);

        // BRANCH
        type = m_type;
        branch = gameObject.AddComponent<MakeBranch>();
        lineRendererBranch = gameObject.GetComponent<LineRenderer>();
        originBranch = m_originBranch;
        int tempNumPoints = (int)Random.Range(10, 20);
        int tempNumControlPoints = (int)Random.Range(4, 20);

        float tempStartWidth = Random.Range(7, 4);
        float tempEndWidth = Random.Range(1, 2);
        float tempGrowingMultiplierM0 = Random.Range(0.1f, 0.5f);
        float tempGrowingMultiplierM1 = Random.Range(0.1f, 0.4f);

        float redS = 0.5f + Random.Range(-0.3f, 0.3f);
        float greenS = 0.5f + Random.Range(-0.3f, 0.3f);
        float blueS = 0.5f + Random.Range(-0.3f, 0.3f);
        float redE = 0.5f + Random.Range(-0.3f, 0.3f);
        float greenE = 0.5f + Random.Range(-0.3f, 0.3f);
        float blueE = 0.5f + Random.Range(-0.3f, 0.3f);

        Color startColor = new Color(redS, greenS, blueS, 255);
        Color endColor = new Color(redE, greenE, blueE, 255);

        TigeControlHierarchie modeBranch = TigeControlHierarchie.Primaire;

        tempControlPointsBranche = generateControlPoints(modeBranch, tempNumControlPoints, originBranch);

        branch.setupPlant(tempControlPointsBranche, fleursIndexPrincipale, feuillesIndex, startColor, endColor, tempStartWidth, tempEndWidth, tempNumPoints, tempNumControlPoints, tempNumControlPoints * 2, tempGrowingMultiplierM0, tempGrowingMultiplierM1, lineRendererBranch, false, type);

        // TIGE
        for (int i = 0; i < nTiges; i++)
        {
            countTige.Add(0.0f);

            obj = new GameObject();

            obj.transform.parent = transform;
            tige = obj.AddComponent<MakeBranch>();

            lineRendererTige = obj.GetComponent<LineRenderer>();
            indexBranch = Random.Range(15, branch.numberOfAllPoints-15);
            tempOriginTige = branch.getAllPoints()[indexBranch];
            int decideMode = Random.Range(0, 2);

            int tempNumPointsTige = Random.Range(10, 15);
            int tempNumControlPointsTige = Random.Range(3, 6);

            float tempStartWidthTige = Random.Range(2, 3);
            float tempEndWidthTige = Random.Range(0.5f, 2.0f);
            float tempGrowingMultiplierM0Tige = Random.Range(0.1f, 0.6f);
            float tempGrowingMultiplierM1Tige = Random.Range(0.1f, 0.6f);

            TigeControlHierarchie modeTige;

            if (decideMode == 0 || decideMode == 2)
            {
                modeTige = TigeControlHierarchie.SecondaireGauche;
                tempControlPointsTige = generateControlPoints(modeTige, tempNumControlPointsTige, tempOriginTige);
            }
            else if (decideMode == 1)
            {
                modeTige = TigeControlHierarchie.SecondaireDroite;
                tempControlPointsTige = generateControlPoints(modeTige, tempNumControlPointsTige, tempOriginTige);
            }

            tige.setupPlant(tempControlPointsTige, fleursIndexTiges, feuillesIndex, startColor, endColor, tempStartWidthTige, tempEndWidthTige, tempNumPointsTige, tempNumControlPointsTige, tempNumControlPointsTige * 2, tempGrowingMultiplierM0Tige, tempGrowingMultiplierM1Tige, lineRendererTige, false, type);

            tige.indexOrigin = indexBranch;

            objs.Add(obj);

            tiges.Add(tige);
        }
    }

    public void Update()
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

            //////////////
            for (int i = 0; i < nTiges; i++)
            {
                if (branch.lineRenderermmm.positionCount >= tiges[i].indexOrigin)
                {
                    objs[i].SetActive(true);

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
        }
    }

    public List<Vector3> generateControlPoints(TigeControlHierarchie m_mode, int m_numControlPoints, Vector3 m_origin)
    {
        List<Vector3> temp = new List<Vector3>();
        Vector3 tempOrigin = new Vector3();
        tempOrigin = m_origin;
        fleursIndexTiges = new List<int>();
        feuillesIndex = new List<int>();
        temp.Add(tempOrigin);

        if (m_mode == TigeControlHierarchie.Primaire)
        {
            Vector3 tempFirst = new Vector3();
            tempFirst = new Vector3(tempOrigin.x, tempOrigin.y + 3, tempOrigin.z);
            temp.Add(tempFirst);
            
            switch (type)
            {
                case 1: // VIOLETTE
                    for (int i = 2; i < m_numControlPoints - 1; i++)
                    {
                        float tempX = tempOrigin.x + Random.Range(-2, 2);
                        float tempY = tempOrigin.y + Random.Range((1 / m_numControlPoints) + i * 8, (1 / m_numControlPoints) + i * 10);
                        Vector3 temptemp = new Vector3(tempX, tempY, tempOrigin.z);
                        temp.Add(temptemp);
                        if (i % 2 == 0)
                        {
                            feuillesIndex.Add(i);
                        }
                    }
                    fleursIndexPrincipale.Add(m_numControlPoints-2);
                    break;

                case 2: // FUSHIA
                    for (int i = 2; i < m_numControlPoints - 1; i++)
                    {
                        float tempX2 = tempOrigin.x + Random.Range(-1, 1);
                        float tempY2 = tempOrigin.y + Random.Range((1 / m_numControlPoints) + i * 8, (1 / m_numControlPoints) + i * 10);
                        Vector3 temptemp2 = new Vector3(tempX2, tempY2, tempOrigin.z);
                        temp.Add(temptemp2);
                        if (i % 2 == 0)
                        {
                            feuillesIndex.Add(i);
                        }
                    }
                    fleursIndexPrincipale.Add(m_numControlPoints - 2);
                    break;

                case 3:
                    for (int i = 2; i < m_numControlPoints - 1; i++)
                    {
                        float tempX3 = tempOrigin.x + Random.Range(-5, 5);
                        float tempY3 = tempOrigin.y + Random.Range((1 / m_numControlPoints) + i * 10, (1 / m_numControlPoints) + i * 12);
                        Vector3 temptemp3 = new Vector3(tempX3, tempY3, tempOrigin.z);
                        temp.Add(temptemp3);
                        if (i % 2 == 0)
                        {
                            feuillesIndex.Add(i);
                        }
                    }
                    fleursIndexPrincipale.Add(m_numControlPoints - 2);
                    break;

                case 4:
                    for (int i = 2; i < m_numControlPoints - 1; i++)
                    {
                        float tempX4 = tempOrigin.x + Random.Range(-14, 14);
                        float tempY4 = tempOrigin.y + Random.Range((1 / m_numControlPoints) + i * 8, (1 / m_numControlPoints) + i * 10);
                        Vector3 temptemp4 = new Vector3(tempX4, tempY4, tempOrigin.z);
                        temp.Add(temptemp4);
                        if (i % 2 == 0)
                        {
                            feuillesIndex.Add(i);
                        }
                    }
                    fleursIndexPrincipale.Add(m_numControlPoints - 2);
                    break;
            }
        }
        else if (m_mode == TigeControlHierarchie.SecondaireDroite)
        {
            Vector3 tempFirst = new Vector3();
            tempFirst = new Vector3(tempOrigin.x + 2, tempOrigin.y + 1, tempOrigin.z);
            temp.Add(tempFirst);

            switch (type)
            {
                case 1: // VIOLETTE
                    for (int i = 2; i < m_numControlPoints - 1; i++)
                    {
                        float tempX = tempOrigin.x + Random.Range(((1 / m_numControlPoints)) + i * 2, ((1 / m_numControlPoints)) + i * 4);
                        float tempY = tempOrigin.y + Random.Range(i, i * 5);
                        Vector3 temptemp = new Vector3(tempX, tempY, tempOrigin.z);
                        temp.Add(temptemp);
                        //if (i % 2 == 0)
                        //{
                        //    feuillesIndex.Add(i);
                        //}
                    }
                    //fleursIndexTiges.Add(m_numControlPoints - 2);
                    feuillesIndex.Add(m_numControlPoints - 2);
                    break;

                case 2: // FUSHIA
                    for (int i = 2; i < m_numControlPoints - 1; i++)
                    {
                        float tempX = tempOrigin.x + Random.Range(((1 / m_numControlPoints)) + i * 2, ((1 / m_numControlPoints)) + i * 4);
                        float tempY = tempOrigin.y + Random.Range(i, i * 5);
                        Vector3 temptemp = new Vector3(tempX, tempY, tempOrigin.z);
                        temp.Add(temptemp);
                    }
                    int r = Random.Range(0,2);
                    if (r == 0)
                    {
                        feuillesIndex.Add(m_numControlPoints - 2);
                    }
                    else if (r == 1)
                    {
                        fleursIndexTiges.Add(m_numControlPoints - 2);
                    }
                    break;

                case 3: 
                    for (int i = 2; i < m_numControlPoints - 1; i++)
                    {
                        float tempX = tempOrigin.x + Random.Range(((1 / m_numControlPoints)) + i * 2, ((1 / m_numControlPoints)) + i * 4);
                        float tempY = tempOrigin.y + Random.Range(i, i * 5);
                        Vector3 temptemp = new Vector3(tempX, tempY, tempOrigin.z);
                        temp.Add(temptemp);
                        if (i % 2 == 0)
                        {
                            feuillesIndex.Add(i);
                        }
                    }
                    fleursIndexTiges.Add(m_numControlPoints - 2);
                    //feuillesIndex.Add(m_numControlPoints - 2);
                    break;

                case 4: 
                    for (int i = 2; i < m_numControlPoints - 1; i++)
                    {
                        float tempX = tempOrigin.x + Random.Range(((1 / m_numControlPoints)) + i * 2, ((1 / m_numControlPoints)) + i * 4);
                        float tempY = tempOrigin.y + Random.Range(i, i * 5);
                        Vector3 temptemp = new Vector3(tempX, tempY, tempOrigin.z);
                        temp.Add(temptemp);
                        if (i % 2 == 0)
                        {
                            feuillesIndex.Add(i);
                        }
                    }
                    fleursIndexTiges.Add(m_numControlPoints - 2);
                    //feuillesIndex.Add(m_numControlPoints - 2);
                    break;
            }
        }

        else if (m_mode == TigeControlHierarchie.SecondaireGauche)
        {
            Vector3 tempFirst = new Vector3();
            tempFirst = new Vector3(tempOrigin.x - 2, tempOrigin.y + 1, tempOrigin.z);
            temp.Add(tempFirst);

            switch (type)
            {
                case 1: // VIOLETTE
                    for (int i = 2; i < m_numControlPoints - 1; i++)
                    {
                        float tempX = tempOrigin.x - Random.Range(((1 / m_numControlPoints)) + i * 5, ((1 / m_numControlPoints)) + i * 9);
                        float tempY = tempOrigin.y + Random.Range(-2, i * 5);
                        Vector3 temptemp = new Vector3(tempX, tempY, tempOrigin.z);
                        temp.Add(temptemp);
                        //if (i % 2 == 0)
                        //{
                        //    feuillesIndex.Add(i);
                        //}
                    }
                    //fleursIndexTiges.Add(m_numControlPoints - 2);
                    feuillesIndex.Add(m_numControlPoints - 2);
                    break;
                case 2: // FUSHIA
                    for (int i = 2; i < m_numControlPoints - 1; i++)
                    {
                        float tempX = tempOrigin.x - Random.Range(((1 / m_numControlPoints)) + i * 5, ((1 / m_numControlPoints)) + i * 9);
                        float tempY = tempOrigin.y + Random.Range(-2, i * 5);
                        Vector3 temptemp = new Vector3(tempX, tempY, tempOrigin.z);
                        temp.Add(temptemp);
                    }
                    int r = Random.Range(0, 2);
                    if (r == 0)
                    {
                        feuillesIndex.Add(m_numControlPoints - 2);
                    }
                    else if (r == 1)
                    {
                        fleursIndexTiges.Add(m_numControlPoints - 2);
                    }
                    break;
                case 3:
                    for (int i = 2; i < m_numControlPoints - 1; i++)
                    {
                        float tempX = tempOrigin.x - Random.Range(((1 / m_numControlPoints)) + i * 5, ((1 / m_numControlPoints)) + i * 9);
                        float tempY = tempOrigin.y + Random.Range(-2, i * 5);
                        Vector3 temptemp = new Vector3(tempX, tempY, tempOrigin.z);
                        temp.Add(temptemp);
                        if (i % 2 == 0)
                        {
                            feuillesIndex.Add(i);
                        }
                    }
                    fleursIndexTiges.Add(m_numControlPoints - 2);
                    break;
                case 4:
                    for (int i = 2; i < m_numControlPoints - 1; i++)
                    {
                        float tempX = tempOrigin.x - Random.Range(((1 / m_numControlPoints)) + i * 5, ((1 / m_numControlPoints)) + i * 9);
                        float tempY = tempOrigin.y + Random.Range(-2, i * 5);
                        Vector3 temptemp = new Vector3(tempX, tempY, tempOrigin.z);
                        temp.Add(temptemp);
                        if (i % 2 == 0)
                        {
                            feuillesIndex.Add(i);
                        }
                    }
                    fleursIndexTiges.Add(m_numControlPoints - 2);
                    break;
            }
        }
        
        return temp;
    }
}

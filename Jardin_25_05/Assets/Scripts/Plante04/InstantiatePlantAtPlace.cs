using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiatePlantAtPlace : MonoBehaviour
{
    public bool received;
    public Vector3 origin, origin1, origin2, origin3;
    public GameObject branche;
    public GameObject lsys;
    private GameObject obj;
    List<GameObject> plants = new List<GameObject>();
    List<Vector3> pointsReceived = new List<Vector3>();


    // Use this for initialization
    void Start ()
    {
        pointsReceived.Add(origin1);
        pointsReceived.Add(origin2);
        pointsReceived.Add(origin3);
    }

    // Update is called once per frame
    void Update ()
    {
        if (received)
        {
            int rand = Random.Range(1, 8);

            for (int i = 0; i < pointsReceived.Count; i++)
            {
                origin = pointsReceived[i];
                createPlant(origin, rand);
            }
            received = false;
        }
    }

    public void createPlant(Vector3 m_origin, int m_type)
    {

        switch (m_type)
        {
            case 1: // VIOLETTE
                obj = (GameObject)Instantiate(branche);
                obj.GetComponent<MakePlants>().setupPlant(m_origin, 1);
                obj.SetActive(true);
                plants.Add(obj);
                break;
            case 2: // BRANCH
                obj = (GameObject)Instantiate(branche);
                obj.GetComponent<MakePlants>().setupPlant(m_origin, 2);
                obj.SetActive(true);
                plants.Add(obj);
                break;
            case 3: // BRANCH
                obj = (GameObject)Instantiate(branche);
                obj.GetComponent<MakePlants>().setupPlant(m_origin, 3);
                obj.SetActive(true);
                plants.Add(obj);
                break;
            case 4: // BRANCH
                obj = (GameObject)Instantiate(branche);
                obj.GetComponent<MakePlants>().setupPlant(m_origin, 4);
                obj.SetActive(true);
                plants.Add(obj);
                break;
            case 5: // LSYS
                obj = (GameObject)Instantiate(lsys);
                obj.GetComponent<makeBrancheLsystem>().setupPlant(m_origin, 5);
                obj.SetActive(true);
                plants.Add(obj);
                break;
            case 6: // LSYS
                obj = (GameObject)Instantiate(lsys);
                obj.GetComponent<makeBrancheLsystem>().setupPlant(m_origin, 6);
                obj.SetActive(true);
                plants.Add(obj);
                break;
            case 7: // LSYS
                obj = (GameObject)Instantiate(lsys);
                obj.GetComponent<makeBrancheLsystem>().setupPlant(m_origin, 7);
                obj.SetActive(true);
                plants.Add(obj);
                break;
        }

    }

    public GameObject getPlant(int m_index)
    {
        return plants[m_index];
    }

}

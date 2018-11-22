using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiatePlantAtPlace : MonoBehaviour
{
    public bool received;
    ReceivePosition receivePos;
    public ParticleSystem ps;
    public Vector3 origin;
    public GameObject branche;
    public GameObject lsys;
    private GameObject obj;
    public GameObject prefabBuisson;
    private List<GameObject> buissons = new List<GameObject>();
    List<GameObject> plants = new List<GameObject>();
    List<float> xReceived = new List<float>();
    List<float> yReceived = new List<float>();

    // Use this for initialization
    void Start()
    {
        //pointsReceived.Add(origin1);
        //pointsReceived.Add(origin2);
        //pointsReceived.Add(origin3);
        receivePos = GetComponent<ReceivePosition>();
        origin = new Vector3(0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        destroyPlante();
        xReceived = receivePos.getListX();
        yReceived = receivePos.getListY();
        //Debug.Log(xReceived.Count);
        if (xReceived.Count != 0)
        {
            //transform.position = receivePos.receivedPos;
            //origin = transform.position;
            //Debug.Log("lala");
            //Debug.Log("Received Count = " + xReceived.Count);
            received = true;
        }
        if (received)
        {
            int rand = Random.Range(1, 8);
            
            for (int i = 0; i < xReceived.Count; i++)
            {
                origin = new Vector3(xReceived[i], 0.0f, yReceived[i]);
                if (i % 4 == 0)
                { 
                    createPlant(origin, rand);
                }
                else
                {
                    int randBuisson = Random.Range(8, 10);
                    createPlant(origin, randBuisson);
                }
            }
            received = false;
            receivePos.clearListes();
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
            case 8: // BUISSON
                //obj = (GameObject)Instantiate(buisson);
                //obj.GetComponent<makeBrancheLsystem>().setupPlant(m_origin, 7);
                float angleB = 0.0f;
                float sizeB = Random.Range(20.0f, 40.0f);
                int indexB = 1;
                createBuisson(m_origin, new Vector3(angleB, sizeB, indexB));
                obj.SetActive(true);
                buissons.Add(obj);
                break;
            case 9: // BUISSON
                float angleB2 = 0.0f;
                float sizeB2 = Random.Range(20.0f, 40.0f);
                int indexB2 = 2;
                createBuisson(m_origin, new Vector3(angleB2, sizeB2, indexB2));
                obj.SetActive(true);
                buissons.Add(obj);
                break;

        }

    }

    public GameObject getPlant(int m_index)
    {
        return plants[m_index];
    }

    public void createBuisson(Vector3 pos, Vector3 info) //in info x: angle, y: size, z: index
    {
        obj = (GameObject)Instantiate(prefabBuisson);
        obj.AddComponent<lifetimeBuisson>();
        //lifetimeBuisson ltb = new lifetimeBuisson();
        obj.transform.position = new Vector3(pos.x, pos.y, pos.z - 1.5f); ;
        obj.transform.rotation = Quaternion.Euler(new Vector3(0.0f, 0.0f, info.x));
        obj.transform.localScale = obj.transform.localScale * info.y;
        obj.GetComponent<SpriteRenderer>().sortingOrder = Random.Range(0, 10922); //To avoid overlaping
        Animator anim = obj.GetComponent<Animator>();
        anim.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("Textures/buisson" + (int)info.z);
        obj.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Textures/buisson" + (int)info.z);
    }

    public void destroyPlante()
    {
        if (plants.Count != 0)
        {
            for (int i = 0; i < plants.Count; i++)
            {
                if (plants[i].GetComponent<MakePlants>())
                {
                    if (plants[i].GetComponent<MakePlants>().lifeTime <= 0)
                    {
                        //Debug.Log("DESTROOOOOOOOOOOOOOOOOY");
                        ps = (ParticleSystem)Instantiate(ps);
                        Vector3 p = new Vector3(plants[i].GetComponent<MakePlants>().originBranch.x, plants[i].GetComponent<MakePlants>().originBranch.y + 60, plants[i].GetComponent<MakePlants>().originBranch.z);

                        ps.transform.position = p;
                        ps.gravityModifier = Random.Range(-10.2f, 10.0f);
                        ps.Play();
                        Destroy(plants[i]);
                        plants.RemoveAt(i);
                    }
                    break;
                }
                if (plants[i].GetComponent<makeBrancheLsystem>())
                {
                    if (plants[i].GetComponent<makeBrancheLsystem>().lifeTime <= 0)
                    {
                        //Debug.Log("DESTROOOOOOOOOOOOOOOOOY");
                        ps = (ParticleSystem)Instantiate(ps);
                        Vector3 p = new Vector3(plants[i].GetComponent<makeBrancheLsystem>().origin.x, plants[i].GetComponent<makeBrancheLsystem>().origin.y + 60, plants[i].GetComponent<makeBrancheLsystem>().origin.z);

                        ps.transform.position = p;
                        ps.gravityModifier = Random.Range(-10.2f, 10.0f);

                        ps.Play();
                        Destroy(plants[i]);
                        plants.RemoveAt(i);
                    }
                    break;
                }
            }
        }
        if (buissons.Count != 0)
        {
            for (int i = 0; i < buissons.Count; i++)
            {
                if (buissons[i].GetComponent<lifetimeBuisson>().lifetime <= 0)
                {
                    ps = (ParticleSystem)Instantiate(ps);
                    Vector3 p = new Vector3(buissons[i].transform.position.x, buissons[i].transform.position.y + 60, buissons[i].transform.position.z);

                    ps.transform.position = p;
                    ps.gravityModifier = Random.Range(-10.2f, 10.0f);
                    ps.Play();
                    Destroy(buissons[i]);
                    buissons.RemoveAt(i);
                }
            }
        }
    }
}

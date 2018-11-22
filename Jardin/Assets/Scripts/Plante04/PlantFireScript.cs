using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantFireScript : MonoBehaviour
{


    // Use this for initialization
    void Start()
    {
        //for (int i = 0; i < 20; i++)
        //{
            Fire();
        //}
	}
	
	// Update is called once per frame
	void Fire ()
    {
        GameObject obj = ObjectPoolerScript.current.GetPooledObject();
        if (obj == null) return;
        obj.SetActive(true);
	}
}

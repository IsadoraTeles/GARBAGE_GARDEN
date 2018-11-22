using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LsysFireScript : MonoBehaviour
{

	// Use this for initialization
	void Start ()
    {
        Fire();
    }
	
	// Update is called once per frame
	void Fire()
    {
        GameObject obj = ObjectPoolerScript.current.GetPooledObject();
        if (obj == null) return;
        obj.SetActive(true);
    }
}

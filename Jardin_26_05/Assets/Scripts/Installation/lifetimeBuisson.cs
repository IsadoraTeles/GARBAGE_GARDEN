using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lifetimeBuisson : MonoBehaviour {

    public float lifetime;
	// Use this for initialization
	void Start ()
    {
        lifetime = Random.Range(15.0f, 40.0f);
	}
	
	// Update is called once per frame
	void Update () {
        lifetime -= 0.01f;
	}
}

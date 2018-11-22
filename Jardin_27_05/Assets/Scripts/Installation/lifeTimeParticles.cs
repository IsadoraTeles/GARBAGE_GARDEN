using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lifeTimeParticles : MonoBehaviour {

    public float lifeTime;
    public bool die = false;
	// Use this for initialization
	void Start ()
    {
        lifeTime = 2;
	}
	
	// Update is called once per frame
	void Update ()
    {
        lifeTime -= 0.01f;
        if (lifeTime <= 0)
        {
            Destroy(gameObject);
        }
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantDestroyScript : MonoBehaviour
{
	void OnEnable ()
    {
        Invoke("Destroy", 2f);
	}

    void Destroy ()
    {
        gameObject.SetActive(false);
	}

    private void OnDisable()
    {
        CancelInvoke();
    }
}

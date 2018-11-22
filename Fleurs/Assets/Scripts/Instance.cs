using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Instance : MonoBehaviour {

    public GameObject prefab;
    private GameObject clone;

	// Use this for initialization
    void Start () {
        for(int i = 0; i<1000; i++)
        {
            float randomX = Random.Range(-10.0f, 10.0f);
            float randomY = Random.Range(-10.0f, 10.0f);
            float randomAngle = Random.Range(-60.0f, 60.0f);
            float size = Random.Range(1.0f, 1.0f);
            int index = Random.Range(1, 4); //Random.Range(1, 3) return 1 or 2
            createLeaf(new Vector3(randomX, randomY, 0.0f), new Vector3(randomAngle, size, index));
        }                   
    }    	
    void createLeaf(Vector3 pos, Vector3 info)
    {
        //info contains x: angleZ, y: size; z: indexSprite
        if (Resources.Load<RuntimeAnimatorController>("Textures/fleur" + (int)info.z))
        {
            clone = (GameObject)Instantiate(prefab);
            //transform
            clone.transform.parent = transform; //put clone as child
            clone.transform.localPosition = pos;        
            clone.transform.localRotation = Quaternion.Euler(new Vector3(0.0f, 0.0f, info.x));
            clone.transform.localScale = clone.transform.localScale * info.y;
            clone.GetComponent<SpriteRenderer>().sortingOrder = Random.Range(0, 32768); //To avoid overlaping
            Animator anim = clone.GetComponent<Animator>();     
            anim.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("Textures/fleur" + (int)info.z);
            clone.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Textures/fleur" + (int)info.z);
        }       
        //Pour les sprites de plantes
        //clone.GetComponent<SpriteRenderer>().sprite = Resources.Load <Sprite> ("Textures/leaf" + (int)info.z);
    }
}

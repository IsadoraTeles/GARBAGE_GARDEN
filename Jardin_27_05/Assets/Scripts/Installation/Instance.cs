using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Instance : MonoBehaviour
{

    public GameObject prefab;
    private GameObject clone;

	public void createLeaf(Vector3 pos, Vector3 info) //in info x: angle, y: size, z: index
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

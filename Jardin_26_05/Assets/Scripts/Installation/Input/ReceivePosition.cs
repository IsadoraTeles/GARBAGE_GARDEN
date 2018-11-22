using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class ReceivePosition : MonoBehaviour
{
    
   	public OSC osc;
    public Vector3 receivedPos;
    private List<float> listeX = new List<float>();
    private List<float> listeY = new List<float>();

    // Use this for initialization
    void Start ()
    {
	   osc.SetAddressHandler( "/CubeXYZ" , OnReceiveXYZ );
        for(int i = 0; i<17; i++)
        {
            osc.SetAddressHandler("/pos_x" + i, OnReceiveX);
            osc.SetAddressHandler("/pos_y" + i, OnReceiveY);
        }     
       //osc.SetAddressHandler("/CubeZ", OnReceiveZ);
    }
	
	// Update is called once per frame
	void Update ()
    {
        
    }

    public List<float> getListX()
    {
        return listeX;
    }

    public List<float> getListY()
    {
        return listeY;
    }

    public void clearListes()
    {
        listeX.Clear();
        listeY.Clear();
    }

    void OnReceiveXYZ(OscMessage message)
    {
		//float x = message.GetFloat(0);
  //      float y = message.GetFloat(1);
		//float z = message.GetFloat(2);
        
        //transform.position = new Vector3(x,y,z);
        //receivedPos = new Vector3(x, y, z);

    }

    void OnReceiveX(OscMessage message)
    {
        float x = message.GetFloat(0);
        x = map(x, 0, 30, -340, 340);
        listeX.Add(x);
        //Debug.Log("Coucou = " + listeX.Count);
        //Vector3 position = transform.position;

        //position.x = x;

        //Debug.Log("coucouX = " + x);
        //transform.position = position;
    }

    void OnReceiveY(OscMessage message)
    {
        float y = message.GetFloat(0);
        y = map(y, 0, 20, -370, 370);
        listeY.Add(y);
        //Vector3 position = transform.position;
        ////Debug.Log("coucouY = " + y);
        //position.z = y;

        //transform.position = position;
    }

    public float map (float value, float min1, float max1, float min2, float max2)
    {
        // Convert the current value to a percentage
        // 0% - min1, 100% - max1
        float perc = (value - min1) / (max1 - min1);

        // Do the same operation backwards with min2 and max2
        float result = perc * (max2 - min2) + min2;

        return result;
    }
}

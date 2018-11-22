using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplineDecorator : MonoBehaviour
{
    public BezierSpline spline;
    public int frequency;
    public bool lookForward;
    public Transform[] items;
    public float counterX = 0;
    public float counterZ = 360;


    private void Awake()
    {
        if(frequency <= 0 || items == null || items.Length == 0)
        {
            return;
        }
        float stepSize = 1f / (frequency * items.Length);
        /*
        if (spline.Loop || stepSize == 1)
        {
            stepSize = 1f / stepSize;
        }
        else
        {
            stepSize = 1f / (stepSize - 1);
        }
        */
        for (int p = 0, f = 0; f < frequency; f ++)
        {
            for(int i = 0; i < items.Length; i++, p++)
            {
                Transform item = Instantiate(items[i]) as Transform;
                Vector3 position = spline.GetPointCubic(p * stepSize);
                item.transform.localPosition = position;
                //item.transform.Rotate(0, Time.time, 0);
                if(lookForward)
                {
                    item.transform.LookAt(position + spline.GetDirectionCubic(p * stepSize));
                    Vector3 axisX = new Vector3(1, 0, 0);
                    Vector3 axisZ = new Vector3(0, 0, 1);

                    item.RotateAround(position, axisX, counterX);
                    item.RotateAround(position, axisZ, counterZ);
                }
                item.transform.parent = transform;
                counterX += 15;
            }
            counterZ--;
        }
        
    }

    private void Update()
    {
        //counterX += Time.time * 10;
    }

}

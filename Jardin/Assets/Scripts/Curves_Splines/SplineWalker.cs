using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplineWalker : MonoBehaviour
{
    public BezierSpline spline;
    public float duration;
    private float progress;
    public bool lookForward;

    public SplineWalkerMode mode;
    private bool goingForward = true;

    // DECORATOR
    public Transform[] items;
    public int frequency;


    // Use this for initialization
    void Start ()
    {
		
	}

    // Update is called once per frame
    void Update ()
    {

        Vector3 position = spline.GetPointCubic(progress);
        transform.localPosition = spline.GetPointCubic(progress);

        if (lookForward)
        {
            transform.LookAt(position + spline.GetDirectionCubic(progress));
        }

        if (frequency <= 0 || items == null || items.Length == 0)
        {
            return;
        }

        float stepSize = 1f / (frequency * items.Length);

        for (int p = 0, f = 0; f < frequency; f++)
        {
            for (int i = 0; i < items.Length; i++, p++)
            {
                Transform item = Instantiate(items[i]) as Transform;
                //Vector3 pos = spline.GetPointCubic(p * stepSize);
                //item.transform.localPosition = position;
                //item.transform.Rotate(0, Time.time, 0);
                if (lookForward)
                {
                   // item.transform.LookAt(position + spline.GetDirectionCubic(p * stepSize));
                    Vector3 axisX = new Vector3(1, 0, 0);
                    Vector3 axisZ = new Vector3(0, 0, 1);

                    //item.RotateAround(pos, axisX, counterX);
                    //item.RotateAround(pos, axisZ, counterZ);
                }
                item.transform.parent = transform;
                //counterX += 15;
            }
            //counterZ--;
        }





        if (goingForward)
        {
            progress += Time.deltaTime / duration;


            if (progress > 1f)
            {
                if (mode == SplineWalkerMode.Once)
                {
                    progress = 1f;
                }
                else if (mode == SplineWalkerMode.Loop)
                {
                    progress -= 1f;
                }
                else
                {
                    progress = 2f - progress;
                    goingForward = false;
                }
            }

            


        }
        else
        {
            progress -= Time.deltaTime / duration;
            if (progress < 0f)
            {
                progress = -progress;
                goingForward = true;
            }
        }

   
        
	}
}

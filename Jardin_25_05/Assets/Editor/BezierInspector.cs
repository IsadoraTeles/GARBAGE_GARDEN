using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(BezierCurve))]
public class BezierInspector : Editor
{
    private BezierCurve curve;
    private Transform handleTransform;
    private Quaternion handleRotation;

    private const int lineSteps = 10;

    private const float directionScale = 10f;

    private void OnSceneGUI()
    {
        curve = target as BezierCurve;
        handleTransform = curve.transform;
        handleRotation = Tools.pivotRotation == PivotRotation.Local ? handleTransform.rotation : Quaternion.identity;

        Vector3 p0 = ShowPoint(0);
        Vector3 p1 = ShowPoint(1);
        Vector3 p2 = ShowPoint(2);
        Vector3 p3 = ShowPoint(3);

        Handles.color = Color.gray;
        Handles.DrawLine(p0, p1);
        Handles.DrawLine(p1, p2);
        Handles.DrawLine(p2, p3);

        ShowDirections();
        Handles.DrawBezier(p0, p3, p1, p2, Color.white, null, 2f);

        /*
        Handles.color = Color.white;
        //Vector3 lineStart = curve.GetPointQuadratic(0f);
        Vector3 lineStart = curve.GetPointCubic(0f);

        Handles.color = Color.green;
        //Handles.DrawLine(lineStart, lineStart + curve.GetVelocityQuadratic(0f));
        Handles.DrawLine(lineStart, lineStart + curve.GetVelocityCubic(0f));

        Handles.color = Color.red;
        //Handles.DrawLine(lineStart, lineStart + curve.GetDirectionQuadratic(0f));
        Handles.DrawLine(lineStart, lineStart + curve.GetDirectionCubic(0f));
        
        for (int i = 0; i <= lineSteps; i++)
        {
            //Vector3 lineEnd = curve.GetPointQuadratic(i/(float)lineSteps);
            Vector3 lineEnd = curve.GetPointCubic(i/(float)lineSteps);

            Handles.color = Color.white;
            Handles.DrawLine(lineStart, lineEnd);

            Handles.color = Color.green;
            //Handles.DrawLine(lineEnd, lineEnd + curve.GetVelocityQuadratic(i / (float)lineSteps));
            Handles.DrawLine(lineEnd, lineEnd + curve.GetVelocityCubic(i / (float)lineSteps));


            Handles.color = Color.red;
            //Handles.DrawLine(lineEnd, lineEnd + curve.GetDirectionQuadratic(i / (float)lineSteps));
            Handles.DrawLine(lineEnd, lineEnd + curve.GetDirectionCubic(i / (float)lineSteps));


            lineStart = lineEnd;
        }
        */
    }

    private Vector3 ShowPoint(int index)
    {
        Vector3 point = handleTransform.TransformPoint(curve.points[index]);

        EditorGUI.BeginChangeCheck();
        point = Handles.DoPositionHandle(point, handleRotation);
        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(curve, "Move Point");
            EditorUtility.SetDirty(curve);
            curve.points[index] = handleTransform.InverseTransformPoint(point);
        }
        return point;
    }

    private void ShowDirections()
    {
        Handles.color = Color.green;
        Vector3 point = curve.GetPointCubic(0f);
        Handles.DrawLine(point, point + curve.GetDirectionCubic(0f) * directionScale);
        for(int i = 1; i <= lineSteps; i++)
        {
            point = curve.GetPointCubic(i / (float)lineSteps);
            Handles.DrawLine(point, point + curve.GetDirectionCubic(i/(float)lineSteps) * directionScale);
        }
    }


}

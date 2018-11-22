using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakePointsforLsys : MonoBehaviour
{
    private string axiom;
    private string currentString;
    private Dictionary<char, string> rules = new Dictionary<char, string>();
    private Stack<TransformInfo_01> transformStack = new Stack<TransformInfo_01>();

    public float length;
    public float angle;
    private bool isGenerating = false;
    private float axiomAngle;
    private int iterationCounter = 0;
    //private int iteration;
    private float ranAngle;
    private float ranLength;
    public int iterations = 0;
    public int count = 0;

    private List<Vector3> points = new List<Vector3>();
    Vector3 origin = new Vector3();

    private List<Vector3> fleursPos = new List<Vector3>();
    private List<Vector3> fleursInfos = new List<Vector3>();

    private List<Vector3> feuillesPos = new List<Vector3>();
    private List<Vector3> feuillesInfos = new List<Vector3>();

    //public int numberOfPoints;

    public List<Vector3> getFleursPos()
    {
        return fleursPos;
    }

    public List<Vector3> getFeuillesPos(int m_numFeuilles)
    {
        for (int i = 0; i < m_numFeuilles; i++)
        {
            int indexSort = Random.Range(0, points.Count);
            feuillesPos.Add(points[indexSort]);
        }
        return feuillesPos;
    }
    public List<Vector3> getFleursInfos()
    { 
        return fleursInfos;
    }

    public List<Vector3> getFeuillesInfos()
    {
        return feuillesInfos;
    }

    public List<Vector3> getPoints()
    {
        return points;
    }

    // Use this for initialization
    public void startLsys(float m_angle, float m_length, int m_iterations, Vector3 m_origin, int m_type)
    {
        // L-system parameters

        if (m_type == 0)
        {
            axiom = "X";
            rules.Add('X', "F[-X][X][+X]");
            rules.Add('F', "FF");
        }

        else if (m_type == 1)
        {
            axiom = "X";
            rules.Add('X', "F-[[X]+X]+F[+FX]-X");
            rules.Add('F', "FF");
        }


        else if (m_type == 2)
        {
            axiom = "X";
            rules.Add('X', "F[+X][-X]FX");
            rules.Add('F', "FF");
        }
            

        currentString = axiom;

        axiomAngle = 0f;
        angle = m_angle;
        length = m_length;
        iterations = m_iterations;
        ranAngle = angle * 0.2f;
        ranLength = length * 0.2f;
        origin = m_origin;

        points = GenerateSystemControlPoints();
    }

    public List<Vector3> GenerateSystemControlPoints()
    {
        List<Vector3> tempPoints = new List<Vector3>();
        while (iterationCounter < iterations)
        {
            tempPoints = Generate();
        }

        return tempPoints;
    }

    public List<Vector3> Generate()
    {
        string newString = "";

        char[] stringCharacters = currentString.ToCharArray();

        for (int i = 0; i < stringCharacters.Length; i++)   // sting character manage and replace
        {
            char currentCharacter = stringCharacters[i];
            if (rules.ContainsKey(currentCharacter))
            {
                newString += rules[currentCharacter];
            }
            else
            {
                newString += currentCharacter.ToString();
            }
        }

        currentString = newString;
        iterationCounter = iterationCounter + 1;
        stringCharacters = currentString.ToCharArray();

        List<Vector3> tempPoints = new List<Vector3>();

        if (iterationCounter == iterations)
        {
            transform.position = origin;
            //transform.Rotate
            Vector3 initialPosition = transform.position;
            tempPoints.Add(initialPosition);


            for (int i = 0; i < stringCharacters.Length; i++)
            {
                bool lala = false;

                char currentCharacter = stringCharacters[i];

                if (currentCharacter == 'F')
                {
                    lala = true;

                    transform.Translate(Vector3.up * (length + Random.Range(-ranLength, ranLength)));
                    transform.Rotate(Vector3.forward * (axiomAngle + Random.Range(-ranAngle, ranAngle)));
                    Vector3 secondPosition = transform.position;
                    tempPoints.Add(secondPosition);
                }
                if (currentCharacter == 'X')
                {
                    transform.Translate(Vector3.up * (length + Random.Range(-ranLength, -ranLength)));
                    transform.Rotate(Vector3.forward * (axiomAngle + Random.Range(-ranLength, ranLength)));
                }
                else if (currentCharacter == '+')
                {
                    transform.Rotate(Vector3.forward * (angle + Random.Range(-ranAngle, ranAngle)));
                }
                else if (currentCharacter == '-')
                {
                    transform.Rotate(Vector3.forward * -(angle + Random.Range(-ranAngle, ranAngle)));
                }
                else if (currentCharacter == '[')
                {
                    TransformInfo_01 ti = new TransformInfo_01();
                    ti.position = transform.position;
                    ti.rotation = transform.rotation;
                    transformStack.Push(ti);
                }
                else if (currentCharacter == ']')
                {
                    
                    TransformInfo_01 ti = transformStack.Pop();
                    transform.position = ti.position;
                    transform.rotation = ti.rotation;
                    if (lala == true)
                    {
                        fleursPos.Add(transform.position);
                        // x = angle, y = size, z = index sprite
                        float newAngle = transform.rotation.eulerAngles.z;
                        //Debug.Log(newAngle);
                        Vector3 lulu = new Vector3(newAngle, 1.0f, 1);
                        fleursInfos.Add(lulu);
                    }
                    lala = false;

                }
            }
            
        }

        return tempPoints;
    }
}

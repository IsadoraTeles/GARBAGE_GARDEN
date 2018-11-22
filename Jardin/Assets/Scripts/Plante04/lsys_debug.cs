using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lsys_debug : MonoBehaviour {
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
    public int iteration = 6;


    //public class TransformInfo ti;

    // Use this for initialization
    void Start()
    {

        // L-system parameters
        axiom = "X";
        rules.Add('X', "F[-X][X][+X]");
        rules.Add('F', "FF");
        currentString = axiom;

        axiomAngle = 0f;
        //angle = 30f;
        //length = 0.3f;
        ranAngle = angle * 0.2f;
        ranLength = length * 0.2f;

        // ready to start
        StartCoroutine(GenerateLSystem());
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    // mise en place de la generation
    IEnumerator GenerateLSystem()
    {
        int count = 0;
        while (count < 5)
        {
            if (!isGenerating)
            {
                isGenerating = true;
                if (iterationCounter <= iteration)  // evite au calcul de continuer au dela de l'iteration choisie
                {
                    StartCoroutine(Generate());     // main fonction
                }
            }
            else
            {
                yield return new WaitForSeconds(0.1f);
            }
        }
    }

    IEnumerator Generate()
    {
        //length = length / 2f; 

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
        Debug.Log(currentString);
        iterationCounter = iterationCounter + 1;
        Debug.Log(iterationCounter);


        stringCharacters = currentString.ToCharArray();

        if (iterationCounter == iteration)
        {        //------ L SYSTEM DRAWING ---------//
            for (int i = 0; i < stringCharacters.Length; i++)
            {
                char currentCharacter = stringCharacters[i];

                if (currentCharacter == 'F')
                {
                    Vector3 initialPosition = transform.position;
                    transform.Translate(Vector3.forward * (length + Random.Range(-ranLength, ranLength)));
                    transform.Rotate(Vector3.up * (axiomAngle + Random.Range(-ranAngle, ranAngle)));
                    Debug.DrawLine(initialPosition, transform.position, Color.white, 10000f, false);
                    yield return null;
                }
                if (currentCharacter == 'X')
                {
                    //transform.Translate(Vector3.forward * (length + Random.Range(-ranLength, -ranLength)));
                    //transform.Rotate(Vector3.up * (axiomAngle + Random.Range(-ranLength, ranLength)));
                    yield return null;
                }
                else if (currentCharacter == '+')
                {
                    transform.Rotate(Vector3.up * (angle + Random.Range(-ranAngle, ranAngle)));
                }
                else if (currentCharacter == '-')
                {
                    transform.Rotate(Vector3.up * -(angle + Random.Range(-ranAngle, ranAngle)));
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
                }
            }
        }
        isGenerating = false;
    }
}

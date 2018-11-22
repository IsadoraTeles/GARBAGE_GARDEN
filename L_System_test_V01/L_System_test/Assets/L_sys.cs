using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L_sys : MonoBehaviour {
    private string axiom;

    private string currentString;
    private Dictionary<char,string> rules = new Dictionary<char, string>();
    private Stack<TransformInfo> transformStack = new Stack<TransformInfo>();

    private float length;
    private float angle;
    private bool isGenerating = false;
    private float coef;
    private float coefDepth;
    private float lengthTwo;
    private float axiomAngle;

    //public class TransformInfo ti;

    // Use this for initialization
	void Start () {
        /*rules.Add('A', "AB");
        rules.Add('B', "A");*/
        /*rules.Add('X', "F[+X]F[-X]+F");
        rules.Add('F', "FF");*/

        axiom = "F";
        rules.Add('F', "F[-F]F[+F]F");
        currentString = axiom;

        axiomAngle = 0f;
        angle = 25f;
        length = 0.1f;
        lengthTwo = 50f;
        coef = 0.5f;
        StartCoroutine(GenerateLSystem());

        /*Generate();
        Generate();
        Generate();*/
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    IEnumerator GenerateLSystem() {
        int count = 0;

    
        while (count < 5)
        {
            if (!isGenerating)
            {
                isGenerating = true;
                StartCoroutine(Generate());
            }
            else
            {
                yield return new WaitForSeconds(0.1f);
            }
        }
    }

    IEnumerator Generate() {
        //length = length / 2f;
        string newString = "";
    
        char[] stringCharacters = currentString.ToCharArray ();

        for (int i = 0; i < stringCharacters.Length; i++) {
            char currentCharacter = stringCharacters[i];

            //newString += rules[currentCharacter];
            if (rules.ContainsKey(currentCharacter))
            {
                newString += rules[currentCharacter];
            }
            else {
                newString +=currentCharacter.ToString();
            }
        }

        currentString = newString;
        Debug.Log(currentString);

        stringCharacters = currentString.ToCharArray();

        for (int i = 0; i < stringCharacters.Length; i++) {
            char currentCharacter = stringCharacters[i];

            if (currentCharacter == 'F')
            {
                //move forward
                Vector3 initialPosition = transform.position;
                transform.Translate(Vector3.forward * length);
                transform.Rotate(Vector3.up * axiomAngle);
                Debug.DrawLine(initialPosition, transform.position, Color.white, 10000f, false);
                yield return null;
            }
            else if (currentCharacter == '+')
            {
                transform.Rotate(Vector3.up * angle);
            }
            else if (currentCharacter == '-')
            {
                transform.Rotate(Vector3.up * -angle);
            }
            else if (currentCharacter == '[')
            {
                TransformInfo ti = new TransformInfo ();
                ti.position = transform.position;
                ti.rotation = transform.rotation;
                transformStack.Push(ti);
                coefDepth = coefDepth * coef;
            }
            else if (currentCharacter == ']')
            {
                TransformInfo ti = transformStack.Pop();
                transform.position = ti.position;
                transform.rotation = ti.rotation;
                coefDepth = coefDepth / coef;
            }
            else if (currentCharacter == '|')
            {
                Vector3 initialPosition = transform.position;
                transform.Translate(Vector3.forward * lengthTwo * coefDepth);
                Debug.DrawLine(initialPosition, transform.position, Color.white, 10000f, false);
                yield return null;
            }
        }
        isGenerating = false;
    }
}

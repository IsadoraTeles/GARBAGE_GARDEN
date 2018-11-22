using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode, RequireComponent(typeof(LineRenderer))]
public class LSystem_01 : MonoBehaviour
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
    public int iteration = 6;
 
    public List<Vector3> points = new List<Vector3>();
    public LineRenderer lineRender;
    public int numberOfPoints;
    public Color startColor = Color.red;
    public Color endColor = Color.blue;
    public float startWidth = 5;
    public float endtWidth = 2;
    public int lineCount = 0;
    public int branchCount = 0;

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

        lineRender = GetComponent<LineRenderer>();
        lineRender.useWorldSpace = true;
        lineRender.material = new Material(Shader.Find("Particles/Standard Unlit"));

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

        lineRender.startColor = startColor;
        lineRender.endColor = endColor;
        lineRender.startWidth = startWidth;
        lineRender.endWidth = endtWidth;

        if (numberOfPoints < 2)
        {
            numberOfPoints = 2;
        }


    }

    // mise en place de la generation
    IEnumerator GenerateLSystem()
    {
        int count = 0;
        while (count < 2)
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
        //Debug.Log(currentString);
        iterationCounter = iterationCounter + 1;
        //Debug.Log(iterationCounter);


        stringCharacters = currentString.ToCharArray();

        if (iterationCounter == iteration)
        {        //------ L SYSTEM DRAWING ---------//

            //LineRenderer lineBase = new LineRenderer();
            ///lineBase = setupLineRenderer(lineBase);
            //int counterF = 0;

            //calculateNumbers(stringCharacters);
            //Debug.Log(lineCount);

            for (int i = 0; i < stringCharacters.Length; i++)
            {
                char currentCharacter = stringCharacters[i];

                if (currentCharacter == 'F')
                {
                    Vector3 initialPosition = transform.position;
                    points.Add(initialPosition);
                    transform.Translate(Vector3.forward * (length + Random.Range(-ranLength, ranLength)));
                    transform.Rotate(Vector3.up * (axiomAngle + Random.Range(-ranAngle, ranAngle)));
                    Vector3 secondPosition = transform.position;
                    points.Add(secondPosition);
                    //Debug.DrawLine(initialPosition, transform.position, Color.white, 10000f, false);
                    //Debug.Log("lala");
                    updateLineRenderer(lineRender, points);

                    yield return null;
                }
                if (currentCharacter == 'X')
                {
                    transform.Translate(Vector3.forward * (length + Random.Range(-ranLength, -ranLength)));
                    transform.Rotate(Vector3.up * (axiomAngle + Random.Range(-ranLength, ranLength)));
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
        //updateLineRenderer(lineRender, points);

    }

    void calculateNumbers(char[] m_stringCharacters)
    {
        for (int i = 0; i < m_stringCharacters.Length; i++)   // sting character manage and replace
        {
            char currentCharacter = m_stringCharacters[i];
            if (currentCharacter == 'F')
            {
                lineCount++;
            }
            else if (currentCharacter == '[')
            {
                branchCount++;
            }
            //else if (m_currentCharacter == ']')
            //{

            //}

        }
    }

    void setupLineRenderer(LineRenderer m_line)
    {
        m_line = GetComponent<LineRenderer>();
        m_line.useWorldSpace = true;
        m_line.material = new Material(Shader.Find("Particles/Standard Unlit"));

        //return m_line;
    }

    void updateLineRenderer(LineRenderer m_line, List<Vector3> m_positions)
    {
        // update line renderer
        //m_line.startColor = startColor;
        //m_line.endColor = endColor;
        //m_line.startWidth = startWidth;
        //m_line.endWidth = endtWidth;

        float timer = Time.time / 10;
        if (timer >= 1) { timer = 1.0f; }
        m_line.positionCount = m_positions.Count;

        for (int i = 0; i < m_positions.Count; i++)
        {
            if (i < m_line.positionCount)
            {
                m_line.SetPosition(i, m_positions[i]);
                //counter++;
            }
        }

        //return m_line;
    }

}

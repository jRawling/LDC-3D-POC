using UnityEngine;
using System.Collections;

public class Highlighter : MonoBehaviour
{

    Shader defaultShader;
    bool isHighlighted;

    // Use this for initialization
    void Start()
    {
        isHighlighted = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnMouseDown()
    {
        Debug.Log("mouse down");
    }

    void OnMouseEnter()
    {
        Debug.Log("mouse enter");

        if (!isHighlighted)
        {
            var renderer = GetComponent<Renderer>();
            defaultShader = renderer.material.shader;
            renderer.material.shader = Shader.Find("VacuumShaders/The Amazing Wireframe/(Preview) Color and Tangent");
            //   renderer.material.shader = Shader.Find("Custom/OcclusionOutline");
            isHighlighted = true;
        }
    }

    void OnMouseExit()
    {
        Debug.Log("mouse exit");

        if (isHighlighted)
        {
            var renderer = GetComponent<Renderer>();
            renderer.material.shader = defaultShader;
            isHighlighted = false;
        }
    }
}
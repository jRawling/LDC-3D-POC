using UnityEngine;
using System.Collections;

public class Adder : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {



        foreach (Transform child in transform)
        {
         //    child.gameObject.AddComponent<TheAmazingWireframeGenerator>();
          //  child.gameObject.AddComponent<Highlighter>();
            child.gameObject.AddComponent<MeshCollider>();
            //   Debug.Log(child.name);
        }

    }

    // Update is called once per frame
    void Update()
    {

    }
}

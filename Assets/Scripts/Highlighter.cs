using UnityEngine;
using System.Collections;

public class Highlighter : MonoBehaviour
{
    Material highlightMaterial;
    Material defaultMaterial;
    Transform currentlySelectedTransform;

    void Start()
    {
        highlightMaterial = Resources.Load("Materials/Glow", typeof(Material)) as Material;
    }

    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            RaycastHit hitInfo = new RaycastHit();
            bool hit = Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo);
            if (hit)
            {
                Select(hitInfo.transform);
                Debug.Log("Hit " + hitInfo.transform.gameObject.name);
            }
            else
            {
                Deselect();
                Debug.Log("No hit");
            }
        }
    }

    void Select(Transform hitTransform)
    {
        Deselect();
        currentlySelectedTransform = hitTransform;
        Renderer renderer = hitTransform.GetComponent<Renderer>();
        defaultMaterial = renderer.material;
        renderer.material = highlightMaterial;
    }

    void Deselect()
    {
        if (currentlySelectedTransform != null)
        {
            Renderer renderer = currentlySelectedTransform.GetComponent<Renderer>();
            renderer.material = defaultMaterial;
            currentlySelectedTransform = null;
            defaultMaterial = null;
        }
    }
}
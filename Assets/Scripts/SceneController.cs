using UnityEngine;
using System.Collections.Generic;
using System;

public class SceneController : MonoBehaviour
{
    public DamageMenuController damageMenuController;
    public Material highlightMaterial;
    public Material newMaterial;
    public Material selectedMaterial;
    Material defaultMaterial;
    RaycastHit? currentlySelected;
    public GameObject markerObjects;
    Dictionary<Guid, int> markers;
    public GameObject markerPrefab;

    void Start()
    {
    }

    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            RaycastHit hitInfo = new RaycastHit();
            bool hit = Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo);
            if (hit)
            {
                if(hitInfo.transform.tag == "Marker")
                {
                    // select the marker and load the menu with the info
                    return;
                }
                Select(hitInfo);
                Debug.Log("Hit " + hitInfo.transform.gameObject.name + " at: " + hitInfo.point.x + ", " + hitInfo.point.y + ", " + hitInfo.point.z);
            }
        }
    }

    void Select(RaycastHit hit)
    {
        Deselect();
        currentlySelected = hit;
        Renderer renderer = hit.transform.GetComponent<Renderer>();
        defaultMaterial = renderer.material;
        renderer.material = highlightMaterial;
        damageMenuController.SetActive(true);
        GameObject obj = Instantiate(markerPrefab, new Vector3(hit.point.x, hit.point.y, hit.point.z), Quaternion.identity) as GameObject;
    }

    public void Deselect()
    {
        if (currentlySelected.HasValue)
        {
            Renderer renderer = currentlySelected.Value.transform.GetComponent<Renderer>();
            renderer.material = defaultMaterial;
            currentlySelected = null;
            defaultMaterial = null;
        }

        damageMenuController.SetActive(false);
    }
}
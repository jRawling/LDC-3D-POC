// Prints the name of the object camera is directly looking at
using UnityEngine;
using System.Collections;

public class RayCaster : MonoBehaviour
{
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hitInfo = new RaycastHit();
            bool hit = Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo);
            if (hit)
            {
                Debug.Log("Hit " + hitInfo.transform.gameObject.name + " at: " + hitInfo.point.x + ", " + hitInfo.point.y + ", " + hitInfo.point.z);
            }
            else
            {
                Debug.Log("No hit");
            }
        }
    }
}
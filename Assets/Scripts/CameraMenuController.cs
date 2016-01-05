using UnityEngine;
using System.Collections;

public class CameraMenuController : MonoBehaviour {

    public void TogglePerspective()
    {
        Camera.main.orthographic = !Camera.main.orthographic;
    }
}

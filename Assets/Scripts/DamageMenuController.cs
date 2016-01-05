using UnityEngine;
using System.Collections;

public class DamageMenuController : MonoBehaviour
{
    public GameObject menu;
    public bool IsOpen
    {
        get; private set;
    }

    public void SetActive(bool isActive)
    {
        menu.SetActive(isActive);
        IsOpen = isActive;
    }

    public void CloseMenu()
    {
        menu.SetActive(false);
    }

    public void ShowMenu()
    {
        menu.SetActive(true);
    }
}
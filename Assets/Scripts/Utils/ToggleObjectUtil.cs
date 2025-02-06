using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleObjectUtil : MonoBehaviour
{
    private bool IsVisible;

    private void Start()
    {
        IsVisible = gameObject.activeSelf;

        gameObject.SetActive(IsVisible);
    }


    public void ToggleObject()
    {
        IsVisible = !IsVisible;
        gameObject.SetActive(IsVisible);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerUtil : MonoBehaviour
{
    public bool IsGrabbing { get; set; } = false;


    public void HandleExit()
    {
#if UNITY_STANDALONE
        Application.Quit();
#endif
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}

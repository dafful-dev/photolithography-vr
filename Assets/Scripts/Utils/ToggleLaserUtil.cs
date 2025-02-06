using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleLaserUtil : MonoBehaviour
{

    public GameManagerUtil GameManagerUtilObj;


    // Update is called once per frame
    void Update()
    {
        if (GameManagerUtilObj == null) return;
        if (GameManagerUtilObj == false) gameObject?.SetActive(false);
        else gameObject?.SetActive(true);

    }
}

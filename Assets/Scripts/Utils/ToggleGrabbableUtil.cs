using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ToggleGrabbableUtil : MonoBehaviour
{

    public GameManagerUtil GameManagerUtilObj;

    private void OnEnable()
    {
        var handModels = FindObjectsOfType<HandModel>(true);


        var grabInteractable = GetComponent<XRGrabInteractable>();
        grabInteractable.selectEntered.AddListener(EnableGrabbing);
        grabInteractable.selectExited.AddListener(DisableGrabbing);
    }

    private void OnDisable()
    {
        var grabInteractable = GetComponent<XRGrabInteractable>();
        grabInteractable.selectEntered.RemoveListener(EnableGrabbing);
        grabInteractable.selectExited.RemoveListener(DisableGrabbing);
    }

    private void DisableGrabbing(SelectExitEventArgs arg0)
    {
        GameManagerUtilObj.IsGrabbing = false;
    }

    private void EnableGrabbing(SelectEnterEventArgs arg0)
    {
        GameManagerUtilObj.IsGrabbing = false;
    }


}

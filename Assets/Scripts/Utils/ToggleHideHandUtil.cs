using System;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ToggleHideHandUtil : MonoBehaviour
{
    private GameObject LeftHandModel;
    private GameObject RightHandModel;


    private void OnEnable()
    {
        var handModels = FindObjectsOfType<HandModel>(true);

        LeftHandModel = Array.Find(handModels, x => x.HandType == HAND_TYPE.LEFT).gameObject;
        RightHandModel = Array.Find(handModels, x => x.HandType == HAND_TYPE.RIGHT).gameObject;

        var grabInteractable = GetComponent<XRGrabInteractable>();
        grabInteractable.selectEntered.AddListener(HideGrabbingHand);
        grabInteractable.selectExited.AddListener(ShowGrappingHand);
    }

    private void OnDisable()
    {
        var grabInteractable = GetComponent<XRGrabInteractable>();
        grabInteractable.selectEntered.RemoveListener(HideGrabbingHand);
        grabInteractable.selectExited.RemoveListener(ShowGrappingHand);
    }

    private void ShowGrappingHand(SelectExitEventArgs args)
    {

        if (args.interactorObject.transform.tag == "LeftHand")
            LeftHandModel.SetActive(true);
        else if (args.interactorObject.transform.tag == "RightHand")
            RightHandModel.SetActive(true);

    }

    private void HideGrabbingHand(SelectEnterEventArgs args)
    {
        if (args.interactorObject.transform.tag == "LeftHand")
            LeftHandModel.SetActive(false);
        else if (args.interactorObject.transform.tag == "RightHand")
            RightHandModel.SetActive(false);

    }
}

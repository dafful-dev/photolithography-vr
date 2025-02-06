using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class HandlePetriDishStep : HandleStepBase
{
    TagSocketInteractor Interactable;
    private void OnEnable()
    {
        Interactable = gameObject.GetComponent<TagSocketInteractor>();
        Interactable.selectEntered.AddListener(HandleSelectEntered);
    }

    private void OnDisable()
    {
        Interactable.selectEntered.RemoveListener(HandleSelectEntered);
    }


    private void HandleSelectEntered(SelectEnterEventArgs args)
    {
        if (!CanPerformStep())
            return;

        HandleCompleteStep();
    }

}

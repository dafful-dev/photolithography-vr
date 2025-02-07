using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class XRRayInteractorWithoutGrabbable : XRRayInteractor
{
    public override bool CanSelect(IXRSelectInteractable interactable)
    {
        // Ensure base conditions still apply
        if (!base.CanSelect(interactable))
            return false;

        // Check if the interactable is associated with a GameObject that has the Grabbable script
        if (interactable.transform.TryGetComponent<Grabbable>(out _))
        {
            return false; // Prevent grabbing if Grabbable script is attached
        }

        return true; // Otherwise, allow selection
    }

}

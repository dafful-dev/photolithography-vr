using UnityEngine.XR.Interaction.Toolkit;

public class HandleWaferStep : HandleStepBase
{
    XRGrabInteractable Interactable;

    private void HandleSelectEntered(SelectEnterEventArgs args)
    {
        if (!CanPerformStep()) return;

        HandleCompleteStep();
    }

    private void OnEnable()
    {
        Interactable = gameObject.GetComponent<XRGrabInteractable>();
        Interactable.selectEntered.AddListener(HandleSelectEntered);
    }

    private void OnDisable()
    {
        Interactable.selectEntered.RemoveListener(HandleSelectEntered);
    }

}

using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public abstract class HandleRaycastStepBase : HandleStepBase
{
    private bool IsActivated;
    private bool HasHit;

    private XRGrabInteractable GrabInteractable;
    private string ObjectTag = "Wafer";

    public Transform ShootSourse;
    public float MaxRaycastDistance = 10;



    public virtual void OnActivated(bool value) { }

    protected virtual Vector3 GetDirection()
    {
        return ShootSourse.forward;
    }

    private void Deactivate()
    {
        if (HasHit) HasHit = false;
        if (IsActivated) IsActivated = false;

       if(!IsActivated) OnActivated(IsActivated);
    }
    private void HandleDeactivated(DeactivateEventArgs args)
    {
        Deactivate();
    }

    private void HandleActivated(ActivateEventArgs args)
    {
        if (IsActivated) return;
        IsActivated = true;
        OnActivated(IsActivated);
    }

    private void HandleSelectExited(SelectExitEventArgs args)
    {
        Deactivate();
    }

    void Update()
    {
        RaycastCheck();
    }

    public void RaycastCheck()
    {
        if (!CanPerformStep() || !IsActivated)
            return;

        RaycastHit hit;

        Physics.Raycast(
            ShootSourse.position,
            GetDirection(),
            out hit,
            MaxRaycastDistance);

        if (hit.collider.CompareTag(ObjectTag) && !HasHit)
        {
            HandleCompleteStep();
            HasHit = true;
        }
    }

    protected void OnEnable()
    {
        GrabInteractable = gameObject.GetComponent<XRGrabInteractable>();
        GrabInteractable.activated.AddListener(HandleActivated);
        GrabInteractable.deactivated.AddListener(HandleDeactivated);
        GrabInteractable.selectExited.AddListener(HandleSelectExited);
    }

    protected void OnDisable()
    {
        GrabInteractable.activated.RemoveListener(HandleActivated);
        GrabInteractable.deactivated.RemoveListener(HandleDeactivated);
        GrabInteractable.selectExited.RemoveListener(HandleSelectExited);
    }
}

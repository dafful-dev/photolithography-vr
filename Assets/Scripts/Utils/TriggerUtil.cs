using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;

public class TriggerUtil : MonoBehaviour
{
  private XRSimpleInteractable Interactable;

    private bool IsTriggered = false;

    public UnityEvent OnTriggerTrue;
    public UnityEvent OnTriggerFalse;

    private bool IsEnabled = true;

    private void OnEnable()
    {
        Interactable = GetComponent<XRSimpleInteractable>();
        Interactable.firstHoverEntered.AddListener(OnSelectEntered);
    }

    private void OnDisable()
    {
        Interactable.firstHoverEntered.RemoveListener(OnSelectEntered);
    }

    public void SetEnabled(bool enabled)
    {
        IsEnabled = enabled;
    }

    private void OnSelectEntered(HoverEnterEventArgs arg)
    {
        if (!IsEnabled) return;

        if (IsTriggered)
        {
            IsTriggered = false;
            OnTriggerFalse.Invoke();
        }
        else
        {
            IsTriggered = true;
            OnTriggerTrue.Invoke();
        }
    }
}

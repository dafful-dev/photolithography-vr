using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;

public class AnimateOnActivate : MonoBehaviour
{
    public GameObject AnimationObject;
    public GameObject TriggerObject;

    private bool IsOpen = false;
    private Animator _Animator;
    XRBaseInteractable Interactable;

    public UnityEvent OnOpen;
    public UnityEvent OnClose;





    private void OnEnable()
    {
        _Animator = AnimationObject.GetComponent<Animator>();
        Interactable = TriggerObject.GetComponent<XRBaseInteractable>();
        Interactable.activated.AddListener(OnActivated);
        Interactable.deactivated.AddListener(OnDeactivate);
        Interactable.lastSelectExited.AddListener(OnSelectExited);
    }



    private void OnDisable()
    {
        Interactable.lastSelectExited.RemoveListener(OnSelectExited);
        Interactable.activated.RemoveListener(OnActivated);
        Interactable.deactivated.RemoveListener(OnDeactivate);

    }

    private void HandleClose()
    {
        _Animator.SetTrigger("TrClose");
        IsOpen = false;
        OnClose.Invoke();
    }

    private void HandleOpen()
    {
        _Animator.SetTrigger("TrOpen");
        IsOpen = true;
        OnOpen.Invoke();
    }

    private void OnDeactivate(DeactivateEventArgs arg)
    {
        HandleClose();
    }

    private void OnActivated(ActivateEventArgs arg)
    {
        if (IsOpen) HandleClose();
        else HandleOpen();
    }


    private void OnSelectExited(SelectExitEventArgs arg)
    {
        HandleClose();
    }

}

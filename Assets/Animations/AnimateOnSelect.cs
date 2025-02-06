using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;

public class AnimateOnSelect : MonoBehaviour
{
    public GameObject AnimationObject;
    public GameObject TriggerObject;

    private bool IsOpen = false;
    private Animator _Animator;
    XRBaseInteractable Interactable;

    public UnityEvent OnOpen;
    public UnityEvent OnClose;
    public UnityEvent OnEnabled;



    private void OnEnable()
    {
        _Animator = AnimationObject.GetComponent<Animator>();
        Interactable = TriggerObject.GetComponent<XRBaseInteractable>();
        Interactable.firstSelectEntered.AddListener(OnSelectEntered);
        OnEnabled.Invoke();
    }

    private void OnDisable()
    {
        Interactable.firstSelectEntered.RemoveListener(OnSelectEntered);
    }

 

    private void OnSelectEntered(SelectEnterEventArgs arg)
    {
        if (IsOpen)
        {
            _Animator.SetTrigger("TrClose");
            IsOpen = false;
            OnClose.Invoke();
        }
        else
        {
            _Animator.SetTrigger("TrOpen");
            IsOpen = true;
            OnOpen.Invoke();
        }
    }
}

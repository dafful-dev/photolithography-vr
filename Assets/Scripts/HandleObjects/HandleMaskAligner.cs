using UnityEngine;

public class HandleMaskAligner : HandleDevice
{
    public GameObject HolderAnimatorObject;
    public GameObject SliderAnimatorObject;
    private Animator HolderAnimator;
    private Animator SliderAnimator;


    public override void OnToggleOpened(bool value)
    {
        if (value) HolderAnimator.SetTrigger("TrOpen");
        else HolderAnimator.SetTrigger("TrClose");
    }

    public override void OnToggleStarted(bool value)
    {
        if (value) SliderAnimator.SetTrigger("TrOpen");
        else SliderAnimator.SetTrigger("TrClose");
    }
  

    private new void OnEnable()
    {
        base.OnEnable();

        HolderAnimator = HolderAnimatorObject.GetComponent<Animator>();
        SliderAnimator = SliderAnimatorObject.GetComponent<Animator>();
    }
}

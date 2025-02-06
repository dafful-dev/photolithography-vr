using UnityEngine;

public class HandleSpinCoater : HandleDevice
{
    public GameObject AnimationObject;
    private Animator OpenAnimator;

    public override void OnToggleOpened(bool value)
    {
        if (value) OpenAnimator.SetTrigger("TrOpen");
        else OpenAnimator.SetTrigger("TrClose");
    }


    private new void OnEnable()
    {
        base.OnEnable();
        OpenAnimator = AnimationObject.GetComponent<Animator>();
    }
}

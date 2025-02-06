using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleOven : HandleDevice
{
    public GameObject AnimationObject;
    private Animator OpenAnimator;

    public GameObject SocketObject1;
    protected TagSocketInteractor SocketInteractor1;

    public override void OnToggleOpened(bool value)
    {
        if (value) OpenAnimator.SetTrigger("TrOpen");
        else OpenAnimator.SetTrigger("TrClose");
    }


    private new void OnEnable()
    {
        base.OnEnable();
        OpenAnimator = AnimationObject.GetComponent<Animator>();

        SocketInteractor1 = SocketObject1.GetComponent<TagSocketInteractor>();
        SocketInteractor1?.selectEntered.AddListener(HandleSocketEntered);
        SocketInteractor1?.selectExited.AddListener(HandleSocketExited);
    }

    private new void OnDisable()
    {
        base.OnDisable();

        SocketInteractor1?.selectEntered.RemoveListener(HandleSocketEntered);
        SocketInteractor1?.selectExited.RemoveListener(HandleSocketExited);
    }
}


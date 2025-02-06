using UnityEngine;

public class HandleNitrogenGunStep : HandleRaycastStepBase
{
    public Animator _Animator;

    public override void OnActivated(bool value)
    {
        if(value)
        {
            AudioManager.PlayLoop(AudioManager.NoiseClip);
            _Animator.SetTrigger("TrOpen");

        }
        else
        {
            AudioManager.StopLoop();
            _Animator.SetTrigger("TrClose");
        }
    }
}

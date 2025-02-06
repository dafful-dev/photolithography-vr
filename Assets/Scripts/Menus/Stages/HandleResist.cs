using UnityEngine;

public class HandleResist : HandleRaycastStepBase
{
    protected override Vector3 GetDirection()
    {
        return ShootSourse.up;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleMaskAlignerStep : HandleStepBase
{
    protected void HandleDeviceComplete()
    {
        if (!CanPerformStep()) return;

        HandleCompleteStep();
    }


    protected void OnEnable()
    {
        HandleDevice.OnDeviceComplete += HandleDeviceComplete;
        HandleDevice.OnWaferPlaced += HandleDeviceComplete;

    }

    protected void OnDisable()
    {
        HandleDevice.OnDeviceComplete -= HandleDeviceComplete;
        HandleDevice.OnWaferPlaced -= HandleDeviceComplete;
    }

}

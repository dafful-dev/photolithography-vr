

public class HandleDeviceStep : HandleStepBase
{
    protected void HandleDeviceComplete()
    {
        if (!CanPerformStep()) return;

        HandleCompleteStep();
    }

    protected void OnEnable()
    {
        HandleDevice.OnDeviceComplete += HandleDeviceComplete;
    }

    protected void OnDisable()
    {
        HandleDevice.OnDeviceComplete -= HandleDeviceComplete;
    }
}

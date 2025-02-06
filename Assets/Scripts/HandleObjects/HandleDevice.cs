using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;

public abstract class HandleDevice : MonoBehaviour
{
    public UnityEvent OnComplete;
    public static event Action OnDeviceComplete;
    public static event Action OnWaferPlaced;

    protected AudioManagerUtil AudioManager;

    public Button PowerButton;
    public Button StartButton;
    public Button OpenButton;

    public GameObject SocketObject;
    protected TagSocketInteractor SocketInteractor;

    protected const int DELAY_TIME_SECONDS = 5;

    protected bool IsPowerOn { get; set; } = false;
    protected bool IsOpened { get; set; } = false;

    protected bool IsInSocket { get; set; } = false;

    protected bool IsDeviceStarted { get; set; } = false;

    public virtual void OnToggleOpened(bool value) { }

    public virtual void OnToggleStarted(bool value) { }


    protected void ToggleStartDevice()
    {

        if (!CanStartDevice()) return;

        IsDeviceStarted = !IsDeviceStarted;

        if (IsDeviceStarted) OnToggleStarted(IsDeviceStarted);
        else
        {
            OnToggleStarted(IsDeviceStarted);
            return;
        }

        AudioManager.PlayLoop(AudioManager.NoiseClip);

        // 5 seconds delay
        StartCoroutine(DelaySeconds(DELAY_TIME_SECONDS, () =>
        {
            AudioManager.PlayClip(AudioManager.SuccessClip);
            OnComplete.Invoke();
            OnDeviceComplete?.Invoke();
        }));

        StartCoroutine(DelaySeconds(DELAY_TIME_SECONDS + 0.5f, () =>
        {
            AudioManager.StopLoop();
            IsDeviceStarted = false;
            OnToggleStarted(IsDeviceStarted);
        }));
    }
    protected void ToggleOpenDevice()
    {
        if (!CanOpenDevice()) return;

        IsOpened = !IsOpened;
        AudioManager.PlayClip(AudioManager.BeepClip);

        OnToggleOpened(IsOpened);
    }
    protected void ResetDevice()
    {
        IsPowerOn = false;
        IsInSocket = false;

        if (IsDeviceStarted)
        {
            IsDeviceStarted = false;
            OnToggleStarted(IsDeviceStarted);
        }

        if (IsOpened)
        {
            IsOpened = false;
            OnToggleOpened(IsOpened);
        }

        if (IsInSocket) IsInSocket = false;

        StartButton.interactable = false;
        OpenButton.interactable = false;

        AudioManager.StopLoop();
    }

    protected void TogglePowerDevice()
    {
        IsPowerOn = !IsPowerOn;
        AudioManager.PlayClip(AudioManager.BeepClip);

        if (IsPowerOn)
        {
            StartButton.interactable = true;
            OpenButton.interactable = true;
        }
        else ResetDevice();
    }

    protected bool CanOpenDevice()
    {
        return IsPowerOn && !IsDeviceStarted;
    }

    protected bool CanStartDevice()
    {
        return IsPowerOn && !IsOpened && IsInSocket;
    }

    protected IEnumerator DelaySeconds(float seconds, Action action)
    {
        yield return new WaitForSeconds(seconds);

        action();
    }


    protected void HandleSocketEntered(SelectEnterEventArgs arg)
    {
        IsInSocket = true;
        OnWaferPlaced?.Invoke();
    }

    protected void HandleSocketExited(SelectExitEventArgs arg)
    {
        IsInSocket = false;
    }

    protected void OnEnable()
    {
        AudioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManagerUtil>();
        SocketInteractor = SocketObject.GetComponent<TagSocketInteractor>();

        PowerButton.onClick.AddListener(TogglePowerDevice);
        StartButton.onClick.AddListener(ToggleStartDevice);
        OpenButton.onClick.AddListener(ToggleOpenDevice);
        SocketInteractor?.selectEntered.AddListener(HandleSocketEntered);
        SocketInteractor?.selectExited.AddListener(HandleSocketExited);

        OpenButton.interactable = false;
        StartButton.interactable = false;
        IsPowerOn = false;
        IsOpened = false;
        IsDeviceStarted = false;
    }

    protected void OnDisable()
    {
        PowerButton.onClick.RemoveListener(TogglePowerDevice);
        StartButton.onClick.RemoveListener(ToggleStartDevice);
        OpenButton.onClick.RemoveListener(ToggleOpenDevice);
        SocketInteractor?.selectEntered.RemoveListener(HandleSocketEntered);
        SocketInteractor?.selectExited.RemoveListener(HandleSocketExited);
    }
}

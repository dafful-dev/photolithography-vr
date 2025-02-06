using UnityEngine;

public class AudioManagerUtil : MonoBehaviour
{
    [SerializeField] AudioSource AudioSource;
    public AudioClip SuccessClip;
    public AudioClip ErrorClip;
    public AudioClip NoiseClip;
    public AudioClip BeepClip;
    public AudioClip WellDoneClip;

    public void PlayClip(AudioClip clip)
    {
        AudioSource.PlayOneShot(clip);
    }

    public void PlayLoop(AudioClip clip)
    {
        AudioSource.clip = clip;
        AudioSource.loop = true;
        AudioSource.Play();
    }

    public void StopLoop()
    {
        AudioSource.loop = false;
        AudioSource.Stop();
        AudioSource.clip = null;
    }
}

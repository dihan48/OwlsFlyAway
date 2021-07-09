using UnityEngine;
using UnityEngine.Audio;

public class AudioClick : MonoBehaviour, IAudioEffect
{
    [SerializeField] private AudioClip audioClip;
    [SerializeField] private AudioMixerGroup audioMixerGroup;
    [Range(0f, 1f)]
    [SerializeField] private float volume = 1;
    private AudioSource audioSource;

    public void Play()
    {
        if (enabled && gameObject.activeInHierarchy)
        {
            audioSource.Play();
        }
    }

    public void SetActive(bool isActive)
    {
        enabled = isActive;

        if(isActive == false)
        {
            Stop();
        }
    }

    public void Stop()
    {
        audioSource.Stop();
    }

    private void Start()
    {
        audioSource = gameObject.AddComponent(typeof(AudioSource)) as AudioSource;
        audioSource.outputAudioMixerGroup = audioMixerGroup;
        audioSource.volume = volume;
        audioSource.clip = audioClip;
    }
}

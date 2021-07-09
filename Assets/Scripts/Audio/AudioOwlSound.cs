using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

public class AudioOwlSound : MonoBehaviour, IAudioEffect
{
    [SerializeField] private AudioClip[] audioClips;
    [SerializeField] private AudioMixerGroup audioMixerGroup;
    [SerializeField] private float volume = 1;

    public void Play()
    {
        if (enabled && gameObject.activeInHierarchy)
        {
            AudioSource audioSource = gameObject.AddComponent(typeof(AudioSource)) as AudioSource;
            audioSource.outputAudioMixerGroup = audioMixerGroup;
            audioSource.volume = volume;
            var clip = audioClips[Random.Range(0, audioClips.Length)];
            audioSource.clip = clip;
            audioSource.Play();
            StartCoroutine(DestroyAudioSource(audioSource, clip.length));
        }
    }

    public void SetActive(bool isActive)
    {
        enabled = isActive;
    }

    public void Stop()
    {
        
    }

    private void Start()
    {
        
    }

    private IEnumerator DestroyAudioSource(AudioSource audioSource, float delay)
    {
        yield return new WaitForSecondsRealtime(delay);
        Destroy(audioSource);
    }
}

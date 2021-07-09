using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

public class AudioAmbient : MonoBehaviour, IAudioAmbient
{
    [SerializeField] private float secondsDelayBetweenPlay = 1;
    [SerializeField] private AudioClip[] audioClips;
    [Range(0f, 1f)]
    [SerializeField] private float volume = 1;
    [SerializeField] private AudioMixerGroup audioMixerGroup;

    private AudioSource audioSource;
    private int currentClipIndex = -1;
    private IEnumerator coroutineLoopPlay;

    public void Play()
    {
        if (enabled && gameObject.activeInHierarchy)
        {
            if (coroutineLoopPlay != null)
            {
                Stop();
            }

            currentClipIndex = RandomClipIndex();
            coroutineLoopPlay = LoopPlay();
            StartCoroutine(coroutineLoopPlay);
        }
    }
    public void SetActive(bool isActive)
    {
        enabled = isActive;

        if (isActive)
        {
            Play();
        }
        else
        { 
            Stop();
        }

    }

    public void Stop()
    {
        StopCoroutine(coroutineLoopPlay);
        coroutineLoopPlay = null;
        audioSource.Stop();
        currentClipIndex = -1;
    }

    private void Start()
    {
        audioSource = gameObject.AddComponent(typeof(AudioSource)) as AudioSource;
        audioSource.outputAudioMixerGroup = audioMixerGroup;
        audioSource.volume = volume;
        Play();
    }

    private int RandomClipIndex()
    {
        int clipIndex = Random.Range(0, audioClips.Length);

        if(clipIndex == currentClipIndex)
        {
            return RandomClipIndex();
        }

        return clipIndex;
    }

    private IEnumerator LoopPlay()
    {
        var clip = audioClips[currentClipIndex];
        audioSource.clip = clip;
        audioSource.Play();

        yield return new WaitForSecondsRealtime(clip.length + secondsDelayBetweenPlay);

        currentClipIndex = RandomClipIndex();
        coroutineLoopPlay = LoopPlay();
        StartCoroutine(coroutineLoopPlay);
    }
}

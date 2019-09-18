using UnityEngine;

public class AudioBackground : MonoBehaviour
{
    public AudioClip[] audios;
    public AudioSource audioSource;

    void Start()
    {
        audioSource.volume = 0.05f;
        audioSource.clip = audios[Random.Range(0, 3)];
        audioSource.Play();
    }

    void Update()
    {
        if (!audioSource.isPlaying)
        {
            audioSource.clip = RandomAudio();
            audioSource.Play();
        }
    }

    AudioClip RandomAudio()
    {
        AudioClip temp = audios[Random.Range(0, 3)];
        if (temp != audioSource.clip)
            return temp;
        else
            return RandomAudio();
    }
}

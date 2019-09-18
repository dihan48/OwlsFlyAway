using UnityEngine;

public class AudioUgu : MonoBehaviour
{
    public AudioClip[] audios;
    public AudioSource audioSource;
    void Start()
    {
        audioSource.volume = 0.5f;
        audioSource.clip = audios[Random.Range(0, 5)];
    }
    void Update()
    {
        if (EqualSearch.soundMarker && !audioSource.isPlaying)
        {
            EqualSearch.soundMarker = false;
            if (audioSource.clip == audios[0] || audioSource.clip == audios[1])
            {
                audioSource.clip = audios[Random.Range(2, 5)];
            }
            else
            {
                audioSource.clip = audios[Random.Range(0, 2)];
            }
            audioSource.Play();
        }
    }
}

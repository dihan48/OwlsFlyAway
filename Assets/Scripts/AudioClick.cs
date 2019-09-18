using UnityEngine;

public class AudioClick : MonoBehaviour
{
    static AudioSource audioSource;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = 0.15f;
    }

    public static void Play()
    {
        if (!audioSource.isPlaying)
            audioSource.Play();
    }
}

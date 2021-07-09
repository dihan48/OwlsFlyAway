using UnityEngine;

public class AudioSetting : MonoBehaviour
{
    public bool ActiveSoundEffects
    {
        get => activeSoundEffects;
        set
        {
            activeSoundEffects = value;

            foreach (var item in audioEffects)
            {
                item.SetActive(value);
            }
        }
    }

    public bool ActiveSoundeAmbient
    {
        get => activeSoundeAmbient;
        set
        {
            activeSoundeAmbient = value;

            foreach (var item in audioAmbients)
            {
                item.SetActive(value);
            }
        }
    }

    private bool activeSoundEffects = true;
    private bool activeSoundeAmbient = true;

    private IAudioEffect[] audioEffects;
    private IAudioAmbient[] audioAmbients;

    private void Start()
    {
        audioEffects = GetComponents<IAudioEffect>();
        audioAmbients = GetComponents<IAudioAmbient>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            ActiveSoundeAmbient = !ActiveSoundeAmbient;
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            ActiveSoundEffects = !ActiveSoundEffects;
        }
    }
}

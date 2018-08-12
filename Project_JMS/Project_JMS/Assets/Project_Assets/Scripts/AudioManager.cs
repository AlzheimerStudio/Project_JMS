using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour
{
    AudioSource audioSource;
    public AudioMixer mixer;
    [SerializeField] private AudioClip[] audioClipsEffects = new AudioClip[0];
    [SerializeField] private AudioClip[] audioClipsPlayer = new AudioClip[0];
    [SerializeField] private AudioClip[] audioClipsUI = new AudioClip[0];

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayFXAudio(int index, float audioPitch, float audioVolume)
    {
        audioSource.pitch = audioPitch;
        audioSource.volume = audioVolume;
        audioSource.PlayOneShot(audioClipsEffects[index]);
        audioSource.pitch = 1;

    }
    public void PlayPlayerAudio(int index, float audioPitch, float audioVolume)
    {
        audioSource.pitch = audioPitch;
        audioSource.volume = audioVolume;
        audioSource.PlayOneShot(audioClipsPlayer[index]);
        audioSource.pitch = 1;

    }
    public void PlayUIAudio(int index, float audioPitch, float audioVolume)
    {
        audioSource.pitch = audioPitch;
        audioSource.volume = audioVolume;
        audioSource.PlayOneShot(audioClipsUI[index]);
        audioSource.pitch = 1;

    }

    public void ChangePitchOnMixer(float newPitch, float volume)
    {
        if (mixer != null)
        {
            mixer.SetFloat("_Volume", volume);
            newPitch = Mathf.Clamp(newPitch, 0.2f, 10);
            mixer.SetFloat("_Pitch", newPitch);
        }
    }
}

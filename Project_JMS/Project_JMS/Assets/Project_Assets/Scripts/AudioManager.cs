using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour
{
    AudioSource audioSource;
    [SerializeField] private AudioClip[] audioClipsEffects = new AudioClip[0];
    [SerializeField] private AudioClip[] audioClipsPlayer = new AudioClip[0];
    [SerializeField] private AudioClip[] audioClipsUI = new AudioClip[0];

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayFXAudio(int index, float audioPitch)
    {
        audioSource.pitch = audioPitch;
        audioSource.PlayOneShot(audioClipsEffects[index]);
        audioSource.pitch = 1;

    }
    public void PlayPlayerAudio(int index, float audioPitch)
    {
        audioSource.pitch = audioPitch;
        audioSource.PlayOneShot(audioClipsPlayer[index]);
        audioSource.pitch = 1;

    }
    public void PlayUIAudio(int index, float audioPitch)
    {
        audioSource.pitch = audioPitch;
        audioSource.PlayOneShot(audioClipsUI[index]);
        audioSource.pitch = 1;

    }
}

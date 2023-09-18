using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance { get; private set; }

    private AudioSource audioSource;

    [SerializeField]
    private List<AudioHolder> audioClipsList = new List<AudioHolder>();

    private Dictionary<SoundsFX, AudioClip> audioClipsDictionary = new Dictionary<SoundsFX, AudioClip>();

    float pitch_default;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }

        audioSource = Camera.main.GetComponent<AudioSource>();

        foreach (AudioHolder audio in audioClipsList)
        {
            if (!audioClipsDictionary.ContainsValue(audio.audioClip))
                audioClipsDictionary.Add(audio.audiokey, audio.audioClip);
        }

        pitch_default = audioSource.pitch;
    }

    public void PlayClip(SoundsFX audioClipKey)
    {
        if (audioClipsDictionary.ContainsKey(audioClipKey))
        {
            audioSource.pitch = pitch_default;
            audioSource.PlayOneShot(audioClipsDictionary[audioClipKey]);
        }
    }
    public void PlayClip(SoundsFX audioClipKey, float pitch)
    {
        if (audioClipsDictionary.ContainsKey(audioClipKey))
        {
            audioSource.pitch = pitch;
            audioSource.PlayOneShot(audioClipsDictionary[audioClipKey]);
        }
    }

    public void PlayHover()
    {
        if (audioClipsDictionary.ContainsKey(SoundsFX.SFX_Hover))
        {
            audioSource.PlayOneShot(audioClipsDictionary[SoundsFX.SFX_Hover]);
        }
    }

    public void PlayButtonClick()
    {
        if (audioClipsDictionary.ContainsKey(SoundsFX.SFX_Click))
        {
            audioSource.PlayOneShot(audioClipsDictionary[SoundsFX.SFX_Click]);
        }
    }
}

[System.Serializable]
public class AudioHolder
{
    public SoundsFX audiokey;
    public AudioClip audioClip;

    public AudioHolder(SoundsFX audiokey, AudioClip audioClip)
    {
        this.audiokey = audiokey;
        this.audioClip = audioClip;
    }
}

public enum SoundsFX
{
    SFX_Hover,
    SFX_Click,
    SFX_LetterPop,
    SFX_Talk,
    None
}
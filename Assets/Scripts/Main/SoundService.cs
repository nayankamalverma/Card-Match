using System;
using UnityEngine;

public class SoundService : MonoBehaviour
{
    [SerializeField] private AudioSource music;
    [SerializeField] private AudioSource soundEffect;
    [SerializeField] private Sound[] sounds;

    public static SoundService Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void Start()
    {
        PlayMusic(global::SoundType.Music);
    }

    public void PlayMusic(SoundType soundType)
    {
        AudioClip clip = GetSoundClip(soundType);
        if (clip != null)
        {
            music.clip = clip;
            music.loop = true;
            music.Play();
        }
    }

    public void Play(SoundType soundType)
    {
        AudioClip clip = GetSoundClip(soundType);
        if (clip != null)
        {
            soundEffect.PlayOneShot(clip);
        }
        else
        {
            Debug.LogWarning("Audio clip not found for " + soundType);
        }
    }


    private AudioClip GetSoundClip(SoundType soundType)
    {
        Sound sound = Array.Find(sounds, audio => audio.SoundType == soundType);
        if (sound != null)
        {
            return sound.soundClip;
        }

        return null;
    }
}

[Serializable]
public class Sound
{
    public SoundType SoundType;
    public AudioClip soundClip;
}

public enum SoundType
{
    Music,
    Button_Click,
    Card_Flip,
    Card_Match,
    Card_Mismatch,
    Game_Complete
}
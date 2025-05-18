using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundFXManager : MonoBehaviour
{
    public static SoundFXManager instance;

    [SerializeField] private AudioSource soundFXObject;
    [SerializeField] private AudioSource soundFXObjectNoMixer;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }

    public void PlaySoundFXClip(AudioClip audioClip, Transform spawnTransform, float volume, bool noMixer)
    {
        AudioSource audioSource;

        if (!noMixer)
        {
            audioSource = Instantiate(soundFXObject, spawnTransform.position, Quaternion.identity);
        }
        else
        {
            audioSource = Instantiate(soundFXObjectNoMixer, spawnTransform.position, Quaternion.identity);
        }

        audioSource.clip = audioClip;

        audioSource.volume = volume;

        audioSource.Play();

        float clipLength = audioSource.clip.length;

        Destroy(audioSource.gameObject, clipLength);
    }
}

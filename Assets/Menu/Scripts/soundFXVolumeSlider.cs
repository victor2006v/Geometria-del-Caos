using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class soundFXVolumeSlider : MonoBehaviour
{
    public Slider volumeSlider;

    private void Start()
    {
        if (!PlayerPrefs.HasKey("soundFXVolume"))
        {
            PlayerPrefs.SetFloat("soundFXVolume", 1);
            Load();
        }

        else
        {
            Load();
        }
    }

    public void changeVolume()
    {
        SoundMixerManager.instance.SetSoundFXVolume(volumeSlider.value);
        Save();
    }

    private void Load()
    {
        volumeSlider.value = PlayerPrefs.GetFloat("soundFXVolume");
    }

    private void Save()
    {
        PlayerPrefs.SetFloat("soundFXVolume", volumeSlider.value);
    }
}

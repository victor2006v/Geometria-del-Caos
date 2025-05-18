using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class generalVolumeSlider : MonoBehaviour
{
    public Slider volumeSlider;

    private void Start()
    {
        if (!PlayerPrefs.HasKey("masterVolume"))
        {
            PlayerPrefs.SetFloat("masterVolume", 1);
            Load();
        }

        else
        {
            Load();
        }
    }

    public void changeVolume()
    {
        SoundMixerManager.instance.SetMasterVolume(volumeSlider.value);
        Save();
    }

    private void Load()
    {
        volumeSlider.value = PlayerPrefs.GetFloat("masterVolume");
    }

    private void Save()
    {
        PlayerPrefs.SetFloat("masterVolume", volumeSlider.value);
    }
}

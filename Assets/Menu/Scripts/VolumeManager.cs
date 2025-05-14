using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeManager : MonoBehaviour{
    [SerializeField] Slider volumeSlider;
    private MenuManager menuManager;
    private void Start() {

        //By default 100%
        if (!PlayerPrefs.HasKey("musicVolume")) {
            PlayerPrefs.SetFloat("musicVolume", 1);
            Load();
        } else {
            Load();
        }
    }
    public void ChangeVolume() {
        menuManager.CountClicks();
        AudioListener.volume = volumeSlider.value;
        Save();
    }
    private void Load() { 
        volumeSlider.value = PlayerPrefs.GetFloat("musicVolume");
    }
    //To save the last settings
    private void Save() {
        PlayerPrefs.SetFloat("musicVolume", volumeSlider.value);
    }
}

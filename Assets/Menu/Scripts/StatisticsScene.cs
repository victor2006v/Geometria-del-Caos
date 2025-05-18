using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class StatisticsScene : MonoBehaviour
{
    [SerializeField]
    AudioClip menuSFX, cancelSFX;

    [SerializeField] private GameObject firstToSelect;

    public void OnEnable()
    {
        EventSystem.current.SetSelectedGameObject(firstToSelect);
        BGMusicController.instance.GetComponent<AudioSource>().clip = menuSFX;
        BGMusicController.instance.GetComponent<AudioSource>().Play();
    }
    public void ReturnToMenu()
    {
        SoundFXManager.instance.PlaySoundFXClip(cancelSFX, transform, 1f, false);
        SceneManager.LoadScene(0);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject mainMenuCanvasGO;
    [SerializeField] private GameObject settingsMenuCanvasGO;

    private bool isPaused;

    private void Start(){
        mainMenuCanvasGO.SetActive(false);
        settingsMenuCanvasGO.SetActive(false);
    }

    private void Update()
    {
        if (InputManager.instance.MenuOpenCloseInput) {
            if (!isPaused)
            {
                Pause();
            }
            else {
                UnPause();
            }
        }
    }
    #region Pause/Unpause Functions
    public void Pause() { 
        isPaused = true;
        Time.timeScale = 0f;
        OpenMainMenu();
    }
    public void UnPause(){

    }
    #endregion
    #region Canvas Activations
    private void OpenMainMenu() { 
        mainMenuCanvasGO.SetActive(true);
        settingsMenuCanvasGO.SetActive(false);
    }
    #endregion
}

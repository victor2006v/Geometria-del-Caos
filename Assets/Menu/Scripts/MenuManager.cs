using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class MenuManager : MonoBehaviour
{
    [Header("Menu Objects")]
    [SerializeField] private GameObject mainMenuCanvasGO;
    [SerializeField] private GameObject settingsMenuCanvasGO;
    [Header("First selected Options")]
    [SerializeField] private GameObject mainMenuFirst;
    [SerializeField] private GameObject settingsMenuFirst;
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
    //It is called when the esc input action is triggered
    #region Pause/Unpause Functions
    public void Pause() { 
        isPaused = true;
        Time.timeScale = 0f;
        OpenMainMenu();
    }
    public void UnPause(){
        Time.timeScale = 1f;

        CloseAllMenus();
    }
    #endregion
    #region Canvas Activations
    private void OpenMainMenu() { 
        mainMenuCanvasGO.SetActive(true);
        settingsMenuCanvasGO.SetActive(false);
        EventSystem.current.SetSelectedGameObject(mainMenuFirst);
    }

    //When we open Settings we desactivate the Main menu
    private void OpenSettingsMenuHandle() {
        settingsMenuCanvasGO.SetActive(true);
        mainMenuCanvasGO.SetActive(false);
        EventSystem.current.SetSelectedGameObject(settingsMenuFirst);
    }

    private void CloseAllMenus() {
        mainMenuCanvasGO.SetActive(false);
        settingsMenuCanvasGO.SetActive(false);
        EventSystem.current.SetSelectedGameObject(null);
    }
    #endregion
    #region Main Menu Button Actions
    public void OnSettingsPress() {
        OpenSettingsMenuHandle();
    }
    public void OnResumePress() {
        UnPause();
    }

    #endregion
    #region Settings Menu Button Actions

    public void OnSettingsBackMenu() {
        OpenMainMenu();
    }
    #endregion
}

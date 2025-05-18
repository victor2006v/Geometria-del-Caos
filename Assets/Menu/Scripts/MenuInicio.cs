using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;
using UnityEngine.SceneManagement;

public class MenuInicio : MonoBehaviour
{
    private bool difficulty, main, done, done2;

    [SerializeField] private GameObject MenuPanelDown;
    [SerializeField] private GameObject firstToSelect;
    [SerializeField] private GameObject difficultyToSelect;

    [SerializeField]
    public Animator mainMenuAnimation, difficultyMenuAnimation;
    [SerializeField]
    private InputSystemUIInputModule input;
    [SerializeField] GameObject objectToSelect;

    [SerializeField]
    private InputActionAsset inputMapping;
    private InputAction cancel;

    [SerializeField]
    AudioClip okSFX, cancelSFX;

    private void Awake(){
        inputMapping.Enable();
        cancel = inputMapping.FindActionMap("UI").FindAction("Cancel");
        main = true;
        difficulty = false;
        done = false;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            done = false;
            done2 = false;
        }

        if (main && !difficulty && !done)
        {
            EventSystem.current.SetSelectedGameObject(objectToSelect);
            done = true;
        }
        else if (!main && difficulty && !done2)
        {
            EventSystem.current.SetSelectedGameObject(difficultyToSelect);
            done2 = true;
        }

        if (!main && difficulty)
        {
            if (cancel.triggered)
            {
                Return();
            }
        }

    }

    private void OnEnable(){
        EventSystem.current.SetSelectedGameObject(firstToSelect);
    }

    public void Singleplayer(){
        input.enabled = false;
        difficulty = true;
        main = false;
        done2 = false;
        MenuManager.instance.CountClicks();
        SoundFXManager.instance.PlaySoundFXClip(okSFX, transform, 0.2f, false);
        mainMenuAnimation.SetTrigger("moveRightTrigger");
        difficultyMenuAnimation.SetTrigger("moveUpTrigger");
    }

    public void Classic(){
        MenuManager.instance.items = false;
        MenuManager.instance.difficulty = 1;
        MenuManager.instance.stepDelay = 0.7f;
        MenuManager.instance.CountClicks();
        SoundFXManager.instance.PlaySoundFXClip(okSFX, transform, 0.2f, false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }


    public void Hard() {
        MenuManager.instance.items = true;
        MenuManager.instance.difficulty = 2;
        MenuManager.instance.stepDelay = 0.5f;
        MenuManager.instance.CountClicks();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        
    }
    public void Extreme() {
        MenuManager.instance.stepDelay = 0.1f;
        MenuManager.instance.difficulty = 3;
        MenuManager.instance.CountClicks();
        MenuManager.instance.items = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        
    }
    public void Return() {
        input.enabled = false;
        difficulty = false;
        main = true;
        done = false;
        MenuManager.instance.CountClicks();
        SoundFXManager.instance.PlaySoundFXClip(cancelSFX, transform, 1f, false);
        mainMenuAnimation.SetTrigger("moveLeftTrigger");
        difficultyMenuAnimation.SetTrigger("moveDownTrigger");
    }

    public void Salir(){
        MenuManager.instance.CountClicks();
        SoundFXManager.instance.PlaySoundFXClip(cancelSFX, transform, 1f, false);
        Debug.Log("Leaving...");
        Application.Quit();
    }
    public void Settings() {
        MenuManager.instance.CountClicks();
        SoundFXManager.instance.PlaySoundFXClip(okSFX, transform, 0.2f, false);
        SceneManager.LoadScene(2);
    }
}

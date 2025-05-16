using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;
using UnityEngine.SceneManagement;

public class MenuInicio : MonoBehaviour
{
    //get a reference to the GameManager
    public MenuManager menuManager;

    [SerializeField] private GameObject MenuPanelDown;
    [SerializeField] private GameObject firstToSelect;

    [SerializeField]
    public Animator mainMenuAnimation, difficultyMenuAnimation;
    [SerializeField]
    private InputSystemUIInputModule input;
    [SerializeField] GameObject objectToSelect;

    //For InputAction to navigate with gamepad and keyboard
    private PlayerInput playerinput;

    private void Awake(){
        playerinput = GetComponent<PlayerInput>();
    }

    private void OnEnable(){
        EventSystem.current.SetSelectedGameObject(firstToSelect);
    }
    public void Singleplayer(){
        menuManager.CountClicks();
        input.enabled = false;
        mainMenuAnimation.SetTrigger("moveRightTrigger");
        difficultyMenuAnimation.SetTrigger("moveUpTrigger");
    }

    public void Classic(){
        menuManager.CountClicks();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Return() {
        menuManager.CountClicks();
        input.enabled = false;
        mainMenuAnimation.SetTrigger("moveLeftTrigger");
        difficultyMenuAnimation.SetTrigger("moveDownTrigger");

        /*if (Input.GetMouseButtonDown(0))
        {
            EventSystem.current.SetSelectedGameObject(objectToSelect);
        }*/
    }

    public void Salir(){
        menuManager.CountClicks();
        Debug.Log("Leaving...");
        Application.Quit();
    }
    public void Settings() {
        menuManager.CountClicks();
        SceneManager.LoadScene(2);
    }
}

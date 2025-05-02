using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public static InputManager instance { get; private set; }

    public bool MenuOpenCloseInput { get; private set; }

    private PlayerInput playerInput;
    private InputAction menuOpenCloseAction;

    private void Awake()
    {
        if (instance != null && instance != this){
            Destroy(gameObject);
        }
        else{
            instance = this;
            transform.parent = null;
            DontDestroyOnLoad(gameObject);
        }
        playerInput = GetComponent<PlayerInput>();
        menuOpenCloseAction = playerInput.actions["MenuOpenClose"];
    }
    private void Update(){
        MenuOpenCloseInput = menuOpenCloseAction.WasPressedThisFrame();
    }
}

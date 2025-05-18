using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    // You can acces this script from everybody but only can be modified in this script
    public static InputManager instance { get; private set; }
    /**
     * With PlayerInput we can move around the canvas
     * With moveInput same as PlayerInput
     */
    private PlayerInput playerInput;
    // Whether the menu is open or closed based on player input

    public bool MenuOpenCloseInput { get; private set; }
    private InputAction menuOpenCloseAction;
    public GameObject[] canvas;
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            transform.parent = null;
            DontDestroyOnLoad(gameObject);
        }
        playerInput = GetComponent<PlayerInput>();
        menuOpenCloseAction = playerInput.actions["MenuOpenClose"];
    }
    private void Update()
    {
        MenuOpenCloseInput = menuOpenCloseAction.WasPressedThisFrame();
    }
}
 
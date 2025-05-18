using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MenuPause : MonoBehaviour{
    [SerializeField] private InputActionAsset actionMapping;
    [SerializeField] private GameObject menuPause;
    private InputAction pause;

    private void Awake() {
        var actionMap = actionMapping.FindActionMap("Controls");
        actionMap.Enable();
        pause = actionMap.FindAction("Pause");
    }
    public void Update() {
        if (pause.triggered) {
            Pause();
        }
    }
    public void Pause() { 
        this.gameObject.SetActive(false);
        menuPause.SetActive(true);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectFinder : MonoBehaviour{
    public GameObject finder;

    private void Start() {
        finder =  FindObjectOfType<MenuManager>()?.gameObject;
    }
}

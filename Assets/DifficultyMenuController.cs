using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultyMenuController : MonoBehaviour
{
    public bool limit = false;
    //Script with the menu movement and the Coroutine
    public NewBehaviourScript menuInicio;

    private void OnTriggerEnter2D(Collider2D collision){
        if (collision.CompareTag("Bounce")) {
            limit = true;
            NewBehaviourScript menuController = FindObjectOfType<NewBehaviourScript>();
            if (menuInicio != null){
                menuInicio.StopMenuCoroutine();
            }

        }

    }
}

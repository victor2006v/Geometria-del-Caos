using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewBehaviourScript : MonoBehaviour
{
    /**
     * This function is called when the Play button is triggered, it opens the Options Scene
     */
    public void Jugar(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    /**
     * This function is called when the Exit button is triggered
     */
    public void Salir(){

        Debug.Log("Leaving...");
        Application.Quit();
    }
}

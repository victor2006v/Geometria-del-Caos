using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class NewBehaviourScript : MonoBehaviour
{
    //get a reference to the GameManager
    public MenuManager menuManager;

    [SerializeField] private GameObject MenuPanelDown;
    [SerializeField] private GameObject firstToSelect;
    [SerializeField]
    Rigidbody2D rgbMain, rgbDifficulty, rgbReturn;
    private float speed = -600f;
    [SerializeField] private DifficultyMenuController difficultyMenuController;
    private Coroutine currentCoroutine;


    //For InputAction to navigate with gamepad and keyboard
    private PlayerInput playerinput;

    /**
     * This function is called when the Play button is triggered, it opens the Options Scene
     */
    private void Awake(){
        playerinput = GetComponent<PlayerInput>();
        difficultyMenuController = GetComponent<DifficultyMenuController>();
    }
    private void OnEnable(){
        EventSystem.current.SetSelectedGameObject(firstToSelect);
    }
    public void Singleplayer(){
        menuManager.CountClicks();
        speed = speed * -1;
        if (currentCoroutine != null)
        {
            StopCoroutine(currentCoroutine);
        }
        currentCoroutine = StartCoroutine(MenuBounceRight(rgbMain, false));
        currentCoroutine = StartCoroutine(MenuGoDown(rgbDifficulty, true));
        
    }

    public void Classic(){
        menuManager.CountClicks();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
    }

    public void Return() {
        menuManager.CountClicks();
        speed = speed * -1;

        if (currentCoroutine != null) {
            StopCoroutine(currentCoroutine);
            rgbDifficulty.velocity = Vector2.zero;
            rgbReturn.velocity = Vector2.zero;
        }

        // Hacer el rebote antes de volver a ejecutar MenuGoDown
        StartCoroutine(ReturnSequence());
    }
    private IEnumerator ReturnSequence() {
        // Rebote hacia arriba antes de bajar de nuevo
        yield return StartCoroutine(MenuUpSoftly(rgbDifficulty));
        yield return StartCoroutine(MenuUpSoftly(rgbReturn));

        // Luego haces el movimiento hacia abajo
        currentCoroutine = StartCoroutine(MenuGoDown(rgbDifficulty, false));
        StartCoroutine(MenuBounceRight(rgbMain, true));
    }
    private IEnumerator MenuBounceRight(Rigidbody2D rgb, bool returnTrue){
        if (returnTrue){
            yield return new WaitForSeconds(0.97f);
        }

        rgb.velocity = new Vector2(-speed * 0.3f, 0);
        yield return new WaitForSeconds(0.1f);

        rgb.velocity = new Vector2(speed, 0);
        yield return new WaitForSeconds(1f); //Menu x to the left

        rgb.velocity = Vector2.zero;

        rgb.velocity = new Vector2(-speed * 0.2f, 0);
        yield return new WaitForSeconds(0.1f);

        rgb.velocity = Vector2.zero;
    }

    private IEnumerator MenuGoDown(Rigidbody2D rgb, bool returnTrue){
        if (returnTrue){
            yield return new WaitForSeconds(0.97f);
        }

        rgb.velocity = new Vector2(0, speed * 0.3f); //Tough movement up
        rgbReturn.velocity = new Vector2(0, speed * 0.3f);
        yield return new WaitForSeconds(0.1f);

        rgb.velocity = new Vector2(0, -speed);
        rgbReturn.velocity = new Vector2(0, -speed);
        yield return new WaitForSeconds(2f); //Menu down y

        rgb.velocity = Vector2.zero;
        rgbReturn.velocity = Vector2.zero;

        rgb.velocity = new Vector2(0, speed * 0.2f);
        rgbReturn.velocity = new Vector2(0, speed * 0.2f);
        yield return new WaitForSeconds(0.1f);

        
    }
    public void StopMenuCoroutine(){
        if (currentCoroutine != null){
            StopCoroutine(currentCoroutine);
            rgbDifficulty.velocity = Vector2.zero;
            rgbReturn.velocity = Vector2.zero;
            Debug.Log("Parao — coroutine detenida");
            StartCoroutine(MenuUpSoftly(rgbDifficulty));
            StartCoroutine(MenuUpSoftly(rgbReturn));
            currentCoroutine = null;
        }
    }

    private IEnumerator MenuUpSoftly(Rigidbody2D rgb) {
        rgb.velocity = new Vector2(0, speed * 0.3f);
        yield return new WaitForSeconds(0.1f);
        rgb.velocity = Vector2.zero;
    }

    /**
     * This function is called when the Exit button is triggered
     */
    public void Salir(){
        menuManager.CountClicks();
        Debug.Log("Leaving...");
        Application.Quit();
    }
    public void Settings() {
        menuManager.CountClicks();
        SceneManager.LoadScene(1);
    }
}

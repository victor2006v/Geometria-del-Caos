using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class LetterManager : MonoBehaviour {
    public static LetterManager instance;
    public TextMeshProUGUI[] letters; // Letras a mostrar
    [SerializeField] private InputActionAsset inputActionMapping; // Asset de controles
    private InputAction left, right, up, down, submit, back; // Acción de entrada
    private char[] currentLetters = new char[3]; // Letras actuales
    private int currentLetterIndex = 0; // Índice de la letra seleccionada
    public string playerName;
    private bool done;
    private int cont;

    [SerializeField] private GameObject doneButton;
    [SerializeField] private GameObject letter1;

    [SerializeField] private AudioClip classicSFX, okSFX, selectSFX, saveSFX, cancelSFX, moveSFX;

    private void Awake() {
        if (instance != null && instance != this) {
            Destroy(gameObject);
            Debug.Log("Destroyed");
        } else { 
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        inputActionMapping.Enable();
        left = inputActionMapping.FindActionMap("MenuName").FindAction("MoveLeft");
        right = inputActionMapping.FindActionMap("MenuName").FindAction("MoveRight");
        up = inputActionMapping.FindActionMap("MenuName").FindAction("MoveUp");
        down = inputActionMapping.FindActionMap("MenuName").FindAction("MoveDown");
        submit = inputActionMapping.FindActionMap("MenuName").FindAction("Submit");
        back = inputActionMapping.FindActionMap("MenuName").FindAction("Back");
        done = false;
        cont = 0;
    }

    private void OnEnable() {

        // Inicialización de las letras a 'A'
        for (int i = 0; i < 3; i++) {
            currentLetters[i] = 'A';
            letters[i].text = "A";
        }

        HighlightCurrentLetter(); // Resalta la letra seleccionada
    }

    private void Update() {
        if (right.triggered && !done) {
            currentLetterIndex++;
            if (currentLetterIndex >= letters.Length) {
                currentLetterIndex = 0;
            }
            SoundFXManager.instance.PlaySoundFXClip(selectSFX, transform, 0.2f, false);
        } else if (left.triggered && !done) { 
            currentLetterIndex--;
            if (currentLetterIndex < 0) {
                currentLetterIndex = 2;
            }
            SoundFXManager.instance.PlaySoundFXClip(selectSFX, transform, 0.2f, false);
        }

        if (down.triggered && !done) {
            currentLetters[currentLetterIndex]++;
            if (currentLetters[currentLetterIndex] > 'Z')
                currentLetters[currentLetterIndex] = 'A';
            SoundFXManager.instance.PlaySoundFXClip(moveSFX, transform, 1f, false);
        } else if (up.triggered && !done) {
            currentLetters[currentLetterIndex]--;
            if (currentLetters[currentLetterIndex] < 'A')
                currentLetters[currentLetterIndex] = 'Z';
            SoundFXManager.instance.PlaySoundFXClip(moveSFX, transform, 1f, false);
        }

        if (submit.triggered)
        {
            done = true;
            cont++;
            if (cont == 1)
            {
                SoundFXManager.instance.PlaySoundFXClip(okSFX, transform, 0.2f, false);
            }
            EventSystem.current.SetSelectedGameObject(doneButton);
            if (submit.triggered && cont == 2)
            {
                
                Done();
            }
        }

        if (back.triggered && done)
        {
            done = false;
            cont--;
            SoundFXManager.instance.PlaySoundFXClip(cancelSFX, transform, 1f, false);
            EventSystem.current.SetSelectedGameObject(letter1);
        }

        // Actualiza las letras en la UI
        UpdateLetters();
        HighlightCurrentLetter(); // Resalta la letra actual

    }

    private void UpdateLetters() {
        // Actualiza las letras en el UI
        for (int i = 0; i < 3; i++) {
            letters[i].text = currentLetters[i].ToString();
        }
    }

    private void HighlightCurrentLetter() {
        // Resalta la letra seleccionada
        for (int i = 0; i < 3; i++) {
            if (i == currentLetterIndex) {
                letters[i].color = Color.yellow; // Resalta la letra seleccionada
            } else {
                letters[i].color = Color.white; // Las otras letras permanecen en blanco
            }
        }
    }


    public string GetPlayerName() {
        return new string(currentLetters); // Retorna el nombre formado por las letras

    }

    public void Done() {
        playerName = GetPlayerName();
        SoundFXManager.instance.PlaySoundFXClip(saveSFX, transform, 1f, false);
        Debug.Log("Player: " + GetPlayerName());
        BGMusicController.instance.GetComponent<AudioSource>().clip = classicSFX;
        BGMusicController.instance.GetComponent<AudioSource>().Play();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
    }


}

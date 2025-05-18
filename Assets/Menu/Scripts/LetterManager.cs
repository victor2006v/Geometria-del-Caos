using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class LetterManager : MonoBehaviour {
    public static LetterManager instance;
    public TextMeshProUGUI[] letters; // Letras a mostrar
    [SerializeField] private InputActionAsset inputActionMapping; // Asset de controles
    private InputAction left, right, up, down; // Acción de entrada
    private char[] currentLetters = new char[3]; // Letras actuales
    private int currentLetterIndex = 0; // Índice de la letra seleccionada
    public string playerName;
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
        if (right.triggered) {
            currentLetterIndex++;
            if (currentLetterIndex >= letters.Length) {
                currentLetterIndex = 0;
            }
        } else if (left.triggered) { 
            currentLetterIndex--;
            if (currentLetterIndex < 0) {
                currentLetterIndex = 2;
            }
        }

        if (up.triggered) {
            currentLetters[currentLetterIndex]++;
            if (currentLetters[currentLetterIndex] > 'Z')
                currentLetters[currentLetterIndex] = 'A';
        } else if (down.triggered) {
            currentLetters[currentLetterIndex]--;
            if (currentLetters[currentLetterIndex] < 'A')
                currentLetters[currentLetterIndex] = 'Z';
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
        Debug.Log("Player: " + GetPlayerName());
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
    }


}

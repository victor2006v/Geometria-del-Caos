using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
    public static GameManager instance;
    public bool ghostPiece;
    public Slider ghostPieceSlider;
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            Debug.Log("Game Manager ha sido destruido");
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }

    private void Start()
    {
        if (!PlayerPrefs.HasKey("ghostPiece"))
        {
            PlayerPrefs.SetFloat("ghostPiece", 1);
            Load();
        }

        else
        {
            Load();
        }
    }

    public void changeGhostPiece()
    {
        if (ghostPieceSlider.value == 0)
        {
            this.ghostPiece = false;
        }
        else if (ghostPieceSlider.value == 1)
        {
            this.ghostPiece = true;
        }
        Save();
    }

    private void Load()
    {
        ghostPieceSlider.value = PlayerPrefs.GetFloat("ghostPiece");
    }

    private void Save()
    {
        PlayerPrefs.SetFloat("ghostPiece", ghostPieceSlider.value);
    }

    private void Update()
    {
        Debug.Log("Existo");
        Debug.Log(ghostPiece);
    }
    public void GhostPiece(System.Single ghostPiece) {
        if (ghostPiece == 0)
        {
            this.ghostPiece = false;
        }
        else if (ghostPiece == 1)
        {
            this.ghostPiece = true;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public static GameManager instance { get; private set; }
    public bool ghostPiece;
    public Ghost ghost;
    private void Awake() {
        if (instance != null && instance != this) {
            Destroy(gameObject);
        } else {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    
    private void GhostPiece() {
        if (ghostPiece) {
            ghost.ghostPiece = ghostPiece;
        } else { 
            ghost.ghostPiece= ghostPiece;
        }
    }
    



}

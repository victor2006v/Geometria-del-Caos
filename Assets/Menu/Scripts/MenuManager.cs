using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
public class MenuManager : MonoBehaviour{

    public int clicks;
    public float current_Time = 0f;
    public static MenuManager instance { get; private set; }
    public string playerName;
    public int score, level, lines_destroyed;
    public float time_played;
    public bool ghostPiece;
    public bool items;
    public float stepDelay;

    private void Update(){
        current_Time = Time.time;
        //Debug.Log("Time: " + Mathf.Round(current_Time));
    }
    private void Awake() {
        if (instance != null && instance != this) {
            Destroy(gameObject);
            Debug.Log("MenuManager ya no existe.");
        } else {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }

    public void CountClicks() {
        clicks++;
        Debug.Log("Clicks: " + clicks);
    }
    public void Statistics() {
        playerName = GameDataController.instance.gameData.name;
        lines_destroyed = GameDataController.instance.gameData.lines_destroyed;
        level = GameDataController.instance.gameData.level;
        score = GameDataController.instance.gameData.score;
        time_played = GameDataController.instance.gameData.time_played;
        ghostPiece = GameDataController.instance.gameData.ghost_piece;
    }
    
    

     
}

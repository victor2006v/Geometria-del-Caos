using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDataController : MonoBehaviour
{
    [SerializeField] Board board;

    [SerializeField] Ghost ghost;

    [SerializeField] public GameDataSerialized gameData = new GameDataSerialized();

    public static GameDataController instance { get; private set; }
    private void Awake(){
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }

    public void saveData(){
        gameData.name = LetterManager.instance.playerName;
        gameData.score = board.score;
        gameData.time_played = board.time;
        gameData.lines_destroyed = board.lines;
        Debug.Log("Linias: " + gameData.lines_destroyed);
        gameData.ghost_piece = ghost.ghostPiece;
        gameData.level = board.level;
        SaveGameData.SaveDataInfo(gameData);
    }

    public void saveName()
    {
        gameData.name = LetterManager.instance.playerName;
        SaveGameData.SaveNameInfo(gameData);
    }
}

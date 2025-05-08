using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDataController : MonoBehaviour
{
    [SerializeField] Board board;

    [SerializeField] Ghost ghost;

    [SerializeField] GameDataSerialized gameData = new GameDataSerialized();

    public void saveData()
    {
        gameData.name = "AAA";
        gameData.score = board.score;
        gameData.time_played = board.time;
        gameData.lines_destroyed = board.lines;
        gameData.ghost_piece = ghost.ghostPiece;
        gameData.level = 1;
        SaveGameData.SaveDataInfo(gameData);
    }
}

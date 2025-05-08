using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SaveGameData : MonoBehaviour
{
    public static GameDataSerialized gameData;

    public static void SaveDataInfo(GameDataSerialized game)
    {
        string json = JsonUtility.ToJson(game);
        File.WriteAllText(Application.persistentDataPath + "GameData.txt", json);
        Debug.Log(Application.persistentDataPath);
        Debug.Log("Partida guardada en la BBDD");
    }

    public static void ClearData()
    {
        if (File.Exists(Application.persistentDataPath + "GameData.json"))
        {
            File.Delete(Application.persistentDataPath + "GameData.json");
            Debug.Log("Documento eliminado");
            return;
        }
        else
        {
            Debug.Log("No existe el documento");
        }
    }
}

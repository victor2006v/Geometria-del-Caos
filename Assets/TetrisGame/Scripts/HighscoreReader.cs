using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class HighscoreReader : MonoBehaviour
{
    void Start()
    {
        string filePath = Path.Combine(Application.persistentDataPath, "../",  "Highscore.dat");

        if (File.Exists(filePath))
        {
            using (BinaryReader reader = new BinaryReader(File.Open(filePath, FileMode.Open)))
            {
                float highscore = reader.ReadSingle();
                Debug.Log($"Highscore: {highscore}");
            }
        }
        else
        {
            Debug.LogError("Highscore.dat not found in StreamingAssets.");
        }
    }
}
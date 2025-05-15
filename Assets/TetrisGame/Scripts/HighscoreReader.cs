using System;
using System.IO;
using UnityEngine;

public class HighscoreReader : MonoBehaviour
{
    [SerializeField]
    public Board board;
    private string filePath;
    private int lastHighscore = -1;

    void Start()
    {
        filePath = Path.Combine(Application.persistentDataPath, "../", "Highscore.dat");
    }

    void Update()
    {
        if (!File.Exists(filePath))
        {
            Debug.LogError("Highscore.dat not found in PersistentDataPath.");
            return;
        }

        int currentHighscore = ReadHighScore();

        if (currentHighscore != lastHighscore)
        {
            this.board.highscore = currentHighscore;
            Debug.Log($"Highscore: {currentHighscore}");
            lastHighscore = currentHighscore;
        }
    }

    int ReadHighScore()
    {
        using (BinaryReader reader = new BinaryReader(File.Open(filePath, FileMode.Open)))
        {
            int highscore = reader.ReadInt32();
            highscore >>= 16;
            int hword = (highscore & 0x00FF) << 8;
            int lword = (highscore & 0xFF00) >> 8;
            return hword + lword;
        }
    }
}

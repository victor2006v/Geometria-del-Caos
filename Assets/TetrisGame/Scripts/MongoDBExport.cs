using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEditor.PackageManager;
using UnityEngine;

public class MongoDBExport : MonoBehaviour{
    public static MongoDBExport instance;
    private MongoClient client;
    private IMongoDatabase database;
    private IMongoCollection<BsonDocument> collection;
    private string playerName;
    private double time, timePlayed, score;
    private bool GhostPiece;
    private int level, lines_Destroyed;


    private void Awake() {
        if (instance != null && instance != this) {
            Destroy(gameObject);
        } else {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start() {
        string connectionString = "mongodb+srv://a22viccarnav:victor2006@geometriacluster.zxtqsud.mongodb.net/";
        try {
            client = new MongoClient(connectionString);
            database = client.GetDatabase("GEOMETRIADELCAOS");
            collection = database.GetCollection<BsonDocument>("HighScoreCollection");
            ExportData();
            
        } catch (System.Exception e) {
            Debug.LogError("MongoDB Connection Error: " + e.Message);
        }

        

    }

    private void ExportData() {
        try {
            var allDocuments = collection.Find(new BsonDocument()).ToList();

            foreach(BsonDocument doc in allDocuments){
                Debug.Log(doc.ToJson());
                playerName = doc["Name"].AsString;
                time = doc["Time"].AsDouble;
                timePlayed = doc["Time Played"].AsDouble;
                score = doc["Score"].AsDouble;
                GhostPiece = doc["Ghost Piece"].AsBoolean;
                level = doc["Level"].AsInt32;
                lines_Destroyed = doc["Lines_Destroyed"].AsInt32;
            }
            /*
            Debug.Log("Player: " + playerName);
            Debug.Log("Time: " + time);
            Debug.Log("Score: " + score);
            Debug.Log("Level: " + level);
            Debug.Log("Time Played: " + timePlayed);
            Debug.Log("GhostPiece: " + GhostPiece);
            Debug.Log("Lines: " +  lines_Destroyed);
            */
        } catch(System.Exception e) {
            Debug.Log("Error exporting data: " + e.Message);
        }
    
    }
}

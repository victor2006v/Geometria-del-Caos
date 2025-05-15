using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

public class MongoDBExport : MonoBehaviour{
    public static MongoDBExport instance;
    private MongoClient client;
    private IMongoDatabase database;
    private IMongoCollection<BsonDocument> collection;
    private string playerName;
    private float time, timePlayed, score;
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
                playerName = doc["Name"].AsString;
                time = (float)doc["Time"].ToDouble();
                timePlayed = (float)doc["Time Played"].ToDouble();
                score = (float)doc["Score"].ToDouble();
                GhostPiece = doc["Ghost Piece"].AsBoolean;
                level = doc["Level"].AsInt32;
                lines_Destroyed = doc["Lines_Destroyed"].AsInt32;
                Debug.Log($"Name: {playerName}, " +
                $"Score: {score}, " +
                $"Time: {time}, " +
                $"TimePlayed: {timePlayed}, " +
                $"GhostPiece: {GhostPiece}," +
                $" Level: {level}, " +
                $"Lines: {lines_Destroyed}");
            }

            
        } catch (System.Exception e) {
            Debug.Log("Error exporting data: " + e.Message);
        }
    } 
}

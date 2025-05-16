using MongoDB.Bson;
using MongoDB.Driver;
using UnityEngine;

public class MongoConnection : MonoBehaviour {
    public static MongoConnection instance;
    private MongoClient client;
    private IMongoDatabase database;
    private IMongoCollection<BsonDocument> collection;
    //SIngleton Behaviour
    private void Awake() {
        if (instance != null && instance != this) {
            Destroy(gameObject);
        } else {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }

    public void InitializeMongo() {
        string connectionString = "mongodb+srv://a22viccarnav:victor2006@geometriacluster.zxtqsud.mongodb.net/";
        try {
            client = new MongoClient(connectionString);
            database = client.GetDatabase("GEOMETRIADELCAOS");
            collection = database.GetCollection<BsonDocument>("HighScoreCollection");

            // Test insert
            var document = new BsonDocument { { "Name: ", MenuManager.instance.playerName } 
                ,{ "Lines_Destroyed: ", MenuManager.instance.lines_destroyed }
                , {"Time: ", MenuManager.instance.current_Time}
                , {"Time Played", MenuManager.instance.time_played }
                , {"Ghost Piece: ", MenuManager.instance.ghostPiece }
                , {"Score: ", MenuManager.instance.score}
                , {"Level: ", MenuManager.instance.level }
                , {"Total Clicks: ", MenuManager.instance.clicks }
            };
            collection.InsertOne(document);
            Debug.Log("Documento insertado correctamente.");
        } catch (System.Exception e) {
            Debug.LogError("MongoDB Connection Error: " + e.Message);
        }
    }
}

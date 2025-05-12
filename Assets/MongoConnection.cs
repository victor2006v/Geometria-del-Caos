using MongoDB.Bson;
using MongoDB.Driver;
using UnityEngine;

public class MongoConnection : MonoBehaviour {
    private static MongoConnection instance;
    private MongoClient client;
    private IMongoDatabase database;
    private IMongoCollection<BsonDocument> collection;
    //SIngleton Behaviour
    private void Awake() {
        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(gameObject); //  esto lo mantiene entre escenas
            InitializeMongo();
        } else {
            Destroy(gameObject); // evita duplicados
        }
    }

    private void InitializeMongo() {
        string connectionString = "mongodb+srv://a22viccarnav:victor2006@geometriacluster.zxtqsud.mongodb.net/";
        try {
            client = new MongoClient(connectionString);
            database = client.GetDatabase("GEOMETRIADELCAOS");
            collection = database.GetCollection<BsonDocument>("HighScoreCollection");

            // Test insert
            var document = new BsonDocument { { "Pepe", 100 } };
            collection.InsertOne(document);
            Debug.Log("Documento insertado correctamente.");
        } catch (System.Exception e) {
            Debug.LogError("MongoDB Connection Error: " + e.Message);
        }
    }


}

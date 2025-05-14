using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ItemManager : MonoBehaviour{
    public static ItemManager instance;
    private float itemTimer;
    public List<GameObject> prefabs;
    private int itemGenerable;
    public GameObject prefab; //placeholder
    private float nextSpawnTime;
    //private PrefabAssetType 
    private void Awake(){
        if (instance != null && instance != this){
            Destroy(gameObject);
        }
        else { 
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        itemTimer = 0f;
        itemGenerable = 0;
        nextSpawnTime = Random.Range(45f, 60f);
    }

    private void Update(){
        itemTimer += Time.deltaTime;
        SpawnTimer();
    }
    
    private void SpawnTimer(){
        if (itemTimer >= nextSpawnTime) {
            itemGenerable = Random.Range(0, prefabs.Count);
            for (int i = 0; i < prefabs.Count; i++) {
                if (itemGenerable == i) { 
                    prefab = prefabs[i];
                }
            }
            itemTimer = 0f;

            itemGenerable = 0;
            nextSpawnTime = Random.Range(45f, 60f);
            InstantiateItem();
            ItemGoDown();
        }
        
    }
    public void InstantiateItem(){
        Instantiate(prefab, new Vector3(0f, 9, 0), Quaternion.identity);
    }
    public void ItemGoDown() {
        prefab.transform.position = new Vector3(0, -transform.position.y, 0);
    }


}

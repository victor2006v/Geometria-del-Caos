using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static UnityEditor.Progress;

public class ItemManager : MonoBehaviour{
    public static ItemManager instance;
    private float itemTimer;
    public List<GameObject> prefabs;
    private int itemGenerable, clockMaxTime;
    public GameObject prefab; //placeholder
    private float nextSpawnTime, clockTime;
    private bool itemFalling = false;
    private Rigidbody2D itemRb;
    private GameObject spawnedItem;
    private Animator animator;

    [SerializeField] private AudioClip clockSound;
    [SerializeField] private AudioClip explosionSound;
    [SerializeField] private Piece pieces;
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
        clockTime = 0f;
        clockMaxTime = 10;
        nextSpawnTime = Random.Range(45f, 60f);
    }

    private void Update(){
        itemTimer += Time.deltaTime;
        SpawnTimer();
        StopItem();
        clockTime += Time.deltaTime;
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
        }    
    }
    private void InstantiateItem(){
        spawnedItem = Instantiate(prefab, new Vector3(0f, 9, 0), Quaternion.identity);
        animator = spawnedItem.GetComponent<Animator>();
        itemFalling = true;
        ItemGoDown(spawnedItem);
         
        
    }
    //Is only called if the item has the tag bomb
    private void ItemGoDown(GameObject item) {
        itemRb = item.GetComponent<Rigidbody2D>();
        itemRb.velocity = new Vector2(0, -5f);

    }
    private void StopItem() {
        if (itemFalling && spawnedItem != null) {
            if (spawnedItem.transform.position.y <= 0.1f) {
                itemRb.velocity = Vector2.zero;
                spawnedItem.transform.position = new Vector2(spawnedItem.transform.position.x, 0);
                itemFalling = false;
                if (spawnedItem.CompareTag("Bomb")) {
                    Explode();
                } else if (spawnedItem.CompareTag("FastClock")) {
                    Clock();
                    StartCoroutine(ClockStepDelay(-0.2f, clockMaxTime));
                } else if (spawnedItem.CompareTag("FreezeClock")) {
                    Clock();
                    StartCoroutine(ClockStepDelay(1f, clockMaxTime));
                }
                    
            }
            
        }
    }

    private void Explode() {   
        //Animation
        animator.SetBool("explode", true);
        StartCoroutine(DelayExplosion(3f));   
    }

    //Coroutine delay explosion
    private IEnumerator DelayExplosion(float delay) { 
        yield return new WaitForSeconds(delay);
        Destroy(this.spawnedItem);
        Board.instance.tilemap.ClearAllTiles();
    }

    private void Clock() {
        //animation
        animator.SetBool("ClockOn", true);
        StartCoroutine(DelayTime(3f));
    }

    //Coroutine delay clockanimation
    private IEnumerator DelayTime(float delay) {
        yield return new WaitForSeconds(delay);
        Destroy(this.spawnedItem);
    }

    private IEnumerator ClockStepDelay(float amount, float duration) { 
        pieces.stepDelay += amount;
        yield return new WaitForSeconds(duration);
        pieces.stepDelay -= amount;
    }
}

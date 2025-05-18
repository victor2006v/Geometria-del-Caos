using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ItemManager : MonoBehaviour{
    public static ItemManager instance;
    private float itemTimer;
    public List<GameObject> prefabs;
    private int itemGenerable, clockMaxTime;
    public GameObject prefab; //placeholder
    private float nextSpawnTime, clockTime, pointsTime;
    private bool itemFalling = false;
    private Rigidbody2D itemRb;
    private GameObject spawnedItem;
    private Animator animator;
    [SerializeField] private TileBase tile;
    [SerializeField] private AudioClip clockSound;
    [SerializeField] private AudioClip explosionSound;
    [SerializeField] private Piece pieces;
    [SerializeField] private Camera mainCamera;
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
        nextSpawnTime = Random.Range(10f, 20f);
        pointsTime = 0f;
    }

    private void Update(){
        itemTimer += Time.deltaTime;
        SpawnTimer();
        StopItem();
        clockTime += Time.deltaTime;
        pointsTime += Time.deltaTime;
    }

    private void SpawnTimer() {
        if (itemTimer >= nextSpawnTime) {
            itemGenerable = Random.Range(0, prefabs.Count);
            prefab = prefabs[itemGenerable];

            itemTimer = 0f;
            itemGenerable = 0;
            nextSpawnTime = Random.Range(15f, 20f);
            InstantiateItem();
        }
    }
    private void InstantiateItem(){
        spawnedItem = Instantiate(prefab, new Vector3(0f, 9, 0), Quaternion.identity);
        animator = spawnedItem.GetComponent<Animator>();
        itemFalling = true;
        ItemGoDown(spawnedItem);
    }
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
                if (spawnedItem.CompareTag("Bomb"))
                {
                    Explode();
                }
                else if (spawnedItem.CompareTag("FastClock"))
                {
                    Clock();
                    StartCoroutine(ClockStepDelay(-0.2f, clockMaxTime));
                }
                else if (spawnedItem.CompareTag("FreezeClock"))
                {
                    Clock();
                    StartCoroutine(ClockStepDelay(1f, clockMaxTime));
                }
                else if (spawnedItem.CompareTag("PointMultiPlayer"))
                {
                    augmentPoints();
                }
                else if (spawnedItem.CompareTag("CameraRotation"))
                {
                    //rotateCam();
                }
                else if (spawnedItem.CompareTag("1LineUP"))
                {
                    Destroy(this.spawnedItem);
                    //augmentLine(tile);
                }
                else if (spawnedItem.CompareTag("BombBlocks")) {
                    BombBlocks();
                }

            }
        }
    }

    private void BombBlocks() {
        animator.SetBool("explode", true);
        StartCoroutine(DelayBombBlocks(6f));
    }

    private IEnumerator DelayBombBlocks(float delay){
        yield return new WaitForSeconds(delay);
        Destroy(this.spawnedItem);
        Vector3 worldPos = spawnedItem.transform.position;
        Vector3Int centerPos = Board.instance.tilemap.WorldToCell(worldPos);

        // Dibuja 3x3 tiles alrededor del centro
        for (int dx = -1; dx <= 1; dx++)
        {
            for (int dy = -1; dy <= 1; dy++)
            {
                Vector3Int tilePos = new Vector3Int(centerPos.x + dx, centerPos.y + dy, 0);
                Board.instance.tilemap.SetTile(tilePos, tile);
            }
        }

        AudioSource.PlayClipAtPoint(explosionSound, worldPos);
        
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

    private IEnumerator delayPoints(float duration) {
        Board.instance.multiplier = 2;
        yield return new WaitForSeconds(duration);
        Board.instance.multiplier = 1;
    }
    private void augmentPoints() {
        Destroy(this.spawnedItem);
        StartCoroutine(delayPoints(15f));
    }
    /*
    private void rotateCam() {
        Destroy(this.spawnedItem);
        StartCoroutine(rotateDelay(30f));
       
    }

    private IEnumerator rotateDelay(float duration) {
        mainCamera.transform.rotation = Quaternion.Euler(0f, 0f, 180f);
        yield return new WaitForSeconds(duration);
        mainCamera.transform.rotation = Quaternion.Euler(0f, 0f, 180f);
    }
    */
    /*
    private void augmentLine(TileBase tile) {
        Destroy(this.spawnedItem);
        Tilemap tilemap = Board.instance.tilemap;
        RectInt bounds = Board.instance.Bounds;

        // Mover todas las tiles hacia arriba una unidad
        for (int y = bounds.yMax - 1; y >= bounds.yMin; y--) {
            for (int x = bounds.xMin; x < bounds.xMax; x++) {
                Vector3Int currentPos = new Vector3Int(x, y, 0);
                Vector3Int abovePos = new Vector3Int(x, y + 1, 0);

                TileBase currentTile = tilemap.GetTile(currentPos);
                tilemap.SetTile(abovePos, currentTile);
            }
        }

        // Agregar nueva línea en la parte inferior
        int bottomY = bounds.yMin;
        for (int x = bounds.xMin; x < bounds.xMax; x++) {
            Vector3Int position = new Vector3Int(x, bottomY, 0);
            tilemap.SetTile(position, tile);
        }
    }
        */
    
}

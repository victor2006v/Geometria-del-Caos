using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {
    [SerializeField] GameObject piecePrefab;          // Prefab of the piece to spawn
    [SerializeField] RectTransform Canvas_Pieces;   // Reference to the UI canvas
    private float spawnTimer = 0f;
    private float spawnInterval = 3.5f; // Every 2 seconds spawn a piece

    private void Update() {
        spawnTimer += Time.deltaTime;
        if (spawnTimer >= spawnInterval) {
            // Instantiate the piece inside the canvas
            GameObject newPiece = Instantiate(piecePrefab, Canvas_Pieces);
            // Call SpawnPiece on the new piece
            PiecesMovement movement = newPiece.GetComponent<PiecesMovement>();
            if (movement != null) {
                movement.SpawnPiece(); // Call the method from PiecesMovement
            }

            spawnTimer = 0f; // Reset timer
        }
    }
}

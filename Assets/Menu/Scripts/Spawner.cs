using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {
    [SerializeField] GameObject piecePrefab;          // Prefab of the piece to spawn
    [SerializeField] RectTransform Canvas_UI;   // Reference to the UI canvas
    private float spawnTimer = 0f;
    private float spawnInterval = 1f; // Every 2 seconds spawn a piece

    private void Update() {
        spawnTimer += Time.deltaTime;
        if (spawnTimer >= spawnInterval) {

            GameObject newPiece = Instantiate(piecePrefab, Canvas_UI);

            // Get the RectTransform of the piece
            RectTransform pieceRect = newPiece.GetComponent<RectTransform>();
            if (pieceRect != null) {
                float canvasWidth = Canvas_UI.rect.width;
                float pieceWidth = pieceRect.rect.width;

                float halfCanvas = canvasWidth / 2f;
                float halfPiece = pieceWidth / 2f;

                // El rango se reduce para que la pieza no se salga del canvas
                float randomX = Random.Range(-halfCanvas + halfPiece, halfCanvas - halfPiece);

                pieceRect.anchorMin = new Vector2(0.5f, 1f);
                pieceRect.anchorMax = new Vector2(0.5f, 1f);
                pieceRect.pivot = new Vector2(0.5f, 1f);

                pieceRect.anchoredPosition = new Vector2(randomX, 0f);
            }
            // Call SpawnPiece on the new piece
            PiecesMovement movement = newPiece.GetComponent<PiecesMovement>();

            if (movement != null) {
                movement.SpawnPiece(); // Call the method from PiecesMovement
            }

            spawnTimer = 0f; // Reset timer
        }
    }
}

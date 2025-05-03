using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PiecesMovement : MonoBehaviour {
    private Rigidbody2D rb;
    [SerializeField] float speed;
    public RectTransform canvasTransform;
    [SerializeField] Sprite[] sprites;
    public Image pieceImage;
    private float spawnTimer = 0f;
    private float spawnInterval = 2f; // Every 2 seconds spawn a piece

    public enum PieceType {
        Type1,
        Type2,
        Type3,
        Type4,
        Type5,
        Type6,
        Type7,
        Type8,
        Type9,
        Type10
    }
    [SerializeField] private PieceType pieceType;
    private void Start() {
        rb = GetComponent<Rigidbody2D>();
    }
    // Update is called once per frame
    private void FixedUpdate() {
        rb.velocity = new Vector2(rb.velocity.x, -speed);
    }
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Floor")) { 
            Destroy(this.gameObject);
        }
    }   

    public void SpawnPiece() {
        // Aseguramos que el RectTransform de la pieza actual esté correctamente ajustado
        RectTransform rectTransform = this.GetComponent<RectTransform>();

        // Ajustamos los anclajes para que la pieza esté en la parte superior
        rectTransform.anchorMin = new Vector2(0.5f, 1f);  // Anclaje en el centro horizontal, arriba
        rectTransform.anchorMax = new Vector2(0.5f, 1f);  // Mismo valor que anchorMin para que se quede en el borde

        // Colocamos la pieza en la parte superior del Canvas
        rectTransform.anchoredPosition = new Vector2(600f, -10f);
        RandomPieceType();
        SetPiece(pieceType);
    }
    public void SetPiece(PieceType type) {
        pieceImage.sprite = sprites[(int)type];
    }

    public void RandomPieceType() {
        pieceType = (PieceType)Random.Range(0, System.Enum.GetValues(typeof(PieceType)).Length);
    }   

}

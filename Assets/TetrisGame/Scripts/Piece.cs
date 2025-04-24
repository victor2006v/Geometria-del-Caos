using UnityEngine;
using static GeometriaDelCaos;

public class Piece : MonoBehaviour
{
    public Board board { get; private set; }
    public PiecesData data { get; private set; }
    public Vector3Int[] cells { get; private set; }
    public Vector3Int position { get; private set; }

    public void Initialize(Board board, Vector3Int position, PiecesData data) { 
        this.board = board;
        this.position = position;
        this.data = data;
        if (this.cells == null) { 
            this.cells = new Vector3Int[data.cells.Length];
        }
        for (int i = 0; i < this.cells.Length; i++) {
            this.cells[i] = (Vector3Int)data.cells[i];
        }
    }
    private void Update()
    {

    }
    /*
    private void Move(Vector2Int translation)
    {
        Vector3Int newPosition = this.position;
        newPosition.x += translation.x;
        newPosition.y += translation.y;

        bool valid = this.board.IsValidPosition(this,newPosition);
        if (valid) {
            this.position = newPosition;
        }
        return valid;
    }
    */
}

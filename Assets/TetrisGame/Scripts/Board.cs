using UnityEngine;
using UnityEngine.Tilemaps;
using static GeometriaDelCaos;

public class Board : MonoBehaviour
{
    /*It stores the tilemap of the board. Get: It can be accessed by other different classes
     * Set it can be only modified in this class*/
    public Tilemap tilemap { get; private set; }
    /*Same happens to the activePiece get is accessed by other classes and set private only can be modified here*/
    public Piece activePiece { get; private set; }
    /*Array to access the pieces by order, there are 7 types of pieces, each one has differents forms*/
    public PiecesData[] pieces;
    /*The point where the pieces are going to spawn*/
    public Vector3Int spawnPosition = new Vector3Int(-1, 8, 0);
    /*The x are the amount of columns and 20 the amounts of rows*/
    public Vector2Int boardSize = new Vector2Int(10, 20);
    /*It creates a delimiter to know the borders and contain the pieces inside */
    public RectInt Bounds
    {
        get
        {
            Vector2Int position = new Vector2Int(-this.boardSize.x / 2, -this.boardSize.y / 2);
            return new RectInt(position, this.boardSize);
        }
    }
    /*It stores the tilemap component of the children and also the Piece Script*/
    private void Awake()
    {
        this.tilemap = GetComponentInChildren<Tilemap>();
        this.activePiece = GetComponentInChildren<Piece>();
        for (int i = 0; i < pieces.Length; i++)
        {
            this.pieces[i].Initialize();
        }
    }
    private void Start()
    {
        SpawnPiece();
    }
    //It spawns one of the seven pieces randomly above the board in the center with the spawnPosition
    public void SpawnPiece()
    {
        int random = Random.Range(0, this.pieces.Length);
        PiecesData data = this.pieces[random];

        this.activePiece.Initialize(this, this.spawnPosition, data);
        if(IsValidPosition(activePiece, spawnPosition)) {
            Set(activePiece);
        } else
        {
            GameOver();
        }
    }
    /*Function that is called when the player dies*/
    public void GameOver() { 
    }

    public void Set(Piece piece)
    {
        for (int i = 0; i < piece.cells.Length; i++)
        {
            Vector3Int tilePosition = piece.cells[i] + piece.position;
            this.tilemap.SetTile(tilePosition, piece.data.tile);
        }
    }
    /*When the piece is in a new position, it clears the old tiles*/
    public void Clear(Piece piece)
    {
        for (int i = 0; i < piece.cells.Length; i++)
        {
            Vector3Int tilePosition = piece.cells[i] + piece.position;
            this.tilemap.SetTile(tilePosition, null);
        }
    }

    public bool IsValidPosition(Piece piece, Vector3Int position)
    {
        RectInt bounds = Bounds;
        for (int i = 0; i < piece.cells.Length; i++)
        {
            Vector3Int tilePosition = piece.cells[i] + position;

            if (!bounds.Contains((Vector2Int)tilePosition))
            {
                return false;
            }

            if (this.tilemap.HasTile(tilePosition))
            {
                return false;
            }
        }
        return true;
    }
}


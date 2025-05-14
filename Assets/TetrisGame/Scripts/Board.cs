using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;
using TMPro;
/*We need it to use methods from Pieces class GeometriaDelCaos*/
using static GeometriaDelCaos;

public class Board : MonoBehaviour
{
    [SerializeField] AudioClip holdSFX, singleSFX, doubleSFX, tripleSFX, tetrisSFX;

    /*It stores the tilemap of the board created with Create> 2D > Tilemap. Get: It can be accessed by other different classes
     * Set it can be only modified in this class*/
    public Tilemap tilemap { get; private set; }
    [SerializeField]
    public Tilemap previewTilemap;
    [SerializeField]
    public Tilemap saveTilemap;
    /*Same happens to the activePiece get is accessed by other classes and set private only can be modified here*/
    public Piece activePiece { get; private set; }
    /*Array to access the pieces by order, there are 7 types of pieces, each one has differents forms*/
    public PiecesData[] pieces;
    /*The point where the pieces are going to spawn*/
    public Vector3Int spawnPosition = new Vector3Int(-1, 8, 0);
    /*The x are the amount of columns and 20 the amounts of rows*/
    public Vector2Int boardSize = new Vector2Int(10, 20);

    private PiecesData nextPieceData;
    private PiecesData savePieceData;
    private bool hasSaved;
    private bool hasSavedPiece;
    [SerializeField]
    private TMP_Text playerText, scoreText, levelText, linesText, timeText;

    public string player { get; set; }
    public int score { get; set; }
    public int level { get; set; } = 1;
    public int lines { get; set; } = 0;
    private int previousLines = 0;
    public float time { get; set; }
    /*It returns a rectangle that represents a delimiter to know the borders and contain the pieces inside */
    public RectInt Bounds
    {
        get
        {
            /**
             * Result boardSize.x is -(10/2) which is -5, Result boardSize.y which is -(20/2) = -10
             * Result Vector2Int(-5, 10) the bottom right corner a
             * 
             */
            Vector2Int position = new Vector2Int(-this.boardSize.x / 2, -this.boardSize.y / 2);
            return new RectInt(position, this.boardSize);
        }
    }
    /*It stores the tilemap component of the children and also the Piece Script*/
    private void Awake()
    {
        /**
         * It stores the tilemap which is in the Children of Board
         */
        this.tilemap = GetComponentInChildren<Tilemap>();

        this.activePiece = GetComponentInChildren<Piece>();
        /**
         * For every piece it call Initialize() method
         */
        for (int i = 0; i < pieces.Length; i++)
        {
            this.pieces[i].Initialize();
        }
    }
    private void Start()
    {
        player = LetterManager.instance.playerName;
        playerText.text = player.ToString();
        this.nextPieceData = GetRandomPieceData();
        GameDataController.instance.saveName();
        SpawnPiece();
    }

    private void Update()
    {
        scoreText.text = score.ToString();
        levelText.text = level.ToString();
        linesText.text = lines.ToString();
        time += Time.deltaTime;
        timeText.text = time.ToString("F2");

        if(lines > previousLines)
        {
            previousLines = lines;
            this.activePiece.AugmentDifficulty();
        }
    }

    private PiecesData GetRandomPieceData()
    {
        int random = Random.Range(0, this.pieces.Length);
        return this.pieces[random];
    }

    //It spawns one of the seven pieces randomly above the board in the center with the spawnPosition
    public void SpawnPiece()
    {
        PiecesData data = this.nextPieceData;
        this.nextPieceData = GetRandomPieceData();

        this.activePiece.Initialize(this, this.spawnPosition, data);

        if(IsValidPosition(this.activePiece, this.spawnPosition)) {
            Set(this.activePiece);
            ShowNextPiece();
            hasSaved = false;
        } else
        {
            GameOver();
        } 
    }

    private void ShowNextPiece()
    {
        previewTilemap.ClearAllTiles();

        Vector2Int[] cells = nextPieceData.cells;

        Vector2 center = Vector2.zero;
        foreach (Vector2Int cell in cells)
        {
            center += (Vector2)cell;
        }
        center /= cells.Length;
        Vector2Int centerRounded = Vector2Int.RoundToInt(center);

        Vector3Int manualOffset = new Vector3Int(-1, -1, 0);

        foreach (Vector2Int cell in cells)
        {
            Vector3Int drawPosition = manualOffset + new Vector3Int(cell.x - centerRounded.x, cell.y - centerRounded.y, 0);
            previewTilemap.SetTile(drawPosition, nextPieceData.tile);
        }
    }

    public void savePiece()
    {
        if (hasSaved)
        {
            return;
        }

        Clear(activePiece);

        if(!hasSavedPiece)
        {
            savePieceData = activePiece.data;
            hasSavedPiece = true;
            SoundFXManager.instance.PlaySoundFXClip(holdSFX, transform, 1f, false);

            SpawnPiece();
        }
        else
        {
            PiecesData temp = activePiece.data;
            activePiece.Initialize(this, spawnPosition, savePieceData);

            if (IsValidPosition(activePiece, spawnPosition))
            {
                savePieceData = temp;
                Set(activePiece);
            }
            else
            {
                GameOver();
            }
        }

        ShowSavePiece();
        hasSaved = true;
    }

    public void ShowSavePiece()
    {
        saveTilemap.ClearAllTiles();

        if (!hasSavedPiece) return;

        Vector2Int[] cells = savePieceData.cells;

        Vector2 center = Vector2.zero;
        foreach (Vector2Int cell in cells)
        {
            center += (Vector2)cell;
        }
        center /= cells.Length;
        Vector2Int centerRounded = Vector2Int.RoundToInt(center);

        Vector3Int manualOffset = new Vector3Int(-1, -1, 0);

        foreach (Vector2Int cell in cells)
        {
            Vector3Int drawPosition = manualOffset + new Vector3Int(cell.x - centerRounded.x, cell.y - centerRounded.y, 0);
            saveTilemap.SetTile(drawPosition, savePieceData.tile);
        }
    }

    /*Function that is called when the player dies*/
    public void GameOver() {
        GameDataController.instance.saveData();
        MenuManager.instance.Statistics();
        MongoConnection.instance.InitializeMongo();
        this.tilemap.ClearAllTiles();
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
        RectInt bounds = this.Bounds;
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

    public void clearLines()
    {
        RectInt bounds = this.Bounds;
        int row = bounds.yMin;
        int contLineas = 0;

        while (row < bounds.yMax)
        {
            if (IsLineFull(row))
            {
                LineClear(row);
                contLineas++;
            }
            else
            {
                row++;
            }
        }

        if (contLineas == 1)
        {
            score += 100;
            SoundFXManager.instance.PlaySoundFXClip(singleSFX, transform, 1f, false);
        }
        else if (contLineas == 2)
        {
            score += 200;
            SoundFXManager.instance.PlaySoundFXClip(doubleSFX, transform, 1f, false);
        }
        else if (contLineas == 3)
        {
            score += 300;
            SoundFXManager.instance.PlaySoundFXClip(tripleSFX, transform, 1f, false);
        }
        else if (contLineas == 4)
        {
            score += 1200;
            SoundFXManager.instance.PlaySoundFXClip(tetrisSFX, transform, 1f, false);
        }
    }

    private bool IsLineFull(int row)
    {
        RectInt bounds = this.Bounds;

        for (int col = bounds.xMin; col < bounds.xMax; col++)
        {
            Vector3Int position = new Vector3Int(col, row, 0);

            if (!this.tilemap.HasTile(position))
            {
                return false;
            }
        }

        return true;
    }

    private void LineClear(int row)
    {
        RectInt bounds = this.Bounds;

        for (int col = bounds.xMin; col < bounds.xMax; col++)
        {
            Vector3Int position = new Vector3Int(col, row, 0);
            this.tilemap.SetTile(position, null);
        }

        while (row < bounds.yMax)
        {
            for (int col = bounds.xMin; col < bounds.xMax; col++)
            {
                Vector3Int position = new Vector3Int(col, row + 1, 0);
                TileBase above = this.tilemap.GetTile(position);

                position = new Vector3Int(col, row, 0);
                this.tilemap.SetTile(position, above);
            }
            row++;
        }
        lines++;    
    }
}


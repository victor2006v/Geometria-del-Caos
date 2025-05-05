using System;
using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.InputSystem;
using static GeometriaDelCaos;

public class Piece : MonoBehaviour
{
    [SerializeField] InputActionAsset inputActionMapping;
    InputAction right, left, rotation_left, rotation_right, down, blockPiece, savePiece;
    public Board board { get; private set; }
    public PiecesData data { get; private set; }
    public Vector3Int[] cells { get; private set; }
    public Vector3Int position { get; private set; }
    public int rotationIndex { get; private set; }

    public float stepDelay = 1f;
    public float lockDelay = 0.01f;

    private float stepTime;
    private float lockTime;
    private float playTimeEasy = 120f;
    private float playTime = 300f;
    private int cont = 0;

    private bool hasAugmentedEasy = false;
    private bool hasAugmentedMedium = false;
    private bool hasAugmentedHard = false;

    private float downRepeatDelay = 0.1f;
    private float nextDownTime = 0f;
    private float horizontalRepeatDelay = 0.15f;
    private float horizontalInitialDelay = 0.25f;
    private float nextHorizontalTime = 0f;
    private int horizontalDirection = 0;
    private bool wasHoldingHorizontal = false;

    [SerializeField]
    GameObject Music;
    [SerializeField]
    AudioClip difficultyChange;

    public void Awake(){
        inputActionMapping.Enable();
        right = inputActionMapping.FindActionMap("Controls").FindAction("Right");
        left = inputActionMapping.FindActionMap("Controls").FindAction("Left");
        rotation_left = inputActionMapping.FindActionMap("Controls").FindAction("Rotation_Left");
        rotation_right = inputActionMapping.FindActionMap("Controls").FindAction("Rotation_Right");
        down = inputActionMapping.FindActionMap("Controls").FindAction("Down");
        blockPiece = inputActionMapping.FindActionMap("Controls").FindAction("BlockPiece");
        savePiece = inputActionMapping.FindActionMap("Controls").FindAction("SavePiece");
    }
    public void Initialize(Board board, Vector3Int position, PiecesData data) { 
        this.board = board;
        this.position = position;
        this.data = data;
        this.rotationIndex = 0;
        this.stepTime = Time.time + this.stepDelay;
        this.lockTime = 0f;

        if (this.cells == null) { 
            this.cells = new Vector3Int[data.cells.Length];
        }
        for (int i = 0; i < this.cells.Length; i++) {
            this.cells[i] = (Vector3Int)data.cells[i];
        }
    }
    /*CONTROLS*/
    private void Update()
    {
        //Clean the board
        this.board.Clear(this);

        this.lockTime += Time.deltaTime;

        bool leftHeld = left.IsPressed();
        bool rightHeld = right.IsPressed();

        if (leftHeld && !rightHeld) {
            HorizontalHold(-1)  ;
        } else if (rightHeld && !leftHeld) {
            HorizontalHold(1);
        }
        else
        {
            horizontalDirection = 0;
            wasHoldingHorizontal = false;
            nextHorizontalTime = Time.time;
        }

        if (rotation_left.triggered) {
            Rotate(-1);
        } else if (rotation_right.triggered) { 
            Rotate(1); 
        }

        if (down.IsPressed())
        {
            if (Time.time >= nextDownTime)
            {
                Move(Vector2Int.down);
                nextDownTime = Time.time + downRepeatDelay;
            }
        }
        else
        {
            nextDownTime = Time.time;
        }

        if (blockPiece.triggered) {
            HardDrop();
        }

        if (Time.time >= this.stepTime) 
        {
            Step();
        }

        if (savePiece.triggered)
        {
            SavePiece();
        }

        if (Time.time >= this.playTimeEasy && !hasAugmentedEasy)
        {
            AugmentDifficulty();
        }

        if (Time.time >= this.playTime + this.playTimeEasy && !hasAugmentedMedium)
        {
            AugmentDifficulty();
        }

        if (Time.time >= this.playTime * 2 + this.playTimeEasy && !hasAugmentedHard)
        {
            AugmentDifficulty();
        }

        this.board.Set(this);
    }

    private void HorizontalHold(int direction)
    {
        if (horizontalDirection != direction || !wasHoldingHorizontal)
        {
            Move(new Vector2Int(direction, 0));
            horizontalDirection = direction;
            nextHorizontalTime = Time.time + horizontalInitialDelay;
            wasHoldingHorizontal = true;
        }
        else if (Time.time >= nextHorizontalTime)
        {
            Move(new Vector2Int(direction, 0));
            nextHorizontalTime = Time.time + horizontalRepeatDelay;
        }
    }

    private void AugmentDifficulty()
    {
        stepDelay -= 0.3f;
        Music.GetComponent<AudioSource>().pitch += 0.1f;
        hasAugmentedEasy = true;
        if (cont == 1)
        {
            hasAugmentedMedium = true;
        }
        else if (cont == 2) {
            hasAugmentedHard = true;
        }
        Music.GetComponent<AudioSource>().volume = 0;
        AudioSource.PlayClipAtPoint(difficultyChange, new Vector3 () ,1f);
        Invoke("BGMaxVolume", 1.2f);
        cont++;
    }

    private void BGMaxVolume()
    {
        Music.GetComponent<AudioSource>().volume = 1f;
    }

    private void SavePiece()
    {
        this.board.savePiece();
    }

    private void Step()
    {
        this.stepTime = Time.time + this.stepDelay;

        Move(Vector2Int.down);

        if(this.lockTime >= this.lockDelay)
        {
            Lock();
        }
    }

    private void Lock() {
        this.board.Set(this);
        this.board.score += 10;
        this.board.clearLines();
        this.board.SpawnPiece();
    }

    /*To block the piece*/
    private void HardDrop() {
        while (Move(Vector2Int.down)) {
            continue;
        }

        Lock();
    }
    /*It moves the piece but before it checks if the position is valid*/
    private bool Move(Vector2Int translation)
    {
        Vector3Int newPosition = this.position;
        newPosition.x += translation.x;
        newPosition.y += translation.y;

        bool valid = this.board.IsValidPosition(this,newPosition);

        if (valid) {
            this.position = newPosition;
            this.lockTime = 0f;
        }
        return valid;
    }

    private IEnumerator MoveDelay(Vector3Int newPosition)
    {
        yield return 1f;
        position = newPosition;
        
    }

    private void Rotate(int direction) {
        int originalRotation = this.rotationIndex;
        this.rotationIndex = Wrap(this.rotationIndex + direction, 0, 4);

        ApplyRotationMatrix(direction);

        if (!TestWallKicks(this.rotationIndex, direction))
        {
            this.rotationIndex = originalRotation;
            ApplyRotationMatrix(-direction);
        }
    }

    private void ApplyRotationMatrix(int direction)
    {
        for (int i = 0; i < this.cells.Length; i++)
        {
            Vector3 cell = this.cells[i];

            int x, y;

            switch (this.data.piece)
            {
                case Pieces.I:
                case Pieces.O:
                    cell.x -= 0.5f;
                    cell.y -= 0.5f;
                    x = Mathf.CeilToInt((cell.x * Data.RotationMatrix[0] * direction) + (cell.y * Data.RotationMatrix[1] * direction));
                    y = Mathf.CeilToInt((cell.x * Data.RotationMatrix[2] * direction) + (cell.y * Data.RotationMatrix[3] * direction));
                    break;
                default:
                    x = Mathf.RoundToInt((cell.x * Data.RotationMatrix[0] * direction) + (cell.y * Data.RotationMatrix[1] * direction));
                    y = Mathf.RoundToInt((cell.x * Data.RotationMatrix[2] * direction) + (cell.y * Data.RotationMatrix[3] * direction));
                    break;
            }
            this.cells[i] = new Vector3Int(x, y, 0);
        }
    }

    private bool TestWallKicks(int rotationIndex, int rotationDirection)
    {
        int wallKickIndex = GetWallKickIndex(rotationIndex, rotationDirection);

        for (int i = 0; i < this.data.wallKicks.GetLength(1); i++)
        {
            Vector2Int translation = this.data.wallKicks[wallKickIndex, i];

            if (Move(translation))
            {
                return true;
            }
        }
        return false;
    }

    private int GetWallKickIndex(int rotationIndex, int rotationDirection) 
    {
        int wallKickIndex = rotationIndex * 2;

        if (rotationDirection < 0)
        {
            wallKickIndex--;
        }

        return Wrap(wallKickIndex, 0, this.data.wallKicks.GetLength(0));
    }

    private int Wrap(int input, int min, int max) {
        if (input < min)
        {
            return max - (min - input) % (max - min);
        }
        else { 
            return min + (input - min) % (max - min);
        }
    }
    
}

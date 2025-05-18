using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static GeometriaDelCaos;

public class Piece : MonoBehaviour
{
    //It stores the inputAction mapping context
    [SerializeField] InputActionAsset inputActionMapping;
    //Here we save a reference with all the different types of InputActions in the mapping context
    InputAction right, left, rotation_left, rotation_right, down, blockPiece, savePiece;

    //The different sound effects that we can hear
    [SerializeField]
    AudioClip difficultyChangeSFX, lastDifficultyChangeSFX, lockSFX, hardDropSFX, rotateSFX, downSFX, moveSFX;

    [SerializeField]
    GameObject particleSystemPrefab;

    //Reference of the board    
    public Board board { get; private set; }
    
    public PiecesData data { get; private set; }
    public Vector3Int[] cells { get; private set; }
    public Vector3Int position { get; private set; }
    public int rotationIndex { get; private set; }
    //1s every time the piece is falling
    public float stepDelay;
    //0.5s that you have to move the piece in the ground before lock
    public float lockDelay;
    

    private float stepTime;
    private float lockTime;

    //It's a little delay where we go down
    private float downRepeatDelay = 0.05f;
    //We start with 0f but if we press down and we hold it we can go down, if not we have the piece in gravity.
    private float nextDownTime = 0f;
    //Delay to move horizontal if we hold the A or D, arrow left or right or the left joystick
    private float horizontalRepeatDelay = 0.05f;
    //The Initial Delay the pieces have horizontally
    private float horizontalInitialDelay = 0.25f;

    private float nextHorizontalTime = 0f;
    //We save if the direction is left or right
    private int horizontalDirection = 0;
    //A boolean to knwo if the horizontal input actions are hold
    private bool wasHoldingHorizontal = false;
    //The amount of possible movements, it sets the limit with 15 and lockDelayMoves counts the amount of moves that has.
    private int lockDelayMoveLimit = 15;
    private int lockDelayMoves = 0;

    //We save a reference of every Input Action in the Action Map
    private void Awake(){
        inputActionMapping.Enable();
        right = inputActionMapping.FindActionMap("Controls").FindAction("Right");
        left = inputActionMapping.FindActionMap("Controls").FindAction("Left");
        rotation_left = inputActionMapping.FindActionMap("Controls").FindAction("Left Rotation");
        rotation_right = inputActionMapping.FindActionMap("Controls").FindAction("Right Rotation");
        down = inputActionMapping.FindActionMap("Controls").FindAction("Down");
        blockPiece = inputActionMapping.FindActionMap("Controls").FindAction("Hard Drop");
        savePiece = inputActionMapping.FindActionMap("Controls").FindAction("Hold");
        this.stepDelay = MenuManager.instance.stepDelay;
    }
    /**
     * Params. Board board: a reference to get the board, the position and the data, which contains how is the piece behaviour
     */
    public void Initialize(Board board, Vector3Int position, PiecesData data) { 
        this.board = board;
        this.position = position;
        this.data = data;

        this.rotationIndex = 0;
        this.stepTime = Time.time + this.stepDelay;
        this.lockTime = 0f;
        this.lockDelayMoves = 0;

        if (this.cells == null) { 
            this.cells = new Vector3Int[data.cells.Length];
        }
        for (int i = 0; i < this.cells.Length; i++) {
            this.cells[i] = (Vector3Int)data.cells[i];
        }
    }
    /*CONTROLS*/
    private void Update(){
        //Clean the board
        this.board.Clear(this);

        this.lockTime += Time.deltaTime;

        bool leftHeld = left.IsPressed();
        bool rightHeld = right.IsPressed();

        if (leftHeld && !rightHeld) {
            HorizontalHold(-1);
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
         
        this.board.Set(this);
    }

    private void HorizontalHold(int direction){
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

    public void AugmentDifficulty()
    {
        if (this.board.level < 10)
        {
            if (stepDelay > 0.2)
            {
                stepDelay -= 0.1f;
            }
            else
            {
                stepDelay -= 0.009f;
            }
        }
        if (this.board.level %2 != 0 && this.board.level <= 10)
        {
            BGMusicController.instance.GetComponent<AudioSource>().pitch += 0.1f;
        }
        this.board.level++;
        if (this.board.level <= 10)
        {
            SoundMixerManager.instance.SetMusicVolume(-80);
            SoundMixerManager.instance.SetSoundFXVolume(-80);
            if (this.board.level < 10)
            {
                SoundFXManager.instance.PlaySoundFXClip(difficultyChangeSFX, transform, 1f, true);
            }
            else if (this.board.level == 10)
            {
                SoundFXManager.instance.PlaySoundFXClip(lastDifficultyChangeSFX, transform, 1f, true);
            }

            Invoke("BGMaxVolume", 1.2f);
        }
    }

    private void BGMaxVolume()
    {
        SoundMixerManager.instance.SetMusicVolume(0f);
        SoundMixerManager.instance.SetSoundFXVolume(0f);
    }

    private void SavePiece()
    {
        this.board.savePiece();
    }

    private void Step()
    {
        this.stepTime = Time.time + this.stepDelay;

        bool moved = Move(Vector2Int.down);

        if(!moved && this.lockTime >= this.lockDelay)
        {
            Lock();
        }
    }

    private void Lock() {
        this.board.Set(this);
        this.board.score += 10 * Board.instance.multiplier;

        SpawnStar();

        this.board.clearLines();
        this.board.SpawnPiece();

        SoundFXManager.instance.PlaySoundFXClip(lockSFX, transform, 1f, false);
    }

    private void SpawnStar()
    {
        int minY = int.MaxValue;
        List<Vector3Int> bottomCells = new List<Vector3Int>();

        foreach (var cell in cells)
        {
            if (cell.y < minY)
            {
                minY = cell.y;
            }
        }

        foreach (var cell in cells)
        {
            if (cell.y == minY)
            {
                Vector3Int localPos = cell + position;
                Vector3 worldPos = new Vector3(localPos.x + 0.5f, localPos.y, 0);
                Instantiate(particleSystemPrefab, worldPos, Quaternion.identity);
                particleSystemPrefab.gameObject.SetActive(true);
            }
        }
    }

    /*To block the piece*/
    private void HardDrop() {
        while (Move(Vector2Int.down)) {
            continue;
        }

        SoundFXManager.instance.PlaySoundFXClip(hardDropSFX, transform, 1f, false);

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
            if (translation.y >= 0 && lockDelayMoves < lockDelayMoveLimit)
            {
                this.lockTime = 0f;
                lockDelayMoves++;
            }
            if (translation.y < 0)
            {
                SoundFXManager.instance.PlaySoundFXClip(downSFX, transform, 1f, false);
            }
            else if (translation.x > 0 || translation.x < 0)
            {
                SoundFXManager.instance.PlaySoundFXClip(moveSFX, transform, 1f, false);
            }
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
        else
        {
            if (lockDelayMoves < lockDelayMoveLimit)
            {
                this.lockTime = 0f;
                lockDelayMoves++;
            }
        }

        SoundFXManager.instance.PlaySoundFXClip(rotateSFX, transform, 1f, false);
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

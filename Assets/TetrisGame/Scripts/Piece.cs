using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using static GeometriaDelCaos;

public class Piece : MonoBehaviour
{

    [SerializeField] InputActionAsset inputActionMapping;
    InputAction right, left, down, rotation_left, rotation_right, blockPiece;
    public Board board { get; private set; }
    public PiecesData data { get; private set; }
    public Vector3Int[] cells { get; private set; }
    public Vector3Int position { get; private set; }
    public int rotationIndex { get; private set; }


    public void Awake(){
        inputActionMapping.Enable();
        right = inputActionMapping.FindActionMap("Controls").FindAction("Right");
        left = inputActionMapping.FindActionMap("Controls").FindAction("Left");
        down = inputActionMapping.FindActionMap("Controls").FindAction("Down");
        rotation_left = inputActionMapping.FindActionMap("Controls").FindAction("Rotation_Left");
        rotation_right = inputActionMapping.FindActionMap("Controls").FindAction("Rotation_Right");
        blockPiece = inputActionMapping.FindActionMap("Controls").FindAction("BlockPiece");
    }
    public void Initialize(Board board, Vector3Int position, PiecesData data) { 
        this.board = board;
        this.position = position;
        this.data = data;
        this.rotationIndex = 0;
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
        if (right.triggered) {
            Move(Vector2Int.right);
        } else if (left.triggered) {
            Move(Vector2Int.left);
        } else if (down.triggered) { 
            Move(Vector2Int.down);    
        }

        if (rotation_left.triggered) {
            Rotate(-1);
        } else if (rotation_right.triggered) { 
            Rotate(1); 
        }

        if (blockPiece.triggered) {
            HardDrop();
        }
        this.board.Set(this);

    }
    /*To block the piece*/
    private void HardDrop() {
        while (Move(Vector2Int.down)) {
            continue;
        }
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
        }
        return valid;
    }

    private IEnumerator MoveDelay(Vector3Int newPosition)
    {
        yield return 1f;
        position = newPosition;
        
    }

    private void Rotate(int direction) {
        this.rotationIndex = Wrap(this.rotationIndex + direction, 0, 4);
        for (int i = 0; i < this.cells.Length; i++){
            Vector3 cell = this.cells[i];

            int x, y;

            switch (this.data.piece) {
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

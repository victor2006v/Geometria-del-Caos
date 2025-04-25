using UnityEngine;
using UnityEngine.InputSystem;
using static GeometriaDelCaos;

public class Piece : MonoBehaviour
{

    [SerializeField] InputActionAsset inputActionMapping;
    InputAction horizontal, down, rotation, blockPiece;
    public Board board { get; private set; }
    public PiecesData data { get; private set; }
    public Vector3Int[] cells { get; private set; }
    public Vector3Int position { get; private set; }
    public int rotationIndex { get; private set; }


    public void Awake(){
        horizontal = inputActionMapping.FindActionMap("Controls").FindAction("Horizontal");
        down = inputActionMapping.FindActionMap("Controls").FindAction("Down");
        rotation = inputActionMapping.FindActionMap("Controls").FindAction("Rotation");
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
        this.board.Clear(this);
        float dir = horizontal.ReadValue<float>();
        /*MOVEMENT*/
        if (dir == -1) {
            Move(Vector2Int.left);
        }
        else if (dir == 1){
            Move(Vector2Int.right);
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
            position = newPosition;

        }
        return valid;
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

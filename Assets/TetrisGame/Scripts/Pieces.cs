 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GeometriaDelCaos : MonoBehaviour
{
    /**
     * The pieces are called Tetrominos, the pieces are related to letters, it depends on the form
     */
    public enum Pieces { 
        I,
        O,
        T,
        J,
        L,
        S,
        Z,
    }
    [System.Serializable]
    /** We used a struct to agroup items like the Pieces, cells and wallKicks because it's more effective
     * than a classic class and also it has less memory size.
     */
    public struct PiecesData {
        public Pieces piece; //
        public Tile tile; // We store the different types of pieces, basically the colors
        public Vector2Int[] cells { get; private set; } //It contains a Vector2D with the cells x and y, it's public with get and you can only modify the script here
        public Vector2Int[,] wallKicks { get; private set; } //It contains a Vector2D with the wallKicks, it's public with get and you can only modify the script here

        //It stores the cells and the wallKicks in cells variable and wallKicks from Data 
        public void Initialize() {
            this.cells = Data.Cells[this.piece];
            this.wallKicks = Data.WallKicks[this.piece];
        }
    }
}

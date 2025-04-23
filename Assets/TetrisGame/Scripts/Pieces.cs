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
    public struct PiecesData {
        public Pieces piece;
        public Tile tile;
        public Vector2Int[] cells { get; private set; }

        public void Initialize() {
            this.cells = Data.Cells[this.piece];
        }
    }
}

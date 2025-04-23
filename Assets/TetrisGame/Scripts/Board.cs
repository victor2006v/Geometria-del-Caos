using UnityEngine;
using UnityEngine.Tilemaps;
using static GeometriaDelCaos;

public class Board : MonoBehaviour

{
    public Tilemap tilemap { get; private set; }
    public PiecesData[] pieces;

    private void Awake()
    {
        this.tilemap = GetComponent<Tilemap>();
        for(int i = 0; i < pieces.Length; i++)
        {
            this.pieces[i].Initialize();
        }
    }
    private void Start()
    {
        SpawnPiece();    
    }
    private void SpawnPiece() { 
        int random = Random.Range(0, this.pieces.Length);
        PiecesData data = this.pieces[random];
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PiecesMovement : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;
    [SerializeField] float speed;
    private void Start() {
        rb = GetComponent<Rigidbody2D>();
    }
    // Update is called once per frame
    private void Update(){
        rb.velocity = new Vector2(rb.velocity.x, -speed);
    }
}

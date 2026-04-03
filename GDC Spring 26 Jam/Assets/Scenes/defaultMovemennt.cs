using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;

public class defaultMovemennt : MonoBehaviour
{
    public Rigidbody2D rb;
    public float speed;
    public Vector2 moveDirection; 
    public InputActionReference move;
    public bool isGrounded;
    public float jumpForce;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //Need to move left and right
        //Need to jump up
        //Need gravity
        moveDirection = move.action.ReadValue<Vector2>();



        /*float inputX = Input.GetAxis("Horizontal");
        rb.linearVelocity = new Vector2 (inputX * speed, rb.linearVelocity.y);
        if(Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.linearVelocity = new Vector2 (rb.linearVelocity.x, jumpForce);
        }*/
    }
    private void FixedUpdate()
    {
        rb.linearVelocity = new Vector2 (moveDirection.x * speed, !isGrounded? rb.linearVelocity.y: ! (moveDirection.y > 0)? rb.linearVelocity.y : moveDirection.y * jumpForce );
    }
    void OnCollisionEnter2D(Collision2D collission)
    {
       isGrounded = true; 
    }
    void OnCollisionExit2D(Collision2D collission)
    {
        isGrounded = false;
        Debug.Log("Off Ground");
    }
}

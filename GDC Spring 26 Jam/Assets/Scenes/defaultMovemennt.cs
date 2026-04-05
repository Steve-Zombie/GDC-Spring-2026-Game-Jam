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
    bool facingLeft = false;
    [SerializeField] private Animator animator;
    [SerializeField] private LayerMask wallLayer; // assign wall layer in inspector

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.freezeRotation = true;
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
        if(moveDirection.x < 0 && facingLeft)
        {
            flip();
        }
        else if(moveDirection.x > 0 && !facingLeft)
        {
            flip();
        }

        animate();

        // checks for wall 
        bool isTouchingWall = Physics2D.Raycast(transform.position, Vector2.right, 0.6f, wallLayer) || 
                              Physics2D.Raycast(transform.position, Vector2.left, 0.6f, wallLayer);

        // kills upward momentum  if touching a wall while in the air
        if (isTouchingWall && !isGrounded && rb.linearVelocity.y > 0)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0);
        }

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

    void animate()
    {
        if (moveDirection.x != 0)
        {
            animator.SetBool("isRunning", true);
        }
        else
        {
            animator.SetBool("isRunning", false);
        }
        if (isGrounded)
        {
           animator.SetBool("isGrounded", true); 
        }
        else
        {
            animator.SetBool("isGrounded", false);
        }   
    }

    private void flip()
    {
        facingLeft = !facingLeft;
        transform.Rotate(0f, 180f, 0f);
    }
}
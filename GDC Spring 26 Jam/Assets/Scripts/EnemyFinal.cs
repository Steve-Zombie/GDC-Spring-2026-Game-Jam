using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyFinal : MonoBehaviour
{
    public Transform player;
    public float chaseSpeed = 2f;
    public float jumpForce = 2f;
    public LayerMask groundLayer;
    public float detectionRange = 10f;

    private Rigidbody2D rb;
    private bool isGrounded;
    private bool shouldJump;
    private bool facingRight = true;
    [SerializeField] private Animator animator;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        isGrounded = Physics2D.Raycast(transform.position, Vector2.down, 1.5f, groundLayer);

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);
        bool playerInRange = distanceToPlayer <= detectionRange;

        if (!playerInRange)
        {
            rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
            animate(0);
            return;
        }

        float direction = Mathf.Sign(player.position.x - transform.position.x);
        bool isPlayerAbove = Physics2D.Raycast(transform.position, Vector2.up, 6f, 1 << player.gameObject.layer);

        // always allows for the player to be chased regardless of if grounded or not
        rb.linearVelocity = new Vector2(direction * chaseSpeed, rb.linearVelocity.y);

        // flip to face player
        if (direction < 0 && facingRight)
        {
            flip();
        }
        else if (direction > 0 && !facingRight)
        {
            flip();
        }

        animate(direction);

        // jump logic only works if grounded
        if (isGrounded)
        {
            RaycastHit2D groundInFront = Physics2D.Raycast(transform.position, new Vector2(direction, 0), 2f, groundLayer);
            RaycastHit2D gapAhead = Physics2D.Raycast(transform.position + new Vector3(direction, 0, 0), Vector2.down, 2f, groundLayer);
            RaycastHit2D platformAbove = Physics2D.Raycast(transform.position, Vector2.up, 6f, groundLayer); // ← increased range

            // the enemy will jump if there's a wall in front of it, a gap ahead of it, or if the player is above and there's a platform above it. This allows the enemy to navigate the environment more effectively while still being able to chase the player.
            if (groundInFront.collider)
            {
                shouldJump = true;
            }
            // if there's a gap ahead and the player is not above, jump to try to get over the gap. 
            else if (!isPlayerAbove && gapAhead.collider == false)
            {
                shouldJump = true;
            }
            else if (!gapAhead.collider)
            {
                shouldJump = true;
            }
            // if there's a platform above and the player is above, the enemy will try to jump to 
            // get to the player
            else if (isPlayerAbove && platformAbove.collider)
            {
                shouldJump = true;
            }
        }
        Debug.DrawRay(transform.position, Vector2.down * 1.5f, Color.green);

    }

    private void FixedUpdate()
    {
        if (isGrounded && shouldJump)
        {
            shouldJump = false;
            Vector2 direction = (player.position - transform.position).normalized;
            Vector2 jumpDirection = direction * jumpForce;
            rb.AddForce(new Vector2(jumpDirection.x, jumpForce), ForceMode2D.Impulse);
        }
    }

    void animate(float direction)
    {
        if (direction != 0)
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
        facingRight = !facingRight;
        transform.Rotate(0f, 180f, 0f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            SceneManager.LoadScene("YouLoose");
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }
}
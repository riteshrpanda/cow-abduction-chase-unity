using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 12f;
    private Rigidbody2D rb;
    private bool isGrounded;
    private int jumpCount;
    public int maxJumps = 2;
    private bool isRolling;
    private Animator animator;

    public Transform groundCheck; // Empty GameObject placed at player's feet
    public float groundCheckRadius = 0.2f; // Small radius to check ground
    public LayerMask groundLayer; // Layer assigned to ground tiles

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        jumpCount = maxJumps;
    }

    void Update()
    {
        Move();
        Jump();
        Roll();
        CheckGrounded();
    }

    void Move()
    {
        float moveInput = Input.GetAxis("Horizontal");
        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);

        if (moveInput != 0)
        {
            transform.localScale = new Vector3(Mathf.Sign(moveInput), 1, 1);
            animator.SetBool("isRunning", true);
        }
        else
        {
            animator.SetBool("isRunning", false);
        }
    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && jumpCount > 0)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            jumpCount--;
            animator.SetTrigger("jump");
        }
    }

    void Roll()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && isGrounded && !isRolling)
        {
            isRolling = true;
            animator.SetTrigger("roll");
            moveSpeed *= 1.5f; // Increase speed while rolling
            Invoke("EndRoll", 0.5f); // End roll after 0.5 seconds
        }
    }

    void EndRoll()
    {
        isRolling = false;
        moveSpeed /= 1.5f; // Reset speed after rolling
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            jumpCount = maxJumps; // Reset jump count on ground
            Debug.Log("Landed on Ground!");
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Obstacle"))
        {
            Debug.Log("Player Hit an Obstacle!");
            // Implement damage, game over, or knockback
        }
    }

    void CheckGrounded()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        if (isGrounded)
        {
            jumpCount = maxJumps;
        }
    }
}

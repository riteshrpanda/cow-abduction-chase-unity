using UnityEngine;
using UnityEngine.UI; // Required for UI components
using UnityEngine.SceneManagement; // Required for scene reloading

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
    public int maxHealth = 3;
    private int currentHealth;
    public float knockbackForceX = 5f; 
    public float knockbackForceY = 3f; 
    public float knockbackDuration = 0.3f; 
    private bool isKnockedBack = false;
    public GameObject gameOverCanvas; 
    public Button restartButton; 

    public Transform groundCheck; 
    public float groundCheckRadius = 0.1f; 
    public LayerMask groundLayer; 

    void Start()
    {
        currentHealth = maxHealth;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        jumpCount = maxJumps;

        if (gameOverCanvas != null)
            gameOverCanvas.SetActive(false); // Hide Game Over UI initially
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
    if (isKnockedBack) return; // Stop movement while knocked back

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
        Debug.Log("Jumping! Jump Count Before: " + jumpCount);
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        jumpCount--; // Always decrease jumpCount on jump
        animator.SetTrigger("jump");
        Debug.Log("Jump Count After: " + jumpCount);
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

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Obstacle"))
        {
            TakeDamage();
        }
    }

    public void TakeDamage()
{
    currentHealth--;
    Debug.Log("Player Hit! Health: " + currentHealth);
    
    Vector2 knockbackDirection = new Vector2(-Mathf.Sign(transform.localScale.x) * knockbackForceX, knockbackForceY);
    rb.linearVelocity = Vector2.zero;  
    rb.AddForce(knockbackDirection, ForceMode2D.Impulse);

    isKnockedBack = true;
    Invoke(nameof(ResetKnockback), knockbackDuration);

    if (currentHealth <= 0)
    {
        GameOver();
    }
}

    void ResetKnockback()
    {
        isKnockedBack = false;
    }

    void CheckGrounded()
{
    bool wasGrounded = isGrounded;  
    isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

    if (isGrounded && !wasGrounded) 
    {
        jumpCount = maxJumps;  // Reset jump count when touching ground
        Debug.Log("Landed! Jump count reset.");
    }
}

    public void GameOver()
    {
        Debug.Log("Game Over! Player Died");

        if (gameOverCanvas != null)
            gameOverCanvas.SetActive(true); // Show Game Over UI

        if (restartButton != null)
        {
            restartButton.onClick.RemoveAllListeners(); // Ensure no duplicate listeners
            restartButton.onClick.AddListener(RestartGame); // Link restart button
        }

        Time.timeScale = 0; // Pause game
    }

    void RestartGame()
    {
        Time.timeScale = 1; // Unpause game
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Restart level
    }
}

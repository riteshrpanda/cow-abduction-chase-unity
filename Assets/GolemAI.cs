using UnityEngine;
using UnityEngine.UI;

public class GolemAI : MonoBehaviour
{
    public Transform player;
    public float speed = 2f;
    public float attackRange = 1.5f;
    private Animator animator;
    private bool isAttacking = false;
    private Rigidbody2D rb;

    public GameObject gameOverCanvas; // Assign in Inspector
    public Button restartButton; // Assign in Inspector
    public GameOverManager gameOverManager; // Assign in Inspector

    public float gameOverDelay = 2f; // Delay before showing Game Over screen

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

        if (gameOverCanvas != null)
            gameOverCanvas.SetActive(false); // Hide Game Over screen initially
    }

    void Update()
{
    if (isAttacking) return;

    float distance = Vector2.Distance(transform.position, player.position);

    if (distance > attackRange)
    {
        transform.position = Vector2.MoveTowards(transform.position, new Vector2(player.position.x, transform.position.y), speed * Time.deltaTime);
        
        animator.SetBool("isWalking", true);  // Enable walking animation
    }
    else
    {
        animator.SetBool("isWalking", false); // Stop walking when close
    }
}


    void OnCollisionEnter2D(Collision2D collision)
{
    if (collision.collider.CompareTag("Player"))
    {
        isAttacking = true;
        rb.linearVelocity = Vector2.zero;

        animator.SetBool("isWalking", false);  // Stop walking animation
        animator.SetTrigger("Attack");         // Play attack animation

        Invoke("GameOver", gameOverDelay);
    }
}

    void GameOver()
    {
        Debug.Log("Game Over! Player Died");

        if (gameOverCanvas != null)
            gameOverCanvas.SetActive(true); // Show Game Over UI

        if (restartButton != null)
            restartButton.onClick.AddListener(RestartGame); // Link restart button

        Time.timeScale = 0; // Pause game
    }

    void RestartGame()
    {
        Time.timeScale = 1; // Unpause game
        
        if (gameOverManager != null)
            gameOverManager.RestartGame(); // Call GameOverManager's RestartGame()
    }
}
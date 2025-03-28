using UnityEngine;
using UnityEngine.UI;

public class GolemAI : MonoBehaviour
{
    public Transform player;
    public float speed = 5f;
    public float attackRange = 1.5f;
    private Animator animator;
    private bool isAttacking = false;
    private Rigidbody2D rb;

    public GameObject gameOverCanvas;
    public Button restartButton;
    public GameOverManager gameOverManager;

    public float gameOverDelay = 2f;
    public float speedIncreaseRate = 0.02f;

    private Animator playerAnimator; // Added player animator reference

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

        if (gameOverCanvas != null)
            gameOverCanvas.SetActive(false);

        if (player != null)
            playerAnimator = player.GetComponent<Animator>(); // Get player's Animator
    }

    void Update()
    {
        if (isAttacking) return;
        speed += speedIncreaseRate * Time.deltaTime;

        float distance = Vector2.Distance(transform.position, player.position);

        if (distance > attackRange)
        {
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(player.position.x, transform.position.y), speed * Time.deltaTime);
            animator.SetBool("isWalking", true);
        }
        else
        {
            animator.SetBool("isWalking", false);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            isAttacking = true;
            rb.linearVelocity = Vector2.zero;

            animator.SetBool("isWalking", false);
            animator.SetTrigger("Attack");

            if (playerAnimator != null)
                playerAnimator.SetTrigger("death"); // Trigger player death animation

            Invoke("GameOver", gameOverDelay);
        }
    }

    void GameOver()
    {
        Debug.Log("Game Over! Player Died");

        if (gameOverCanvas != null)
            gameOverCanvas.SetActive(true);

        if (restartButton != null)
            restartButton.onClick.AddListener(RestartGame);

        Time.timeScale = 0;
    }

    void RestartGame()
    {
        Time.timeScale = 1;

        if (gameOverManager != null)
            gameOverManager.RestartGame();
    }
}

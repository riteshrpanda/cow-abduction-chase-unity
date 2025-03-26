using UnityEngine;

public class BringerOfDeathController : MonoBehaviour
{
    private Animator animator;
    public GameObject gameOverCanvas; // Assign in Inspector
    public GameObject restartButton; // Assign in Inspector
    public float speed = 2f; // Movement speed
    private bool hasAttacked = false;

    void Start()
    {
        animator = GetComponent<Animator>();
        gameOverCanvas.SetActive(false);  
        restartButton.SetActive(false);
    }

    void Update()
    {
        if (!hasAttacked)
        {
            transform.position += Vector3.right * speed * Time.deltaTime; // Move right constantly
            animator.SetBool("isWalking", true);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
{
    if (!hasAttacked && collision.CompareTag("Player"))
    {
        float distanceToPlayer = Vector2.Distance(transform.position, collision.transform.position);
        if (distanceToPlayer < 1f) // Reduce from a larger range
        {
            hasAttacked = true;
            animator.SetTrigger("Attack");
            Invoke(nameof(GameOver), 0.5f);
        }
    }
}


    void GameOver()
    {
        Time.timeScale = 0f; 
        gameOverCanvas.SetActive(true); 
        restartButton.SetActive(true);
    }
}

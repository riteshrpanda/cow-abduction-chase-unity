using UnityEngine;

public class SpearTrap : MonoBehaviour
{
    private Animator animator;
    private bool hasTriggered = false;
    private bool isDangerous = false;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !hasTriggered)
        {
            hasTriggered = true;
            animator.Play("SpearGrow");
            Invoke(nameof(ActivateKnockback), 0.5f); // Wait until spear is fully extended
        }
    }

    void ActivateKnockback()
    {
        isDangerous = true;
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player") && isDangerous)
        {
            PlayerController player = other.GetComponent<PlayerController>();
            if (player != null)
            {
                Debug.Log("Spear knocks back player!");
                player.ApplyKnockback();
                isDangerous = false; // Prevent repeated knockback
            }
        }
    }
}

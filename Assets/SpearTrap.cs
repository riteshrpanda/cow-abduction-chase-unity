using UnityEngine;

public class SpearTrap : MonoBehaviour
{
    private Animator animator;
    private bool hasTriggered = false;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !hasTriggered)
        {
            hasTriggered = true;
            animator.Play("SpearGrow"); // Play spear animation
        }
    }
}

using UnityEngine;

public class SpikeTrap : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Spike hit the player! Applying knockback.");
            PlayerController player = collision.GetComponent<PlayerController>();
            if (player != null)
            {
                player.ApplyKnockback();
            }
        }
    }
}

using UnityEngine;

public class AerialSpike : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Aerial Spike hit the player!");
            PlayerController player = collision.GetComponent<PlayerController>();
            if (player != null)
            {
                Debug.Log("Applying knockback to player.");
                player.ApplyKnockback();
            }
        }
    }
}

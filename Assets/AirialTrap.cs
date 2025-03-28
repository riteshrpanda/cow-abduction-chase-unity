using UnityEngine;

public class AerialSpike : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
{
    if (collision.CompareTag("Player"))
    {
        Debug.Log("Spike hit the player!");
        PlayerController player = collision.GetComponent<PlayerController>();
        if (player != null)
        {
            Debug.Log("Calling TakeDamage() on player.");
            player.TakeDamage();
        }
    }
}

}

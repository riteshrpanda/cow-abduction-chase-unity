using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class PlayerStats : MonoBehaviour
{
    [SerializeField] private int health = 3;
    [SerializeField] private Text heartCountText;

    void Start()
    {
        UpdateHeartCountText();
    }

    public void TakeDamage(int damage)
    {
        health = Mathf.Max(0, health - damage);
        UpdateHeartCountText();
    }

    private void UpdateHeartCountText()
    {
        if (heartCountText != null)
        {
            heartCountText.text = $"Hearts: {health}";
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Obstacle")) 
        {
            TakeDamage(1);
        }
    }
}

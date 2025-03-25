using UnityEngine;
using UnityEngine.SceneManagement;

public class LaserCollision : MonoBehaviour
{
    public GameObject gameOverCanvas; // Assign in Inspector
    public float speed = 5f; // Speed of the laser

    void Start()
    {
        gameOverCanvas.SetActive(false);
    }

    void Update()
    {
        transform.position += Vector3.right * speed * Time.deltaTime; // Keep the laser moving
    }

    void OnTriggerEnter2D(Collider2D other)
{
    Debug.Log("Laser hit something: " + other.name); // Log what it hits

    if (other.CompareTag("Player")) 
    {
        Debug.Log("Laser hit the player! Calling GameOver.");
        GameOver();
    }
}

    void GameOver()
    {
        gameOverCanvas.SetActive(true);
        Time.timeScale = 0f; // Pause the game
    }

    public void RestartGame()
    {
        Time.timeScale = 1f; // Resume game
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // Reload scene
    }
}

using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    public GameObject gameOverCanvas; // Assign this in the Inspector

    void Start()
    {
        if (gameOverCanvas == null)
        {
            Debug.LogError("GameOverCanvas is not assigned in the Inspector!");
        }
        else
        {
            gameOverCanvas.SetActive(false); // Ensure it starts hidden
        }
    }

    public void ShowGameOverScreen()
    {
        if (gameOverCanvas != null)
        {
            gameOverCanvas.SetActive(true);
            Time.timeScale = 0f; // Pause game on game over
            Debug.Log("Game Over screen displayed.");
        }
        else
        {
            Debug.LogError("GameOverCanvas reference is missing!");
        }
    }

    public void RestartGame()
    {
        Debug.Log("Restarting Game...");
        Time.timeScale = 1f; // Ensure time resumes
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}

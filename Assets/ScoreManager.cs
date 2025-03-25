using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText; // Assign in Inspector
    public GameObject gameOverCanvas; // Assign in Inspector
    public GameObject restartButton; // Assign in Inspector

    private int score = 0;
    private bool isGameOver = false;

    void Start()
    {
        scoreText.gameObject.SetActive(true); // Ensure score is always visible
        gameOverCanvas.SetActive(false); // Ensure Game Over UI is hidden at start
    }

    void Update()
    {
        if (!isGameOver)
        {
            score += Mathf.FloorToInt(Time.deltaTime * 100000); // Increase score over time
            UpdateScoreText();
        }
    }

    void UpdateScoreText()
{
    if (scoreText != null) // Prevents error if text is missing
    {
        scoreText.text = "Score: " + score;
    }
    else
    {
        Debug.LogError("ScoreText is missing or destroyed!");
    }
}

    public void StopScoring()
    {
        isGameOver = true;
    }

    public void ShowGameOverScreen()
    {
        isGameOver = true; // Stop score updating
        gameOverCanvas.SetActive(true); // Show game over UI
        restartButton.SetActive(true); // Ensure restart button is visible
    }

    public void RestartGame()
    {
        Time.timeScale = 1f; // Resume game speed
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // Reload scene
    }
}

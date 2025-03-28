using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText; // Assign in Inspector
    private int score = 0;
    private bool isGameOver = false;
    private float scoreFloat = 0f;

    void Start()
    {
        if (scoreText == null)
        {
            Debug.LogError("⚠️ ScoreText is NOT assigned! Assign it in the Inspector.");
        }
    }

    void Update()
{
    if (!isGameOver)
    {
        scoreFloat += Time.deltaTime * (10 + (score / 500f)); 
        score = Mathf.FloorToInt(scoreFloat); 
        UpdateScoreText();
    }
}

    void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score;
        }
    }

    public void StopScoring()
    {
        isGameOver = true;
    }
}

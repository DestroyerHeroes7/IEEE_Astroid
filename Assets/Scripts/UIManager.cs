using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    public LevelManager levelManager;
    public Text playerScoreText;

    public Text highScoreText;

    public GameObject gameOverPanel;
    private void Awake()
    {
        Instance = this;
    }
    public void OnDestroyAsteroid()
    {
        playerScoreText.text = "Score : " + levelManager.playerScore.ToString();
    }
    public void OnPlayerDestroyed()
    {
        highScoreText.text =  "High Score : " + PlayerPrefs.GetInt("high score").ToString();
        gameOverPanel.SetActive(true);
    }
    public void OnRetryButtonClick()
    {
        levelManager.RestartGame();
    }
}

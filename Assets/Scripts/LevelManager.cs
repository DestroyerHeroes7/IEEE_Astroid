using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;
    public int playerScore;
    private void Awake()
    {
        Instance = this;
    }
    public void OnDestroyAsteroid(int score)
    {
        playerScore += score;
    }
    public void OnPlayerDestroyed()
    {
        if (playerScore > PlayerPrefs.GetInt("high score", 0))
            PlayerPrefs.SetInt("high score", playerScore);
    }
    public void RestartGame()
    {
        SceneManager.LoadScene(0);
    }
}

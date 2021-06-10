using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSession : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] int score = 0;
    //[SerializeField] TMPro.TextMeshProUGUI scoreText;

    private void Awake()
    {
        SetUpSingleton();
    }

    private void SetUpSingleton()
    {
        int numberGameSessions = FindObjectsOfType<GameSession>().Length;
        if(numberGameSessions>1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }
    public int GetScore()
    {
        return score;
    }
    public void AddToScore(int scoreValue)
    {
        score += scoreValue;
        //FindObjectOfType<ScoreDisplay>().UpdateScoreDisplay(score);
    }
    public void ResetGame()
    {
        Destroy(gameObject);
    }
}

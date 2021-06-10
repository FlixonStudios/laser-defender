using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreDisplay : MonoBehaviour
{
    TMPro.TextMeshProUGUI scoreText;
    GameSession gameSession;
    
    // Start is called before the first frame update
    void Start()
    {
        scoreText = GetComponent<TextMeshProUGUI>();
        gameSession = FindObjectOfType<GameSession>();
        //scoreText.text = gameSession.GetScore().ToString();
    }

    // Update is called once per frame
    
    public void UpdateScoreDisplay(int getScore)
    {
        scoreText.text = getScore.ToString();
    }
    
    void Update()
    {
        if (gameSession == null)
        {
            gameSession = FindObjectOfType<GameSession>();
        }
        scoreText.text = gameSession.GetScore().ToString();
    }
}

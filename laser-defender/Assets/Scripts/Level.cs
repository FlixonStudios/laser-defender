using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour
{       
    GameObject player;

    public void LoadNextScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex + 1);
        
        //player.LockFire(false);
    }
    public void LoadStartMenu()
    {
        RespawnPlayer();
    }
    public void LoadGameOver()
    {
        StartCoroutine(DelayGameOver());        
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    IEnumerator DelayGameOver()
    {

        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(SceneManager.sceneCountInBuildSettings - 1);
    }

    public void RespawnPlayer()
    {
        Respawner playerRespawner = FindObjectOfType<Respawner>();
        player = playerRespawner.GetPlayerObject();
        player.SetActive(true);
        player.GetComponent<Player>().ResetHealth();
        player.transform.position = playerRespawner.GetRespawnPoint(); 
        SceneManager.LoadScene(0);
        GameSession gameSession = FindObjectOfType<GameSession>();
        gameSession.ResetGame();

    }
}

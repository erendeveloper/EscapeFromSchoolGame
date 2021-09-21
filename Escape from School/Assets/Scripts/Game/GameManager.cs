using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//Added on Main Camera
//Works Game Scene
public class GameManager : MonoBehaviour
{
    //Access other script
    public PlayerController playerControllerScript;
    private GameUIController gameUIControllerScript;

    private int score=0; 

    private void Awake()
    {
        gameUIControllerScript = GetComponent<GameUIController>();
    }

    public void IncreaseScore(int amount)
    {
        score += amount;
        gameUIControllerScript.changeScoreText(score.ToString());
    }
    public void Die()
    {
        gameUIControllerScript.OpenGameOverMenu();
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(1);
    }
    public void OpenHomeScreen()
    {
        SceneManager.LoadScene(0);
    }
    
}

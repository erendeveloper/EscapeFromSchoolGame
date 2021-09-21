using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Added on Main Camera
//UI controller for Game Scene
public class GameUIController : MonoBehaviour
{

    public Canvas gameOverCanvas;

    public Text score;

    public void changeScoreText(string scoreText)
    {
        score.text = scoreText;
    }
    public void OpenGameOverMenu()
    {
        gameOverCanvas.gameObject.SetActive(true);
    }
    
}

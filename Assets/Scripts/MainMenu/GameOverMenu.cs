using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverMenu : MonoBehaviour
{
    public Text scoreText;

    public void QuitGame()
    {
        Application.Quit();
    }


    public void SetScore(int score)
    {
        scoreText.text = score.ToString();
    }
}

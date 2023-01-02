using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LeaderboardMenu : MonoBehaviour
{
   public TextMeshProUGUI highScoreText;

    void Start()
    {
        getHighScore();
    }

    public void getHighScore()
    {
        // get high score
        float highScore = PlayerPrefs.GetFloat("HighScore");
        // round to 2 decimal places
        highScore = Mathf.Round(highScore * 100f) / 100f;

        // set high score text
        if( PlayerPrefs.HasKey("HighScore"))
        {
            highScoreText.text =  highScore.ToString() + " s";
        }
        else
        {
            highScoreText.text = "N/A!";
        }
    }
}

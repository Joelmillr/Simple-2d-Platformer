using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PauseMenu : MonoBehaviour
{
    float startTime;

    public static bool GameIsPaused = false;

    public GameObject pauseMenuUI;

    public TextMeshProUGUI currentTimeText;

    void Start()
    {
        startTime = Time.time;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
        currentTimeText.text = (Time.time - startTime).ToString("0.00") + "s";
    }

    public void setHighScore()
    {
        // Set High Score
        float currentTime = Time.time - startTime;
        if (PlayerPrefs.GetFloat("HighScore") > currentTime  || !PlayerPrefs.HasKey("HighScore"))
        {
            PlayerPrefs.SetFloat("HighScore", currentTime);
        }
    }

    public void LoadMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishLevel : MonoBehaviour
{
    public PauseMenu pauseMenu;

    void OnTriggerEnter2D(Collider2D other)
    {
        Player controller = other.GetComponent<Player>();
        if (controller != null)
        {
            // Save the score
            pauseMenu.setHighScore();
            // Load the main menu
            SceneManager.LoadScene("MainMenu");
        }
    }
}

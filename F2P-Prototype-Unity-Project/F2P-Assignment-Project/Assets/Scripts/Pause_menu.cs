using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause_menu : MonoBehaviour
{
    public GameObject pauseScreen;
    public GameObject gameUI;
    public GameObject gameUIp2;

    public void ResumeGame()
    {
        Time.timeScale = 1;
        FindObjectOfType<Player_Movement_Script>().isPaused = false;
        pauseScreen.SetActive(false);
        gameUI.SetActive(true);
        gameUIp2.SetActive(true);
    }

    public void QuitGame()
    {
        Debug.Log("Quit Game");
        Application.Quit();
    }

    public void ExitLevel()
    {
        Debug.Log("Exited Level");
        SceneManager.LoadScene("ShopScene", LoadSceneMode.Single);
    }
}

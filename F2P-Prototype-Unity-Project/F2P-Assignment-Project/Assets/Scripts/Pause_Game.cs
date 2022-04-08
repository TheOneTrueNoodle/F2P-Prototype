using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause_Game : MonoBehaviour
{
    public Player_Controls pInput;

    public GameObject pauseScreen;
    public GameObject gameUI;
    public GameObject gameUIp2;

    private void Awake()
    {
        pInput = new Player_Controls();
        pInput.Gameplay.PauseGame.performed += _ => pauseGame();
    }

    private void pauseGame()
    {
        Time.timeScale = 0;
        FindObjectOfType<Player_Movement_Script>().isPaused = true;
        pauseScreen.SetActive(true);
        gameUI.SetActive(false);
        gameUIp2.SetActive(false);
    }
    private void OnEnable()
    {
        pInput.Gameplay.Enable();
    }

    private void OnDisable()
    {
        pInput.Gameplay.Disable();
    }
}

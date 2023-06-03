using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    public GameObject pauseMenu;

    private void Start()
    {
        pauseMenu.SetActive(false); // Make sure the pause menu is hidden at start
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    public void TogglePause()
    {
        if (pauseMenu.activeSelf)
        {
            // If the pause menu is active, hide it and resume the game
            pauseMenu.SetActive(false);
        }
        else
        {
            // If the pause menu is not active, show it and pause the game
            pauseMenu.SetActive(true);
        }
    }
}

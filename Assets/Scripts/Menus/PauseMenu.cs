using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public PauseManager pauseManager; // Reference to the PauseManager script

    public void HandleQuitButtonOnClickEvent()
    {
        pauseManager.TogglePause(); // Resume the game
    }

    public void HandleReturnButtonOnClickEvent()
    {
        MenuManager.GoToMenu(MenuName.Main);
    }
}

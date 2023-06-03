using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class MenuManager
{
    public static void GoToMenu(MenuName name, Difficulty difficulty = Difficulty.Easy)
    {
        switch (name)
        {
            case MenuName.Settings:
                Object.Instantiate(Resources.Load("SettingsMenu"));
                break;
            case MenuName.Load:
                Object.Instantiate(Resources.Load("LoadMenu"));
                break;
            case MenuName.Main:
                SceneManager.LoadScene("MainMenu");
                break;
            case MenuName.Pause:
                Object.Instantiate(Resources.Load("PauseMenu"));
                break;
            case MenuName.Difficulty:
                SceneManager.LoadScene("DifficultyMenu");
                break;
            case MenuName.LoadingScreen:
                SceneManager.LoadScene("LoadingScreen");
                break;
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultyMenu : MonoBehaviour
{
    public void HandleEazyButtonOnClickEvent()
    {
        AudioManager.Play(AudioClipName.MenuButtonClick);
        PlayerPrefs.SetInt("SelectedDifficulty", (int)Difficulty.Easy);
        MenuManager.GoToMenu(MenuName.LoadingScreen);
    }

    public void HandleNormalButtonOnClickEvent()
    {
        AudioManager.Play(AudioClipName.MenuButtonClick);
        PlayerPrefs.SetInt("SelectedDifficulty", (int)Difficulty.Normal);
        MenuManager.GoToMenu(MenuName.LoadingScreen);
    }

    public void HandleHardButtonOnClickEvent()
    {
        AudioManager.Play(AudioClipName.MenuButtonClick);
        PlayerPrefs.GetInt("SelectedDifficulty", (int)Difficulty.Hard);
        MenuManager.GoToMenu(MenuName.LoadingScreen);
    }

    public void HandleQuitButtonOnClickEvent()
    {
        AudioManager.Play(AudioClipName.MenuButtonClick);
        MenuManager.GoToMenu(MenuName.Main);
    }
}

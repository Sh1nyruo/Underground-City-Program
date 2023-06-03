using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DifficultyUtils
{
    static Difficulty difficulty;

    // Add this property to make it possible to get the difficulty level
    #region Public methods
    public static Difficulty CurrentDifficulty
    {
        get { return difficulty; }
    }

    public static void SetDifficulty(Difficulty difficultyLevel)
    {
        difficulty = difficultyLevel;
    }

    public static void Initialize()
    {
        EventManager.AddListener(EventName.GameStartedEvent, HandleGameStartedEvent);
    }
    #endregion

    #region Private methods
    static void HandleGameStartedEvent(int intDifficulty)
    {
        difficulty = (Difficulty)intDifficulty;
        SceneManager.LoadScene("Gameplay");
    }
    #endregion
}

using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.Events;

public class LoadingScreenController : MonoBehaviour
{
    void Start()
    {
        Difficulty difficulty = (Difficulty)PlayerPrefs.GetInt("SelectedDifficulty");
        // Debug.Log("LoadingScene for difficulty: " + difficulty);
        StartCoroutine(LoadYourAsyncScene());
    }

    IEnumerator LoadYourAsyncScene()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("Gameplay");

        // While the async scene isn't loaded, show your loading screen
        while (!asyncLoad.isDone)
        {
            // Update your loading UI here
            // Remember that asyncLoad.progress will be between 0 and 0.9
            // If you need it to be from 0 to 1, use Mathf.Clamp01(asyncLoad.progress / 0.9f)

            yield return null;
        }

        // When the scene is loaded, you can hide your loading screen (you may implement this in different ways, depending on your UI setup)
        // ... hide loading screen
    }
}

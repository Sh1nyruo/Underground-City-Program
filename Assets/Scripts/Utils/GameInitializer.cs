using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInitializer : MonoBehaviour
{

    private void Start()
    {
        EventManager.Initialize();
        // DifficultyUtils.Initialize();
        BuildingManager.Initialize();
    }

}

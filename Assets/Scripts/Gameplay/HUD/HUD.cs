using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HUD : MonoBehaviour
{
    #region Fields

    [SerializeField]
    TextMeshProUGUI foodStorageText;
    [SerializeField]
    TextMeshProUGUI foodProductionText;

    [SerializeField]
    TextMeshProUGUI stoneStorageText;
    [SerializeField]
    TextMeshProUGUI stoneProductionText;

    [SerializeField]
    TextMeshProUGUI oreStorageText;
    [SerializeField]
    TextMeshProUGUI oreProductionText;

    [SerializeField]
    TextMeshProUGUI metalStorageText;
    [SerializeField]
    TextMeshProUGUI metalProductionText;


    [SerializeField]
    TextMeshProUGUI toolStorageText;
    [SerializeField]
    TextMeshProUGUI toolProductionText;

    [SerializeField]
    TextMeshProUGUI researchText;

    [SerializeField]
    TextMeshProUGUI populationText;


    #endregion


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        foodStorageText.text = ResourceManager.current.foodStorage.ToString();
        foodProductionText.text = GetProductionText(ResourceManager.current.foodProduction);

        stoneStorageText.text = ResourceManager.current.stoneStorage.ToString();
        stoneProductionText.text = GetProductionText(ResourceManager.current.stoneProduction);

        oreStorageText.text = ResourceManager.current.oreStorage.ToString();
        oreProductionText.text = GetProductionText(ResourceManager.current.oreProduction);

        metalStorageText.text = ResourceManager.current.metalStorage.ToString();
        metalProductionText.text = GetProductionText(ResourceManager.current.metalProduction);

        toolStorageText.text = ResourceManager.current.toolStorage.ToString();
        toolProductionText.text = GetProductionText(ResourceManager.current.toolProduction);

        researchText.text = GetProductionText(ResourceManager.current.research);

        populationText.text = GetProductionText(ResourceManager.current.population);
    }

    #region Private Methods

    private string GetProductionText(int production)
    {
        if (production == 0)
        {
            return "+ 0";
        }
        else if(production > 0)
        {
            return "+" + " " + production.ToString();
        }
        else
        {
            return "-" + " " + (-production).ToString();
        }
    }

    #endregion
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    public bool Placed { get; private set; }
    public BoundsInt area;

    public int foodProductionPerTurn = 0;
    public int stoneProductionPerTurn = 0;
    public int oreProductionPerTurn = 0;
    public int metalProductionPerTurn = 0;
    public int toolProductionPerTurn = 0;

    public int researchProduction = 0;
    public int populationProduction = 0;

    public int foodConsumptionPerTurn = 0;
    public int stoneConsumptionPerTurn = 0;
    public int oreConsumptionPerTurn = 0;
    public int metalConsumptionPerTurn = 0;
    public int toolConsumptionPerTurn = 0;

    public int foodPlacementConsumption = 0;
    public int stonePlacementConsumption = 0;
    public int orePlacementConsumption = 0;
    public int metalPlacementConsumption = 0;
    public int toolPlacementConsumption = 0;

    public int populationConsumption = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    #region Build Methods

    public bool CanBePlaced()
    {
        Vector3Int positionInt = GridBuildingSystem.current.gridLayout.LocalToCell(transform.position);
        BoundsInt areaTemp = area;
        areaTemp.position = positionInt;

        if(GridBuildingSystem.current.CanTakeArea(areaTemp))
        {
            return true;
        }

        return false;
    }

    public void Place()
    {
        Vector3Int positionInt = GridBuildingSystem.current.gridLayout.LocalToCell(transform.position);
        BoundsInt areaTemp = area;
        areaTemp.position = positionInt;
        Placed = true;
        GridBuildingSystem.current.TakeArea(areaTemp);
    }
    #endregion
}

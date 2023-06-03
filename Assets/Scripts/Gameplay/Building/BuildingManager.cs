using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BuildingManager : MonoBehaviour
{
    #region fields
    public static BuildingManager current;

    public static Dictionary<BuildingName, GameObject> buildings = new Dictionary<BuildingName, GameObject>();

    public static Dictionary<BuildingName, List<GameObject>> instantiatedBuildings = new Dictionary<BuildingName, List<GameObject>>();
    #endregion

    #region Unity Methods
    private void Awake()
    {
        current = this;
        
    }


    public static void Initialize()
    {
        string buildingPath = @"Buildings\";

        buildings.Add(BuildingName.empty, null);
        buildings.Add(BuildingName.basePrefab, Resources.Load<GameObject>(buildingPath + "Base"));
        buildings.Add(BuildingName.housePrefab, Resources.Load<GameObject>(buildingPath + "House"));
        buildings.Add(BuildingName.greenhousePrefab, Resources.Load<GameObject>(buildingPath + "Greenhouse"));
        buildings.Add(BuildingName.quarryPrefab, Resources.Load<GameObject>(buildingPath + "Quarry"));
        buildings.Add(BuildingName.minePrefab, Resources.Load<GameObject>(buildingPath + "Mine"));
        buildings.Add(BuildingName.smelterPrefab, Resources.Load<GameObject>(buildingPath + "Smelter"));
        buildings.Add(BuildingName.factoryPrefab, Resources.Load<GameObject>(buildingPath + "Factory"));
        buildings.Add(BuildingName.researchInstitutePrefab, Resources.Load<GameObject>(buildingPath + "ResearchInstitute"));

        foreach (BuildingName buildingName in System.Enum.GetValues(typeof(BuildingName)))
        {
            instantiatedBuildings.Add(buildingName, new List<GameObject>());
        }
    }

    #endregion

    #region Building Management

    /// <summary>
    /// Creates a base at the center of the grid
    /// </summary>
    public void CreateBase()
    {
        // Get the center cell position of the 3x3 area
        Vector3Int centerCellPosition = new Vector3Int(GridBuildingSystem.current.gridWidth / 2, GridBuildingSystem.current.gridHeight / 2, 0);


        // Check if the area can be taken
        BoundsInt baseArea = new BoundsInt(centerCellPosition.x - 1, centerCellPosition.y - 1, 0, 1, 1, 1);

        if (GridBuildingSystem.current.CanTakeArea(baseArea))
        {
            // Convert cell position to world position
            Vector3 centerWorldPosition = GridBuildingSystem.current.gridLayout.CellToWorld(centerCellPosition);

            // Adjust the position to the center of the cell
            Vector3 cellSize = GridBuildingSystem.current.gridLayout.cellSize;
            centerWorldPosition += new Vector3(cellSize.x / 2, cellSize.y / 2, 0);

            // Instantiate the base prefab at the center world position
            GameObject baseObject = Instantiate(buildings[BuildingName.basePrefab], centerWorldPosition, Quaternion.identity);

            // Optionally parent the base object to the BuildingManager
            baseObject.transform.SetParent(transform);

            // Add the base object to the instantiated buildings dictionary
            instantiatedBuildings[BuildingName.basePrefab].Add(baseObject);

            TileDataManager.PlaceBuilding(baseObject, BuildingName.basePrefab,centerCellPosition);

            ResourceManager.current.AddResource(baseObject.GetComponent<Building>());
        }
        else
        {
            Debug.LogError("Can't create base, area is already taken");
        }

    }
    #endregion
}

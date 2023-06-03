using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BuildingPlacementSystem : MonoBehaviour
{
    #region fields

    private GameObject currentBuildingPrefab = null;
    private BuildingName currentBuildingName;
    public static BuildingPlacementSystem current;

    private Vector3Int previousCellPosition = Vector3Int.zero;

    [SerializeField] private GridLayout gridLayout;

    public TextMeshProUGUI warningText;
    #endregion

    #region Unity Methods
    private void Awake()
    {
        current = this;
    }
    void Start()
    {
        warningText.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentBuildingPrefab != null)
        {
            Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3Int cellPosition = gridLayout.LocalToCell(mouseWorldPosition);
            
            if (cellPosition != previousCellPosition)
            {
                currentBuildingPrefab.transform.position = gridLayout.CellToLocalInterpolated(cellPosition + new Vector3(.5f,.5f,0f));

                if (Input.GetMouseButtonDown(0))
                {
                    HandleBuildingPlacement(currentBuildingPrefab.transform.position);
                }
                else if (Input.GetMouseButtonDown(1))
                {
                    HandleCanclePlacement();
                }
            }
        }
    }
    #endregion





    #region Building Placement

    private void StartPlacement(BuildingName buildingName)
    {
        currentBuildingPrefab = Instantiate(BuildingManager.buildings[buildingName]);
        currentBuildingName = buildingName;
    }

    public void StopPlacement()
    {
        Destroy(currentBuildingPrefab);
        currentBuildingPrefab = null;
    }

    #endregion




    #region Building Placement Detail

    public void HandleHouseCardOnClickEvent()
    {
        if (currentBuildingPrefab != null)
        {
            return;
        }
        else
        {
            StartPlacement(BuildingName.housePrefab);
        }
    }

    public void HandleGreenhouseCardOnClickEvent()
    {
        if (currentBuildingPrefab != null)
        {
            return;
        }
        else
        {
            StartPlacement(BuildingName.greenhousePrefab);
        }
    }

    public void HandleQuarryCardOnClickEvent()
    {
        if (currentBuildingPrefab != null)
        {
            return;
        }
        else
        {
            StartPlacement(BuildingName.quarryPrefab);
        }
    }

    public void HandlemineCardOnClickEvent()
    {
        if (currentBuildingPrefab != null)
        {
            return;
        }
        else
        {
            StartPlacement(BuildingName.minePrefab);
        }
    }

    public void HandleSmelterCardOnClickEvent()
    {
        if (currentBuildingPrefab != null)
        {
            return;
        }
        else
        {
            StartPlacement(BuildingName.smelterPrefab);
        }
    }

    public void HandleFactoryCardOnClickEvent()
    {
        if (currentBuildingPrefab != null)
        {
            return;
        }
        else
        {
            StartPlacement(BuildingName.factoryPrefab);
        }
    }

    public void HandleResearchInstituteCardOnClickEvent()
    {
        if (currentBuildingPrefab != null)
        {
            return;
        }
        else
        {
            StartPlacement(BuildingName.researchInstitutePrefab);
        }
    }


    private void HandleBuildingPlacement(Vector3 position)
    {

        currentBuildingPrefab.transform.SetParent(BuildingManager.current.transform);

        currentBuildingPrefab.GetComponent<Building>().area.position = gridLayout.WorldToCell(position);

        if (!TileDataManager.IsTileAvailable(currentBuildingPrefab.GetComponent<Building>().area.position))
        {
            Debug.Log("Tile is occupied");
            return;
        }

        Building building = currentBuildingPrefab.GetComponent<Building>();
        if (!ResourceManager.current.AddResource(building))
        {
            warningText.text = "×ÊÔ´²»×ã";
            warningText.enabled = true;
            Debug.Log("Not enough resource");
            StartCoroutine(HideWarningAfterDelay(3));

            return;
        }

        TileDataManager.PlaceBuilding(currentBuildingPrefab, currentBuildingName, currentBuildingPrefab.GetComponent<Building>().area.position);

        // Debug.Log(currentBuildingPrefab.GetComponent<Building>().area);
        // TileDataManager.TraverseAllTiles(tile => Debug.Log(tile.position.ToString() + " " + tile.buildingName + " " + tile.state));

        currentBuildingPrefab = null;
    }

    private void HandleCanclePlacement()
    {
        Destroy(currentBuildingPrefab);
        currentBuildingPrefab = null;
    }
    #endregion

    IEnumerator HideWarningAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        warningText.enabled = false;
    }

}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TappingManager : MonoBehaviour
{

    [SerializeField] private GameObject tappingPrefab;

    private GameObject temp = null;
    private BoundsInt prevArea;
    private Vector3 prevPos;
    private Vector3Int previousCellPosition = Vector3Int.zero;

    public TextMeshProUGUI warningText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (temp != null)
        {
            Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3Int cellPosition = GridBuildingSystem.current.gridLayout.WorldToCell(mouseWorldPosition);

            if (cellPosition != previousCellPosition)
            {
                temp.transform.position = GridBuildingSystem.current.gridLayout.CellToLocalInterpolated(cellPosition + new Vector3(.5f, .5f, 0f));
                ClearArea();
                FollowTapTile();

                if (Input.GetMouseButtonDown(0))
                {
                    ClearArea();
                    HandleTapping(cellPosition);
                    
                }
                else if (Input.GetMouseButtonDown(1))
                {
                    StopTapping();
                    ClearArea();
                }
            }
        }
    }

    public void FollowTapTile()
    {
        ClearArea();

        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3Int cellPosition = GridBuildingSystem.current.gridLayout.WorldToCell(mouseWorldPosition);

        BoundsInt buildingArea = new BoundsInt(cellPosition.x, cellPosition.y, 0, 1, 1, 1);

        TileBase[] baseArray = GridBuildingSystem.GetTilesBlock(buildingArea, GridBuildingSystem.current.mainTilemap);

        int size = baseArray.Length;
        TileBase[] tileArray = new TileBase[size];

        for (int i = 0; i < baseArray.Length; i++)
        {
            if (baseArray[i] == GridBuildingSystem.tileBases[TileName.Untapped])
            {
                tileArray[i] = GridBuildingSystem.tileBases[TileName.GreenUntapped];
            }
            else
            {
                GridBuildingSystem.FillTiles(tileArray, TileName.RedTapped);
                break;
            }
        }

        GridBuildingSystem.current.tempTilemap.SetTilesBlock(buildingArea, tileArray);
        prevArea = buildingArea;
    }

    public void ClearArea()
    {
        TileBase[] toClear = new TileBase[prevArea.size.x * prevArea.size.y * prevArea.size.z];
        GridBuildingSystem.FillTiles(toClear, TileName.Empty);
        GridBuildingSystem.current.tempTilemap.SetTilesBlock(prevArea, toClear);
    }

    public void HandleTapping(Vector3Int postion)
    {
        Destroy(temp);

        if(!ResourceManager.current.TappingConsumption())
        {
            warningText.text = "×ÊÔ´²»×ã";
            warningText.enabled = true;
            Debug.Log("Not enough resource");
            StartCoroutine(HideWarningAfterDelay(3));

            return;
        }

        BoundsInt BuildingArea = new BoundsInt(postion.x, postion.y, 0, 1, 1, 1);
        TileBase[] baseArray = GridBuildingSystem.GetTilesBlock(BuildingArea, GridBuildingSystem.current.mainTilemap);

        int size = baseArray.Length;
        TileBase[] tileArray = new TileBase[size];

        for (int i = 0; i < baseArray.Length; i++)
        {
            if (baseArray[i] == GridBuildingSystem.tileBases[TileName.Untapped])
            {
                tileArray[i] = GridBuildingSystem.tileBases[TileName.Tapped];
            }
            else
            {
                return;
            }
        }
        
        GridBuildingSystem.current.mainTilemap.SetTilesBlock(BuildingArea, tileArray);

        TileDataManager.MarkTileAsAvailable(postion);
    }

    public void StartTapping()
    {
        temp = Instantiate(tappingPrefab);
    }

    private void StopTapping()
    {
        Destroy(temp);
    }

    IEnumerator HideWarningAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        warningText.enabled = false;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Tilemaps;

public class GridBuildingSystem : MonoBehaviour
{
    public static GridBuildingSystem current;

    public GridLayout gridLayout;
    public Tilemap mainTilemap;
    public Tilemap tempTilemap;


    public static Dictionary<TileName, TileBase> tileBases = new Dictionary<TileName, TileBase>();

    private Building temp;
    private Vector3 prevPos;
    private BoundsInt prevArea;

    [SerializeField] public int gridWidth = 10;
    [SerializeField] public int gridHeight = 10;

    public Camera mainCamera;

    
    #region Unity Methods

    private void Awake()
    {
        current = this;
    }

    private void Start()
    {
        string tilePath = @"Tiles\";
        tileBases.Add(TileName.Empty, null);
        tileBases.Add(TileName.Untapped, Resources.Load<TileBase>(tilePath + "Untapped"));
        tileBases.Add(TileName.Tapped, Resources.Load<TileBase>(tilePath + "Tapped"));
        tileBases.Add(TileName.GreenTapped, Resources.Load<TileBase>(tilePath + "GreenTapped"));
        tileBases.Add(TileName.RedTapped, Resources.Load<TileBase>(tilePath + "RedTapped"));
        tileBases.Add(TileName.GreenUntapped, Resources.Load<TileBase>(tilePath + "GreenUntapped"));
        tileBases.Add(TileName.RedUntapped, Resources.Load<TileBase>(tilePath + "RedUntapped"));

        TileDataManager.Initialize(gridWidth, gridHeight);

        InitializeGridMap();

        mainCamera.transform.position = new Vector3(gridWidth * 4 / 2 + 3, gridHeight * 4 / 2 + 1, mainCamera.transform.position.z);

        BuildingManager.current.CreateBase();

    }


    private void Update()
    {
        
    }

    #endregion

    #region Tilemap Management
    public static TileBase[] GetTilesBlock(BoundsInt area, Tilemap tilemap)
    {
        TileBase[] array = new TileBase[area.size.x * area.size.y * area.size.z];
        // Debug.Log("Array Size: " + array.Length);
        int counter = 0;

        foreach(var v in area.allPositionsWithin)
        {
            Vector3Int pos = new Vector3Int(v.x, v.y, v.z);
            array[counter] = tilemap.GetTile(pos);
            counter++;
        }

        return array;
    }

    public static void SetTilesBlock(BoundsInt area, TileName type, Tilemap tilemap)
    {
        int size = area.size.x * area.size.y * area.size.z;
        TileBase[] tileArray = new TileBase[size];
        FillTiles(tileArray, type);
        tilemap.SetTilesBlock(area, tileArray);
    }

    public static void FillTiles(TileBase[] array, TileName type)
    {
        for(int i = 0; i < array.Length; i++)
        {
            array[i] = tileBases[type];
        }
    }

    public void InitializeGridMap()
    {
        // Create a new BoundsInt for the entire grid
        BoundsInt fullGridBounds = new BoundsInt(0, 0, 0, gridWidth, gridHeight, 1);

        // Fill the entire grid with Untapped tiles
        SetTilesBlock(fullGridBounds, TileName.Untapped, mainTilemap);

        // Initialize the TileDataManager with the grid dimensions
        TileDataManager.Initialize(gridWidth, gridHeight);

        // Mark all tiles as untapped (available) in the TileDataManager
        foreach (Vector3Int position in fullGridBounds.allPositionsWithin)
        {
            // Debug.Log(position);
            TileDataManager.MarkTileAsUnavailable(position);
        }

        // Calculate the position of the center of the grid
        int centerX = gridWidth / 2;
        int centerY = gridHeight / 2;

        // Create a new BoundsInt for the 3x3 center
        BoundsInt centerBounds = new BoundsInt(centerX - 1, centerY - 1, 0, 3, 3, 1);

        // Fill the 3x3 center with Tapped tiles
        SetTilesBlock(centerBounds, TileName.Tapped, mainTilemap);

        // Mark the 3x3 center as tapped (unavailable) in the TileDataManager
        foreach (Vector3Int position in centerBounds.allPositionsWithin)
        {
            // Debug.Log(position);
            TileDataManager.MarkTileAsAvailable(position);
        }
    }


    #endregion


    #region Building Placement

    public void InitializeWithBuilding(GameObject building)
    {
        temp = Instantiate(building, Vector3.zero, Quaternion.identity).GetComponent<Building>();
        // Debug.Log("Initialized" + temp);
        FollowBuilding();
    }

    public void ClearArea()
    {
        TileBase[] toClear = new TileBase[prevArea.size.x * prevArea.size.y * prevArea.size.z];
        FillTiles(toClear, TileName.Empty);
        tempTilemap.SetTilesBlock(prevArea, toClear);
    }

    private void FollowBuilding()
    {
        // Debug.Log("Follow Building");
        ClearArea();

        temp.area.position = gridLayout.WorldToCell(temp.transform.position);
        BoundsInt buildingArea = temp.area;

        TileBase[] baseArray = GetTilesBlock(buildingArea, mainTilemap);

        int size = baseArray.Length;
        TileBase[] tileArray = new TileBase[size];

        for(int i = 0; i < baseArray.Length; i++)
        {
            if (baseArray[i] == tileBases[TileName.Tapped])
            {
                
                tileArray[i] = tileBases[TileName.GreenTapped];
            }
            else
            {
                FillTiles(tileArray, TileName.RedTapped);
                break;
            }
        }


        tempTilemap.SetTilesBlock(buildingArea, tileArray);
        prevArea = buildingArea;
    }


    public bool CanTakeArea(BoundsInt area)
    {
        TileBase[] baseArray = GetTilesBlock(area, mainTilemap);

        foreach(var b in baseArray)
        {
            if(b != tileBases[TileName.Tapped])
            {
                Debug.Log("Can't take area");
                return false;
            }
        }

        return true;
    }

    public void TakeArea(BoundsInt area)
    {
        SetTilesBlock(area, TileName.Empty, tempTilemap);
        SetTilesBlock(area, TileName.GreenTapped, mainTilemap);
    }
    #endregion
}

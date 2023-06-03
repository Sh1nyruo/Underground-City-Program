using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public static class TileDataManager
{
    private static Dictionary<Vector3Int, TileData> tileDataMap;
    public static void Initialize(int gridWidth, int gridHeight)
    {
        tileDataMap = new Dictionary<Vector3Int, TileData>();

        for (int x = 0; x < gridWidth; x++)
        {
            for (int y = 0; y < gridHeight; y++)
            {
                Vector3Int position = new Vector3Int(x, y, 0);
                TileData tileData = new TileData(position);
                tileDataMap.Add(position, tileData);
            }
        }
    }

    public static void PlaceBuilding(GameObject building, BuildingName buildingName,Vector3Int position)
    {
        if (tileDataMap.TryGetValue(position, out TileData tileData))
        {
            tileData.building = building;
            tileData.buildingName = buildingName;
            tileData.state = TileState.Occupied;
        }
        else
        {
            Debug.LogError("Invalid position: " + position);
        }
    }

    public static bool IsTileAvailable(Vector3Int position)
    {
        if (tileDataMap.TryGetValue(position, out TileData tileData))
        {
            return tileData.state == TileState.Tapped;
        }
        else
        {
            Debug.LogError("Invalid position: " + position);
            return false;
        }
    }

    public static void TraverseAllTiles(Action<TileData> action)
    {
        foreach (TileData tile in tileDataMap.Values)
        {
            action(tile);
        }
    }

    public static TileData GetTileData(Vector3Int position)
    {
        if (tileDataMap.TryGetValue(position, out TileData tileData))
        {
            return tileData;
        }
        else
        {
            Debug.LogError("Invalid position: " + position);
            return null;
        }
    }

    public static void MarkTileAsAvailable(Vector3Int position)
    {
        if (tileDataMap.TryGetValue(position, out TileData tileData))
        {
            tileData.state = TileState.Tapped;
            tileData.building = null;
        }
        else
        {
            Debug.LogError("Invalid position: " + position);
        }
    }

    public static void MarkTileAsUnavailable(Vector3Int position)
    {
        if (tileDataMap.TryGetValue(position, out TileData tileData))
        {
            // Debug.Log("Marking tile as unavailable: " + position);
            tileData.state = TileState.Untapped;
        }
        else
        {
            Debug.LogError("Invalid position: " + position);
        }
    }


}
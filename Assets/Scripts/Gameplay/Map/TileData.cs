using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileData
{
    public GameObject building;
    public BuildingName buildingName;
    public TileState state;
    public Vector3Int position;

    public TileData(Vector3Int position, TileState state = TileState.Untapped)
    {
        building = null;
        buildingName = BuildingName.empty;
        this.state = state;
        this.position = position;
    }
}
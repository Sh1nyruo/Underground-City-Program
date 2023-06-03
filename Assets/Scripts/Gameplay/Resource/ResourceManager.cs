using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ResourceManager : MonoBehaviour
{
    public int turn = 1;

    public static ResourceManager current;

    public int foodStorage = 0;
    public int foodProduction = 0;

    public int stoneStorage = 0;
    public int stoneProduction = 0;

    public int oreStorage = 0;
    public int oreProduction = 0;

    public int metalStorage = 0;
    public int metalProduction = 0;

    public int toolStorage = 0;
    public int toolProduction = 0;

    public int research = 0;

    public int population = 0;


    public int stoneTappingCost = 100;
    public int oreTappingCost = 100;

    public int metalTappingCost = 10;
    public int toolTappingCost = 10;
    private void Awake()
    {
        Initialize();
        current = this;
    }

    private void Start()
    {
        
    }

    private void Initialize()
    {
        foodStorage = 500;
        stoneStorage = 500;
        oreStorage = 500;

        metalStorage = 300;
        toolStorage = 200;
    }

    public bool AddResource(Building building)
    {
        if (foodStorage < building.foodPlacementConsumption || stoneStorage < building.stonePlacementConsumption ||
                       oreStorage < building.orePlacementConsumption ||
                                  metalStorage < building.metalPlacementConsumption ||
                                             toolStorage < building.toolPlacementConsumption ||
                                                population < building.populationConsumption)
        {
            return false;
        }
        else
        {
            foodStorage -= building.foodPlacementConsumption;
            stoneStorage -= building.stonePlacementConsumption;
            oreStorage -= building.orePlacementConsumption;
            metalStorage -= building.metalPlacementConsumption;
            toolStorage -= building.toolPlacementConsumption;

            foodProduction += building.foodProductionPerTurn;
            stoneProduction += building.stoneProductionPerTurn;
            oreProduction += building.oreProductionPerTurn;
            metalProduction += building.metalProductionPerTurn;
            toolProduction += building.toolProductionPerTurn;

            foodProduction -= building.foodConsumptionPerTurn;
            stoneProduction -= building.stoneConsumptionPerTurn;
            oreProduction -= building.oreConsumptionPerTurn;
            metalProduction -= building.metalConsumptionPerTurn;
            toolProduction -= building.toolConsumptionPerTurn;

            research += building.researchProduction;

            population += building.populationProduction;

            population -= building.populationConsumption;

            return true;
        }
    }

    public void NextTurn()
    {
        foodStorage = Math.Max(0, foodStorage + foodProduction);
        stoneStorage = Math.Max(0, stoneStorage + stoneProduction);
        oreStorage = Math.Max(0, oreStorage + oreProduction);
        metalStorage = Math.Max(0, metalStorage + metalProduction);
        toolStorage = Math.Max(0, toolStorage + toolProduction);
    }


    public bool TappingConsumption()
    {
        if (stoneStorage < stoneTappingCost || oreStorage < oreTappingCost || toolStorage < toolTappingCost || metalStorage < metalTappingCost)
        {
            return false;
        }
        else
        {
            stoneStorage -= stoneTappingCost;
            oreStorage -= oreTappingCost;

            toolStorage -= toolTappingCost;
            metalStorage -= metalTappingCost;

            return true;
        }
    }
}

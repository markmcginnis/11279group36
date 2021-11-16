using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    public int capacity;
    public int totalPopulation;
    public int healthyPopulation;
    public int sickPopulation;
    public float maxConcentration;
    public float fecalMatterConcentration;
    public float decompositionRate;
    public float antibioticsMax;
    public float growthConstant;
    public int fishPrice;
    public int filteringCost;
    public int antibioticCost;
    public int totalRevenue;
    public int totalCosts;
    public int totalProfit;

    public int week;
    public int fishSold;

    public List<LineGraphManager.GraphData> gd1;

    public List<LineGraphManager.GraphData> gd2;

    public SaveData(FisheryManager fisheryData, EconomyManager economyData, LineGraphManager graphData)
    {
        capacity = fisheryData.capacity;
        totalPopulation = fisheryData.totalPopulation;
        healthyPopulation = fisheryData.healthyPopulation;
        sickPopulation = fisheryData.sickPopulation;
        maxConcentration = fisheryData.maxConcentration;
        fecalMatterConcentration = fisheryData.fecalMatterConcentration;
        decompositionRate = fisheryData.decompositionRate;
        antibioticsMax = fisheryData.antibioticsMax;
        growthConstant = fisheryData.growthConstant;
        fishPrice = economyData.fishPrice;
        filteringCost = economyData.filteringCost;
        antibioticCost = economyData.antibioticCost;
        totalRevenue = economyData.totalRevenue;
        totalCosts = economyData.totalCosts;
        totalProfit = economyData.totalProfit;
        gd1 = graphData.graphData1;
        gd2 = graphData.graphData2;
        week = fisheryData.week;
        fishSold = economyData.fishSold;
    }
}

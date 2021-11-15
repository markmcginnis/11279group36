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
    public double maxConcentration;
    public double fecalMatterConcentration;
    public double decompositionRate;
    public double antibioticsMax;
    public double growthConstant;
    public int fishPrice;
    public int filteringCost;
    public int antibioticCost;
    public int totalRevenue;
    public int totalCosts;
    public int totalProfit;

    public SaveData(FisheryManager fisheryData, EconomyManager economyData)
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
    }
}


using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class EconomyManager : MonoBehaviour
{
    public int fishPrice = 10; //affects revenue

    public int filteringCost = 2500; //affects cost
    public int antibioticCost = 2500;

    public int totalRevenue = 0; //stores values
    public int totalCosts = 0;
    public int totalProfit = 0;

    public void Sell(int harvestCount, double filtering, double antibiotics)
    {
        totalRevenue += harvestCount * fishPrice; //update totalRevenue according to price
        totalCosts += (int)Math.Floor(filtering * filteringCost) + (int)Math.Floor(antibiotics * antibioticCost); //update totalCosts according to filtering/antibiotics
        totalProfit = totalRevenue - totalCosts; //update totalProfit
    }
}

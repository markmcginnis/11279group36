using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class FisheryManager : MonoBehaviour
{
    //Attributes
    public int capacity = 1000;
    public int totalPopulation = 100;
    public int healthyPopulation = 65;
    public int sickPopulation = 35; //several categories for population numbers

    public float maxConcentration = 1f;
    public float fecalMatterConcentration = 0.2f; //keep track of our various things for the math models
    public float decompositionRate = 25f;
    public float antibioticsMax = 1f;

    public float growthConstant = 0.5f; //FIXME change to feeding amount??

    public Slider filteringSlider;
    public Slider antibioticsSlider; //sliders for values
    public Slider harvestSlider;

    public bool harvest = false; //only harvest when this is true

    float windowSize = 1f;
    float stepSize = 0.01f; //euler values

    public GameManager gm; //holds FishMath
    public EconomyManager em; //holds economy stuff

    public void TimeStep()
    {
        //print("Before: ");
        //print("Total Population: " + totalPopulation);
        //print("Healthy Population: " + healthyPopulation);
        //print("Sick Population: " + sickPopulation);
        //print("Fecal Concentration: " + fecalMatterConcentration);
        Harvest();
        AdvanceFecal();
        Disease();
        Birth();
        //print("After: ");
        //print("Total Population: " + totalPopulation);
        //print("Healthy Population: " + healthyPopulation);
        //print("Sick Population: " + sickPopulation);
        //print("Fecal Concentration: " + fecalMatterConcentration);
    }

    public void Harvest()
    {
        if (!harvest)
        {
            em.Sell(0, filteringSlider.value, antibioticsSlider.value);
            return;
        }
        int harvestCount = (int)Mathf.Floor(harvestSlider.value * healthyPopulation);
        //print("HarvestCount: " + harvestCount);
        healthyPopulation -= harvestCount;
        totalPopulation -= harvestCount;
        //print("Population after Harvest: " + totalPopulation);
        em.Sell(harvestCount, filteringSlider.value, antibioticsSlider.value);
    }

    public void AdvanceFecal()
    {
        //print("Fecal before: " + fecalMatterConcentration);
        float newFecalMatterConcentration = gm.GetFecalConcentration(totalPopulation, fecalMatterConcentration, filteringSlider.value * decompositionRate);
        newFecalMatterConcentration = Mathf.Clamp(newFecalMatterConcentration, 0, maxConcentration);
        fecalMatterConcentration = newFecalMatterConcentration;
        //print("Fecal after: " + fecalMatterConcentration);
    }

    public void Disease()
    {
        float diseaseProbability = gm.Disease(totalPopulation, maxConcentration, fecalMatterConcentration, antibioticsSlider.value/2f, antibioticsMax);
        int newDiseasedPopulation = (int)Mathf.Floor(diseaseProbability * healthyPopulation);
        sickPopulation += newDiseasedPopulation;
        healthyPopulation -= newDiseasedPopulation; //disease affects first
        
        float recoveryProbability = gm.Recovery(sickPopulation, maxConcentration, fecalMatterConcentration, antibioticsSlider.value/2f, antibioticsMax);
        int newHealthyPopulation = (int)Mathf.Floor(recoveryProbability * sickPopulation);
        healthyPopulation += newHealthyPopulation;
        sickPopulation -= newHealthyPopulation; //recovery affects second
    }

    public void Birth()
    {
        int newPopulation = gm.EulerLogistic(totalPopulation, capacity, windowSize, stepSize, growthConstant);
        healthyPopulation += newPopulation - totalPopulation;
        totalPopulation = newPopulation;
    }

    public void DecreaseGrowth()
    {
        growthConstant = Mathf.Max(0.2f,growthConstant - 0.1f); //growthConstant cannot go below 0.2
    }

    public void IncreaseGrowth()
    {
        growthConstant = Mathf.Min(0.8f, growthConstant + 0.1f); //growthConstant cannot go above 0.8
    }

    public void ToggleHarvest()
    {
        harvest = !harvest;
    }

    public void ButtonTest()
    {
        Debug.Log("button has been pressed");
    }

    
}

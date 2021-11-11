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

    public double maxConcentration = 1;
    public double fecalMatterConcentration = 0.2; //keep track of our various things for the math models
    public double decompositionRate = 25;
    public double antibioticsMax = 1;

    public double growthConstant = 0.5; //FIXME change to feeding amount??

    public Slider filteringSlider;
    public Slider antibioticsSlider; //sliders for values
    public Slider harvestSlider;

    public bool harvest = false; //only harvest when this is true

    double windowSize = 1;
    double stepSize = 0.01; //euler values

    public GameManager gm; //holds FishMath
    public EconomyManager em; //holds economy stuff

    public void TimeStep()
    {
        Harvest();
        AdvanceFecal();
        Disease();
        Birth();
    }

    public void Harvest()
    {
        if (!harvest)
        {
            em.Sell(0, (double)filteringSlider.value, (double)antibioticsSlider.value);
            return;
        }
        int harvestCount = (int)Math.Floor((double)harvestSlider.value * healthyPopulation);
        healthyPopulation -= harvestCount;
        totalPopulation -= harvestCount;
        em.Sell(harvestCount, (double)filteringSlider.value, (double)antibioticsSlider.value);
    }

    public void AdvanceFecal()
    {
        print(fecalMatterConcentration);
        fecalMatterConcentration = gm.GetFecalConcentration(totalPopulation, fecalMatterConcentration, (double)filteringSlider.value * decompositionRate);
        print(fecalMatterConcentration);
    }

    public void Disease()
    {
        double diseaseProbability = gm.Disease(totalPopulation, maxConcentration, fecalMatterConcentration, (double)antibioticsSlider.value, antibioticsMax);
        int newDiseasedPopulation = (int)Math.Floor(diseaseProbability * healthyPopulation);
        sickPopulation += newDiseasedPopulation;
        healthyPopulation -= newDiseasedPopulation; //disease affects first
        
        double recoveryProbability = gm.Recovery(sickPopulation, maxConcentration, fecalMatterConcentration, (double)antibioticsSlider.value, antibioticsMax);
        int newHealthyPopulation = (int)Math.Floor(recoveryProbability * sickPopulation);
        healthyPopulation += newHealthyPopulation;
        sickPopulation -= newHealthyPopulation; //recovery affects second
    }

    public void Birth()
    {
        totalPopulation = gm.EulerLogistic(totalPopulation, capacity, windowSize, stepSize, growthConstant); //if this changes to healthy only, change capacity to capacity - diseased
    }

    public void DecreaseGrowth()
    {
        growthConstant = Math.Max(0.2,growthConstant - 0.1); //growthConstant cannot go below 0.2
    }

    public void IncreaseGrowth()
    {
        growthConstant = Math.Min(0.8, growthConstant + 0.1); //growthConstant cannot go above 0.8
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

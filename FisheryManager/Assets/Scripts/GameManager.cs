using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System;

public class GameManager : MonoBehaviour
{
    //all UI elements
    public Slider fecalMeter;
    public Image fecalFill;
    public Slider diseaseMeter;
    public Image diseaseFill;
    public Slider populationMeter;
    public Image populationFill;

    public TMP_Text feedAmount;
    public Text weekText;
    public Text profitText;
    public Text fishText;

    public Text harvestButton;

    public FisheryManager fm;
    public EconomyManager em;

    public LineGraphManager lm;

    //update all UI elements
    void Update()
    {
        //update harvest button with true/false
        harvestButton.text = (fm.harvest) ? "Harvest: Yes" : "Harvest: No";
        //update potential harvest/profit/cost text
        weekText.text = "Week " + fm.week;
        profitText.text = "Total Profit: $" + em.totalProfit;
        fishText.text = "Fish Sold: " + em.fishSold;
        //update fecal meter
        fecalMeter.value = (float)fm.fecalMatterConcentration / (float)fm.maxConcentration;
        //print("FecalMeter: " + fecalMeter.value);
        if(fecalMeter.value < 0.25)
        {
            fecalFill.color = Color.green;
        }
        else if(fecalMeter.value < 0.75)
        {
            fecalFill.color = Color.yellow;
        }
        else
        {
            fecalFill.color = Color.red;
        }
        //update disease meter
        diseaseMeter.value = (float)fm.sickPopulation / (float)fm.totalPopulation;
        //print("DiseaseMeter: " + diseaseMeter.value);
        if(diseaseMeter.value < 0.25)
        {
            diseaseFill.color = Color.green;
        }
        else if (diseaseMeter.value < 0.75)
        {
            diseaseFill.color = Color.yellow;
        }
        else
        {
            diseaseFill.color = Color.red;
        }
        //update population meter
        populationMeter.value = (float)fm.totalPopulation / (float)fm.capacity;
        //print("PopulationMeter: " + populationMeter.value);
        if (populationMeter.value < 0.25)
        {
            populationFill.color = Color.red;
        }
        else if (populationMeter.value < 0.75)
        {
            populationFill.color = Color.yellow;
        }
        else
        {
            populationFill.color = Color.green;
        }
        //update feeding amount
        feedAmount.text = "Feeding Amount: " + fm.growthConstant;
    }



    public void MainMenu()
    {
        SceneManager.LoadScene("StartScene");
    }



    /*
        assume assumption that maxPopulation is 100 "fish"
        assume rate of decomposition of 5 kg/m^3/week
        assume constant time step of 7 days
        assume "maximum" fecal matter concetration of 10 kg/m^3
        */

    /*
    structure for unit testing
    1. Check valid probability (0 < x < 1) on lowest input value
    2. Check valid probaibility (0 < x < 1) on highest input value
    3. Check valid probability at median value
    4. Check that probability is nonlinear f(0.5) - f(0.25) != f(0.75) = f(0.5)
    */
    public int EulerLogistic(int currentPop, int carryingCap, float n, float h, float growthConstant)
    {
        // this method uses Euler's Method and a logistic ODE
        if (currentPop == 0)
        {
            return currentPop;
        }
        float result = currentPop;
        for (float i = 0; i <= n; i += h)
        {
            result += h * growthConstant * result * (1.0f - (result / carryingCap));
        }
        int finalResult = (int)Mathf.Floor(result);
        return finalResult;
    }

    public float Disease(int population, float maxConc, float fecalMatterConc, float antibiotics, float antibioticsMax)
    {
        // this method uses an exponential distribution with lambda = 1
        float exp = -1f * (maxConc - fecalMatterConc) / 2f;
        float probability = Mathf.Exp(exp);
        float antibioticsMultiplier = Mathf.Log(2.5f - antibiotics / antibioticsMax);
        // this method returns the probability of a fish catching the disease, puts a cap at 0.75
        float result = Mathf.Clamp(antibioticsMultiplier * probability, 0f, 0.75f);
        //float result = antibioticsMultiplier * probability > 0.75f ? antibioticsMultiplier * probability : 0.75f;
        return result;
    }

    public float Recovery(int population, float maxConc, float fecalMatterConc, float antibiotics, float antibioticsMax)
    {
        float probability = Mathf.Exp(-1f * (fecalMatterConc / maxConc));
        probability /= 4f;
        return probability;
    }

    public int Harvest(int population, int harvest)
    {
        return population -= harvest;
    }

    public float GetFecalConcentration(int population, float fecalMatterConc, float decomposition)
    {
        float result = fecalMatterConc + 0.01f * population;
        result -= decomposition; // assume a decomposition of 5
        result = result < 0 ? 0 : result;
        return result;
    }

    public void saveGame()
    {
        SaveAndLoad.saveGame(fm, em, lm);
    }

    public void loadGame()
    {
        SaveData newData = SaveAndLoad.loadGame();
        fm.capacity = newData.capacity;
        fm.totalPopulation = newData.totalPopulation;
        fm.healthyPopulation = newData.healthyPopulation;
        fm.sickPopulation = newData.sickPopulation;
        fm.maxConcentration = newData.maxConcentration;
        fm.fecalMatterConcentration = newData.fecalMatterConcentration;
        fm.decompositionRate = newData.decompositionRate;
        fm.antibioticsMax = newData.antibioticsMax;
        fm.growthConstant = newData.growthConstant;
        em.fishPrice = newData.fishPrice;
        em.filteringCost = newData.filteringCost;
        em.antibioticCost = newData.antibioticCost;
        em.totalRevenue = newData.totalRevenue;
        em.totalCosts = newData.totalCosts;
        em.totalProfit = newData.totalProfit;
        lm.graphData1 = newData.gd1;
        lm.graphData2 = newData.gd2;
        fm.week = newData.week;
        em.fishSold = newData.fishSold;
    }
}

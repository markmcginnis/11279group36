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
    //more text elements for economy tab

    public Text harvestButton;

    public FisheryManager fm;
    public EconomyManager em;

    //update all UI elements
    void Update()
    {
        //update harvest button with true/false
        harvestButton.text = (fm.harvest) ? "Harvest: Yes" : "Harvest: No";
        //update potential harvest/profit/cost text

        //update fecal meter
        fecalMeter.value = (float)fm.fecalMatterConcentration;
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
        if (diseaseMeter.value < 0.25)
        {
            diseaseFill.color = Color.red;
        }
        else if (diseaseMeter.value < 0.75)
        {
            diseaseFill.color = Color.yellow;
        }
        else
        {
            diseaseFill.color = Color.green;
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
    public int EulerLogistic(int currentPop, int carryingCap, double n, double h, double growthConstant)
    {
        // this method uses Euler's Method and a logistic ODE
        if (currentPop == 0)
        {
            return currentPop;
        }
        double result = currentPop;
        for (double i = 0; i <= n; i += h)
        {
            result += h * growthConstant * result * (1.0 - (result / carryingCap));
        }
        int finalResult = (int)Math.Floor(result);
        return finalResult;
    }

    public double Disease(int population, double maxConc, double fecalMatterConc, double antibiotics, double antibioticsMax)
    {
        // this method uses an exponential distribution with lambda = 1
        double exp = -1 * (maxConc - fecalMatterConc) / 2;
        double probability = Math.Exp(exp);
        double antibioticsMultiplier = Math.Log(2.5 - antibiotics / antibioticsMax);
        // this method returns the probability of a fish catching the disease, puts a cap at 0.75
        double result = antibioticsMultiplier * probability > 0.75 ? antibioticsMultiplier * probability : 0.75;
        return result;
    }

    public double Recovery(int population, double maxConc, double fecalMatterConc, double antibiotics, double antibioticsMax)
    {
        double probability = Math.Exp(-1 * (fecalMatterConc / maxConc));
        probability /= 4;
        return probability;
    }

    public int Harvest(int population, int harvest)
    {
        return population -= harvest;
    }

    public double GetFecalConcentration(int population, double fecalMatterConc, double decomposition)
    {
        double result = fecalMatterConc + 0.01 * population;
        result -= decomposition; // assume a decomposition of 5
        result = result < 0 ? 0 : result;
        return result;
    }
}

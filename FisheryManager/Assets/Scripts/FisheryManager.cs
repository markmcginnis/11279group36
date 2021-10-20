using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FisheryManager : MonoBehaviour
{
    //Attributes
    public int capacity = 1000;
    public int totalPopulation = 100;
    public int healthyPopulation = 65;
    public int sickPopulation = 10; //several categories for proportion based stuff
    public int youngPopulation = 25;

    public float harvestProportion = 0.3f;
    public int harvestCount = 30; //one of these, but not both, unless we want 2 ways to choose harvest amount (not recommended)

    public void Harvest()
    {
        //harvest logic here, clamp the harvested population to healthyPopulation
        Debug.Log("harvest pressed");
    }

    public void Disease()
    {
        //harvest logic here, use random and counter to determine numbers
    }

    public void Birth()
    {
        //birthing logic here, use random and counter to determine numbers
    }

    // Start is called before the first frame update
    public void Start()
    {
        //initialize what is needed
    }
}

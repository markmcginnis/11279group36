using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EconomyManager : MonoBehaviour
{
    public int balance = 1000;
    public int costsPerWeek = 100;

    public void Sell()
    {
        Debug.Log("sell pressed");
    }

    void Start()
    {
        //initialize what is needed
    }
}

using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using MathModel;
public class MathTestSuite
{
    // Start is called before the first frame update
    [UnityTest]
    public IEnumerator testAddIntegers()
    {
        var testObject = new FishMath();
        int temp = testObject.testAdd(5, 3);
        yield return null;
        Assert.AreEqual(temp, 8);
    }

    // int maxConcentration = 10;
    // double decomposition = 5;
    int carryingCapacity = 100;
    double windowSize = 1;
    double stepSize = 0.01;

    
    [UnityTest]
    public IEnumerator testEulerLogistic0()
    {
        var testObject = new FishMath();
        int result = testObject.EulerLogistic(0, carryingCapacity, windowSize, stepSize);
        yield return null;
        Assert.AreEqual(result, 0);
    }
    
    [UnityTest]
    public IEnumerator testEulerLogistic25()
    {
        var testObject = new FishMath();
        int result = testObject.EulerLogistic(25, carryingCapacity, windowSize, stepSize);
        System.Diagnostics.Debug.WriteLine(result);
        yield return null;
        Assert.LessOrEqual(result, 60);
        Assert.Greater(result, 25);
    }

    [UnityTest]
    public IEnumerator testEulerLogistic50()
    {
        var testObject = new FishMath();
        int result = testObject.EulerLogistic(50, carryingCapacity, windowSize, stepSize);
        yield return null;
        Assert.LessOrEqual(result, 75);
        Assert.Greater(result, 55);
    }
    [UnityTest]
    public IEnumerator testEulerLogistic75()
    {
        var testObject = new FishMath();
        int result = testObject.EulerLogistic(75, carryingCapacity, windowSize, stepSize);
        yield return null;
        Assert.LessOrEqual(result, 90);
        Assert.Greater(result, 77);
    }
    [UnityTest]
    public IEnumerator testEulerLogistic100()
    {
        var testObject = new FishMath();
        int result = testObject.EulerLogistic(100, carryingCapacity, windowSize, stepSize);
        yield return null;
        Assert.AreEqual(result, 100);
    }
}

using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class Tests
{
    [Test]
    public void Multiplication()
    {
        int a = 5;
        int b = 2;
        int result = a * b;
        Assert.AreEqual(10, result);
    }

    [Test]
    public void RemoveVowelsFromString()
    {
        //this method only removes the lowercase vowels, as shown in the specifications
        string input = "this is a testing string babebibobub";
        string result = "";
        char[] chars = input.ToCharArray();
        for (int i = 0; i < input.Length; i++)
        {
            if (!(chars[i].Equals('a') || chars[i].Equals('e') || chars[i].Equals('i') || chars[i].Equals('o') || chars[i].Equals('u')))
            {
                result += chars[i];
            }
        }

        //the above removes the vowels
        //below checks to make sure all vowels are removed

        int index = -1;
        index = result.IndexOf('a');
        if (index == -1)
        {
            index = result.IndexOf('e');
        }
        if (index == -1)
        {
            index = result.IndexOf('i');
        }
        if (index == -1)
        {
            index = result.IndexOf('o');
        }
        if (index == -1)
        {
            index = result.IndexOf('u');
        }
        Assert.AreEqual(-1, index);
    }
}
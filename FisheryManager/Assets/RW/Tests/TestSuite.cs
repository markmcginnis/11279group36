using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

public class TestSuite
{
    public IEnumerator RemoveAllVowelsFromInputString(string input)
    {
        string result = "";
        char[] chars = input.ToCharArray();
        for (int i = 0; i < input.Length; i++)
        {
            if (!(chars[i].Equals('a') || chars[i].Equals('e') || chars[i].Equals('i') || chars[i].Equals('o') || chars[i].Equals('u')))
            {
                result += chars[i];
            }
        }
        yield return null;

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
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveAndLoad
{
    public static void saveGame(FisheryManager fisheryData, EconomyManager economyData, LineGraphManager graphData)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/saveData.txt";
        FileStream stream = new FileStream(path, FileMode.Create);
        SaveData saveData = new SaveData(fisheryData, economyData, graphData);
        formatter.Serialize(stream, saveData);
        stream.Close();
    }

    public static SaveData loadGame()
    {
        string path = Application.persistentDataPath + "/saveData.txt";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            SaveData saveData = formatter.Deserialize(stream) as SaveData;
            stream.Close();
            return saveData;
        }
        else
        {
            Debug.LogError("No save file!");
            return null;
        }
    }
}

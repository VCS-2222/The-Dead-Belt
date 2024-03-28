//using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using UnityEditor;
using UnityEngine;

public interface IDataSaver
{
    bool SaveData<T>(string PathNeeded, T Data);

    T LoadData<T>(string PathNeeded);
}

public class SaveSystem : IDataSaver
{
    public bool SaveData<T>(string PathNeeded, T Data)
    {
        string path = Application.persistentDataPath + PathNeeded;
        try
        {
            if (File.Exists(path))
            {
                Debug.Log("Data exists, deleting old data file!");
                File.Delete(path);
            }
            else
            {
                Debug.Log("Making file!");
            }

            using FileStream stream = File.Create(path);
            stream.Close();
            //File.WriteAllText(path, JsonConvert.SerializeObject(Data));
            return true;
        }
        catch(Exception exception)
        {
            Debug.LogError("Unable to save data" + exception.Message + " " + exception.StackTrace);
            return false;
        }
    }

    public T LoadData<T>(string PathNeeded)
    {
        throw new NotImplementedException();

        //string path = Application.persistentDataPath + PathNeeded;

        //if (!File.Exists(path))
        //{
        //    Debug.LogError("Cannot load file " + path + ". File doesn't exist!");
        //}
    }
}
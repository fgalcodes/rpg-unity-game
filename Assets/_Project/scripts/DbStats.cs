using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DbStats : MonoBehaviour
{
    public string Name = "Ted";
    public int Health = 100;

    public void SaveIntoJson()
    {
        string data = JsonUtility.ToJson(this);
        File.WriteAllText(Application.persistentDataPath + "/Autosave.json", data);
    }

    public string GetPath()
    {
        return Path.GetDirectoryName(Application.persistentDataPath); 
    }

}

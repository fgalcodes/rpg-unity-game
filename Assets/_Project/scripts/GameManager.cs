using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	public static DataBase Data { get; set; }

    private void Awake()
    {
		try
		{
            StreamReader sr = File.OpenText("./Assets/_Project/Data/rpg-game.json");
			string content = sr.ReadToEnd();
			sr.Close();

			Debug.Log(content);
			Data = JsonUtility.FromJson<DataBase>(content);

		}
		catch (Exception ex)
		{
			Debug.LogException(ex);
		}
    }
}

public class DataBase
{
    public string Description { get; set; } 
    public PlayerData Player { get; set; }

    public class PlayerData
    {
		public float PosX { get; set; }
		public float PosY { get; set; }
		public float PosZ { get; set; }
    }
}
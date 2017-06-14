using System;
using System.IO;
using UnityEngine;
using System.Collections.Generic;

public class GameController : MonoBehaviour
{

    //public variables

    //private variables
    private static GameController _gameControllerInstance;
    private List<CatalogEntry> _catalog = new List<CatalogEntry>();

    //private variable accessors
    public static GameController Instance
    {
        get { return _gameControllerInstance ?? (_gameControllerInstance = new GameObject("GameController").AddComponent<GameController>()); }
    }

    //methods

    //method to read entire TXT file in on initialization
    private void ReadDataFile()
    {
        StreamReader reader = new StreamReader("Assets/Resources/catalog.txt");
        string s = reader.ReadLine();
        while (s != null)
        {
            char[] delimiter = { ',' };
            string[] entryFields = s.Split(delimiter);
            //Debug.Log(entryFields[0] + " " + entryFields[1] + " " + entryFields[2] + " " + entryFields[3] + " " + entryFields[4]);
            _catalog.Add(new CatalogEntry(entryFields[0], entryFields[1], Convert.ToBoolean(entryFields[2]), entryFields[3], entryFields[4]));
            s = reader.ReadLine();
        }
    }
    
    public void GetRoomCatalog(string roomID)
    {
        Debug.Log(roomID);
        ReadDataFile();
        //get index for where where entries start/stop in list?
        //return list and indices
    }

}
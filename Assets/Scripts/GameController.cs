using System;
using System.IO;
using UnityEngine;
using System.Collections.Generic;

public class GameController : MonoBehaviour
{

    //public variables

    //private variables
    private List<CatalogEntry> _catalog = new List<CatalogEntry>();

    //private variable accessors
    private static GameController _gameControllerInstance;
    public static GameController Instance
    {
        get { return _gameControllerInstance ?? (_gameControllerInstance = new GameObject("GameController").AddComponent<GameController>()); }
    }

    private string _currentRoom;
    public string CurrentRoom
    {
        get
        {
            return _currentRoom;
        }

        set
        {
            _currentRoom = value;
        }
    }

    //methods

    //method to read in entire TXT file for catalog on initialization
    private void ReadDataFile()
    {
        StreamReader reader = new StreamReader("Assets/Resources/catalog.txt");
        string s = reader.ReadLine();
        while (s != null)
        {
            char[] delimiter = { ',' };
            string[] entryFields = s.Split(delimiter);
            _catalog.Add(new CatalogEntry(entryFields[0], entryFields[1], Convert.ToBoolean(entryFields[2]), entryFields[3], entryFields[4]));
            s = reader.ReadLine();
        }
    }

    //only returns portion of the catalog for room which which player is located
    public List<CatalogEntry> GetRoomCatalog(string roomID)
    {
        List<CatalogEntry> roomCatalog = new List<CatalogEntry>();
        if (_catalog.Count == 0)
        {
            ReadDataFile();
        }

        foreach (CatalogEntry entry in _catalog)
        {
            if(roomID == entry.roomNumber)
            {
                roomCatalog.Add(entry);
            }
        }

        return roomCatalog;
    }

    //receives data from CatalogButtonScript.cs to flag art in catalog as already being displayed
    public void UpdateCatalog(string room, string artwork)
    {
        foreach(CatalogEntry entry in _catalog)
        {
            if(room == entry.roomNumber && artwork == entry.resourceTitle)
            {
                entry.resourceDisplayed = true;
            }
        }
    }

}
using UnityEngine;
using System.IO;
using System.Collections;
using System.Collections.Generic;

public class GameController : MonoBehaviour
{

    //public variables

    //private variables
    private static GameController _gameControllerInstance;

    //private variable accessors
    public static GameController Instance
    {
        get { return _gameControllerInstance ?? (_gameControllerInstance = new GameObject("GameController").AddComponent<GameController>()); }
    }


}
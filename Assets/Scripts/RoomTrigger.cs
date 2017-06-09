//<remarks>
// Capsule collider with "Player" name must be attached to SteamVR camera rig
// to detect when player is in room.
//</remarks>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTrigger : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.name == "Player")
        {
            Debug.Log("Player in " + gameObject.name);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            Debug.Log("Player left " + gameObject.name);
        }
    }
}

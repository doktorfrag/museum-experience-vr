//<remarks>
// Script must be attached to LeftController and RightControlller objects in [VRTK] package.
//</remarks>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocationDetector : MonoBehaviour
{
    //private variables
    private string _currentRoom = null;

    void Update()
    {
        GetCurrentPosition();
    }

    //method to tell GameController room in which player is currently located
    public void GetCurrentPosition()
    {
        //project ray down to find position
        RaycastHit hit;
        if (Physics.Raycast(gameObject.transform.position, Vector3.down, out hit))
        {
            if (hit.collider.tag == "Room" && hit.collider.name != _currentRoom)
            {
                //update GameController with current room
                _currentRoom = hit.collider.name;
                GameController.Instance.CurrentRoom = _currentRoom;
            }

        }
    }
}

//<remarks>
// Script must be attached to either LeftController or RightControlller object in [VRTK] package.
//</remarks>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    //serialized variables

    //public variables

    //private variables
    private string _currentRoom = null;

    //private variable accessors

    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(gameObject.transform.position, Vector3.down, out hit))
        {
            if (hit.collider.tag == "Room" && hit.collider.name != _currentRoom)
            {
                _currentRoom = hit.collider.name;

                //load room resources
                GameController.Instance.GetRoomCatalog(_currentRoom);
            }

        }
    }
}

using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class UI_CatalogManager : MonoBehaviour {
    //serialized variables
    [SerializeField]
    private Text _catalogTitle;

    [SerializeField]
    private GameObject _button;

    [SerializeField]
    private Transform _contentPanel;

    //private variables
    private string _currentRoom;
    private List<CatalogEntry> _roomCatalog = new List<CatalogEntry>();

    //private variable accessors

	// methods
	void Update () {
        //check to see which room player is in
        if (_currentRoom != GameController.Instance.CurrentRoom)
        {
            RefreshMenu();
        }
    }

    public void RefreshMenu()
    {
        //get room
        _currentRoom = GameController.Instance.CurrentRoom;

        //update title in scroll menu and get new room catalog from GameController
        _catalogTitle.text = "Catalog: " + _currentRoom;
        _roomCatalog = GameController.Instance.GetRoomCatalog(_currentRoom);

        //update buttons in scroll menu
        RemoveButtons();
        AddButtons();
    }

    private void RemoveButtons()
    {
        for (int i = 0; i < _contentPanel.childCount; i++)
        {
            //destroy all the buttons in room catalog!
            GameObject toRemove = transform.GetChild(i).gameObject;
            Destroy(toRemove);
        }
    }

    private void AddButtons()
    {
        foreach (CatalogEntry entry in _roomCatalog)
        {
            //see if art is already displayed in room
            if (entry.resourceDisplayed == false)
            {
                //if not, populate list with buttons of artwork in room catalog
                GameObject catalogButton = Instantiate(_button) as GameObject;
                CatalogButtonScript buttonScript = catalogButton.GetComponent<CatalogButtonScript>();
                buttonScript.roomNumber = entry.roomNumber;
                buttonScript.resourceTitle = entry.resourceTitle;
                buttonScript.resourceDisplayed = entry.resourceDisplayed;
                buttonScript.resourceDescription = entry.resourceDescription;
                buttonScript.filePath = entry.filePath;
                catalogButton.transform.SetParent(_contentPanel.transform, false);
            }
        }
    }
}

using UnityEngine;
using UnityEngine.UI;

public class CatalogButtonScript : MonoBehaviour {

    //public variables
    public string roomNumber;
    public string resourceTitle;
    public bool resourceDisplayed;
    public string resourceDescription;
    public string filePath;

    //private variables
    public Button _thisButton;
    private UI_CatalogManager _catalogList;

    // methods
    void Start ()
    {
        _thisButton = gameObject.GetComponent(typeof(Button)) as Button;
        _thisButton.GetComponentInChildren<Text>().text = resourceTitle;
        _thisButton.onClick.AddListener(HandleClick);
    }

    void HandleClick()
    {
        //update main catalog list in GameController.cs
        GameController.Instance.UpdateCatalog(roomNumber, resourceTitle);

        //update roopm catalog display
        _catalogList = gameObject.GetComponentInParent<UI_CatalogManager>();
        _catalogList.RefreshMenu();

        //instantiate artwork in room
        Debug.Log("Just hung " + resourceTitle + " in " + roomNumber + ". " + "Art description: " + resourceDescription);
    }
}

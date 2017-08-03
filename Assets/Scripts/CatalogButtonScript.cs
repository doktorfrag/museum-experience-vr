using UnityEngine;
using UnityEngine.UI;
using VRTK;
using VRTK.GrabAttachMechanics;
using VRTK.SecondaryControllerGrabActions;

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
        Debug.Log("Just instantiated " + resourceTitle + " in " + roomNumber + ". " + "Art description: " + resourceDescription);
        string artPath = "Exhibition Rooms/" + roomNumber + "/" + resourceTitle;
        GameObject art = Instantiate(Resources.Load(artPath)) as GameObject;
        art.AddComponent<VRTK_InteractableObject>();
        art.AddComponent<VRTK_FixedJointGrabAttach>();
        art.AddComponent<VRTK_SwapControllerGrabAction>();
        art.AddComponent<Rigidbody>();
        art.AddComponent<PictureScript>();

        //set options in VRTK_InteractableObject
        art.GetComponent<VRTK_InteractableObject>().disableWhenIdle = true;
        art.GetComponent<VRTK_InteractableObject>().isGrabbable = true;
        art.GetComponent<VRTK_InteractableObject>().stayGrabbedOnTeleport = true;
        art.GetComponent<VRTK_InteractableObject>().grabAttachMechanicScript = art.GetComponent<VRTK_FixedJointGrabAttach>();
        art.GetComponent<VRTK_InteractableObject>().secondaryGrabActionScript = art.GetComponent<VRTK_SwapControllerGrabAction>();

        //insert into scene
        art.transform.position = new Vector3(0, 1, 0);
    }
}

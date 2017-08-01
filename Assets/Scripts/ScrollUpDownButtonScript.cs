using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class ScrollUpDownButtonScript : MonoBehaviour, IPointerDownHandler {

    //serialized variables
    [SerializeField]
    private ScrollRectScript _scrollRectScript;

    [SerializeField]
    private bool _isDownButton;

    public void OnPointerDown(PointerEventData eventData)
    {
        if (_isDownButton)
        {
            _scrollRectScript.DownButtonPressed();
        }
        else
        {
            _scrollRectScript.UpButtonPressed();
        }
    }
}

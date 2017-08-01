using UnityEngine;
using UnityEngine.UI;

public class ScrollRectScript : MonoBehaviour {

    //serialized variables
    //public variables

    //private variables
    private ScrollRect _scrollRect;
    private bool _mouseDown, _buttonDown, _buttonUp;

    //private variable accessors

    //methods
    private void Start()
    {
        _scrollRect = GetComponent<ScrollRect>();
    }

    private void Update()
    {
        if (_mouseDown)
        {
            if(_buttonDown)
            {
                ScrollDown();
            }
            else if (_buttonUp)
            {
                ScrollUp();
            }
        }
    }

    public void DownButtonPressed()
    {
        _mouseDown = true;
        _buttonDown = true;
    }

    public void UpButtonPressed()
    {
        _mouseDown = true;
        _buttonUp = true;
    }

    private void ScrollDown()
    {
        if(Input.GetMouseButtonUp(0))
        {
            _mouseDown = false;
            _buttonDown = false;
        }
        else
        {
            _scrollRect.verticalNormalizedPosition -= 0.01f;
        }
    }

    private void ScrollUp()
    {
        if (Input.GetMouseButtonUp(0))
        {
            _mouseDown = false;
            _buttonUp = false;
        }
        else
        {
            _scrollRect.verticalNormalizedPosition += 0.01f;
        }
    }

}

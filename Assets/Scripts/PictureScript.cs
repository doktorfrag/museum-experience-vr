//<remarks>
//  Developed picture meshes need to have image applied on positive "z" side of the mesh
//  In addition to this script, the picture object also needs the following VRTK scripts:
//      - VRTK_InteractibleObject
//      - VRTK_FixedJointGrabAttach
//      - VRTK_SwapControllerGrabAction
//  The picture object also needs the following components attached:
//      - Rigidbody
//      - BoxCollider
//  In the VRTK_FixedJointGrabAttch script, the Break Force under Joint Options should be set to "Infinity"
//</remarks>


using UnityEngine;
using VRTK;

public class PictureScript : MonoBehaviour {

    //serialized variables

    //public variables
    public float fadeDuration = 0.5f;
    public bool useMaterialAlpha = false;
    public float alphaStart = 1.0f;
    public float alphaEnd = 0.5f;

    //private variables
    private bool _isFadingOut = false;
    private bool _alreadyFaded = false;
    private float _alphaDiff;
    private float _startTime;
    private Renderer _rend;
    private Color _fadeColor;
    private bool _pictureFrozen = true;

    //private variable accessors

    //methods
    private void Awake()
    {
        //make sure VRTK scripts are attached
        if (GetComponent<VRTK_InteractableObject>() == null)

        {
            Debug.LogError("Picture must have the VRTK_InteractableObject script attached to it");
            return;
        }

        //create event listeners for grabbing and releasing picture
        GetComponent<VRTK_InteractableObject>().InteractableObjectGrabbed += new InteractableObjectEventHandler(PictureGrabbed);
        GetComponent<VRTK_InteractableObject>().InteractableObjectUngrabbed += new InteractableObjectEventHandler(PictureReleased);
    }

    void Start () {
        //get renderer and set fade color
        //rendering mode on material must be set to "Fade"
        _rend = GetComponent<Renderer>();
        _fadeColor = _rend.material.color;

        if (!useMaterialAlpha)
        {
            _fadeColor.a = alphaStart;
        }

        _alphaDiff = alphaStart - alphaEnd;
	}
	
	void Update () {
        //manage picture physics and kinematics states
        if (_pictureFrozen)
        {
            gameObject.GetComponent<Rigidbody>().useGravity = false;
            gameObject.GetComponent<Rigidbody>().isKinematic = true;
        }

        if (!_pictureFrozen)
        {
            gameObject.GetComponent<Rigidbody>().useGravity = true;
            gameObject.GetComponent<Rigidbody>().isKinematic = false;
        }

        //fade picture out
        if (_isFadingOut && !_alreadyFaded)
        {
            float elapsedTime = Time.time - _startTime;
            if (elapsedTime <= fadeDuration)
            {
                float fadeProgress = elapsedTime / fadeDuration;
                float alphaChange = fadeProgress * _alphaDiff;
                _fadeColor.a = alphaStart - alphaChange;
                _rend.material.color = _fadeColor;
            }
            else
            {
                _fadeColor.a = alphaEnd;
                _rend.material.color = _fadeColor;
                _alreadyFaded = true;
            }
        }

        //fade picture in
        if(!_isFadingOut && _alreadyFaded)
        {
            float elapsedTime = Time.time - _startTime;
            if (elapsedTime <= fadeDuration)
            {
                float fadeProgress = elapsedTime / fadeDuration;
                float alphaChange = fadeProgress * _alphaDiff;
                _fadeColor.a = alphaEnd + alphaChange;
                _rend.material.color = _fadeColor;
            }
            else
            {
                _fadeColor.a = alphaStart;
                _rend.material.color = _fadeColor;
                _alreadyFaded = false;
            }
        }
	}

    private void PictureGrabbed(object sender, InteractableObjectEventArgs e)
    {
        //set gravity and kinematic for simulation realism
        _pictureFrozen = false;
        FadePictureOut();
    }

    private void PictureReleased(object sender, InteractableObjectEventArgs e)
    {
        FadePictureIn();
        RaycastHit hit;
        //raycast in direction of picture back
        if (Physics.Raycast(gameObject.transform.position, -transform.forward, out hit, 1.0f))
        {
            if(hit.collider.tag == "Wall")
            {
                //remove gravity and kinematic so picture hangs on wall
                _pictureFrozen = true;

                //align picture to wall
                //Quaternion.LookRotation() points the positive 'Z' side of the picture in a specified direction.
                Collider coll = hit.transform.gameObject.GetComponent<Collider>(); 
                Quaternion rotation = Quaternion.LookRotation(hit.normal);
                gameObject.transform.rotation = rotation;
                Vector3 closestBounds = coll.ClosestPointOnBounds(transform.localPosition);
                gameObject.transform.position = new Vector3(
                            closestBounds.x,
                            transform.position.y,
                            closestBounds.z
                        );
            }
            else
            {
                _pictureFrozen = false;
            }
        }
    }

    public void FadePictureOut()
    {
        _isFadingOut = true;
        _startTime = Time.time;
    }

    public void FadePictureIn()
    {
        _isFadingOut = false;
        _startTime = Time.time;
    }
}

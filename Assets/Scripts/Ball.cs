using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Ball : MonoBehaviour
{
    #region Old COde
    //public Rigidbody rb;
    //private XRGrabInteractable xr;
    //private Vector3 previousPosition;
    //private Quaternion previousRotation;
    //private float previousTime;

    //void Start()
    //{
    //    rb = GetComponent<Rigidbody>();
    //    xr = GetComponent<XRGrabInteractable>();
    //    xr.onSelectEntered.AddListener(OnGrab);
    //    xr.onSelectExited.AddListener(OnThrow);
    //}

    //void OnGrab(XRBaseInteractor interactor)
    //{
    //    previousPosition = interactor.transform.position;
    //    previousRotation = interactor.transform.rotation;
    //    previousTime = Time.time;

    //}

    //private void OnThrow(XRBaseInteractor interactor)
    //{

    //    Vector3 currentPosition = interactor.transform.position;
    //    Quaternion currentRotation = interactor.transform.rotation;
    //    float currentTime = Time.time;

    //    Vector3 velocity = (currentPosition - previousPosition) / (currentTime - previousTime);
    //    Vector3 angularVelocity = GetAngularVelocity(previousRotation, currentRotation, currentTime - previousTime);


    //    rb.velocity = velocity;
    //    rb.angularVelocity = angularVelocity;
    //}

    //private Vector3 GetAngularVelocity(Quaternion previousRotation, Quaternion currentRotation, float deltaTime)
    //{
    //    Quaternion deltaRotation = currentRotation * Quaternion.Inverse(previousRotation);
    //    Vector3 deltaEuler = deltaRotation.eulerAngles;

    //    if (deltaEuler.x > 180) deltaEuler.x -= 360;
    //    if (deltaEuler.y > 180) deltaEuler.y -= 360;
    //    if (deltaEuler.z > 180) deltaEuler.z -= 360;

    //    return deltaEuler / deltaTime * Mathf.Deg2Rad;
    //}

    //private void FixedUpdate()
    //{
    //    if (xr.isSelected)
    //    {
    //        previousPosition = xr.selectingInteractor.transform.position;
    //        previousRotation = xr.selectingInteractor.transform.rotation;
    //        previousTime = Time.time;
    //    }
    //    if(gameObject.transform.position.y < 0)
    //    {
    //        Destroy(gameObject);
    //    }
    //}
    #endregion
    public Rigidbody rb;
    private XRGrabInteractable xr;
    private Vector3 previousPosition;
    private Quaternion previousRotation;
    private float previousTime;

    // Dampening factors
    public float velocityDampening = 0.8f; 
    public float angularVelocityDampening = 0.5f;
    public float maxVelocity = 10f; 
    public float maxAngularVelocity = 10f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            Debug.LogError("Rigidbody not found!");
            return;
        }

        xr = GetComponent<XRGrabInteractable>();
        if (xr == null)
        {
            Debug.LogError("XRGrabInteractable not found!");
            return;
        }

        xr.onSelectEntered.AddListener(OnGrab);
        xr.onSelectExited.AddListener(OnThrow);

        //rb.mass = 1.0f; 
        //rb.drag = 0.1f; 
        //rb.angularDrag = 0.05f;
    }

    void OnGrab(XRBaseInteractor interactor)
    {
        if (interactor == null) return;

        previousPosition = interactor.transform.position;
        previousRotation = interactor.transform.rotation;
        previousTime = Time.time;
    }

    private void OnThrow(XRBaseInteractor interactor)
    {
        if (interactor == null) return;

        Vector3 currentPosition = interactor.transform.position;
        Quaternion currentRotation = interactor.transform.rotation;
        float currentTime = Time.time;

        Vector3 velocity = (currentPosition - previousPosition) / (currentTime - previousTime);
        Vector3 angularVelocity = GetAngularVelocity(previousRotation, currentRotation, currentTime - previousTime);

        rb.velocity = Vector3.ClampMagnitude(velocity * velocityDampening, maxVelocity);
        rb.angularVelocity = Vector3.ClampMagnitude(angularVelocity * angularVelocityDampening, maxAngularVelocity);
    }

    private Vector3 GetAngularVelocity(Quaternion previousRotation, Quaternion currentRotation, float deltaTime)
    {
        Quaternion deltaRotation = currentRotation * Quaternion.Inverse(previousRotation);
        Vector3 deltaEuler = deltaRotation.eulerAngles;

        if (deltaEuler.x > 180) deltaEuler.x -= 360;
        if (deltaEuler.y > 180) deltaEuler.y -= 360;
        if (deltaEuler.z > 180) deltaEuler.z -= 360;

        return deltaEuler / deltaTime * Mathf.Deg2Rad;
    }

    private void FixedUpdate()
    {
        if (xr != null && xr.isSelected && xr.selectingInteractor != null)
        {
            previousPosition = xr.selectingInteractor.transform.position;
            previousRotation = xr.selectingInteractor.transform.rotation;
            previousTime = Time.time;
        }

        if (transform.position.x > 7.25 || rb.velocity.magnitude > maxVelocity)
        {
            string ballColor = "";
            if (gameObject.name.Contains("Blue"))
            {
                ballColor = "Blue";
            }
            else if (gameObject.name.Contains("Purple"))
            {
                ballColor = "Purple";
            }
            else if (gameObject.name.Contains("Yellow"))
            {
                ballColor = "Yellow";
            }

            if (!string.IsNullOrEmpty(ballColor))
            {
                Game_Manager.Instance.Ball_Instantitaion(ballColor);
            }

            Destroy(gameObject);
        }
    }
}




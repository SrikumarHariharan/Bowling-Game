using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondaryCamera : MonoBehaviour
{
    public static SecondaryCamera cameraInstance { get; private set; }
    public GameObject secondaryCamera = null;
    void Awake()
    {
        if (cameraInstance != null && cameraInstance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            cameraInstance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("BowlingBall"))
        {
            if(secondaryCamera!= null && secondaryCamera.activeInHierarchy == false)
            {
                secondaryCamera.SetActive(true);
            }
        }
    }
    public void TurnOffCamera()
    {
        if(secondaryCamera.activeInHierarchy)
        {
            secondaryCamera.SetActive(false);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndPoint : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("BowlingBall"))
        {
            //Destroy(other.gameObject);
            if (Game_Manager.Instance != null)
            {
                Game_Manager.Instance.PinHit(true);
            }
            if (SecondaryCamera.cameraInstance != null)
            {
                SecondaryCamera.cameraInstance.TurnOffCamera();
            }
        }
    }
}

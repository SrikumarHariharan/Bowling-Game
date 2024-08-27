using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pins : MonoBehaviour
{
   // public float fallThreshold = 30.0f;
    private void OnTriggerEnter(Collider c)
    {
        if (c.gameObject.CompareTag("BowlingBall"))
        {
            Destroy(gameObject);
            //yes
            //if(IsPinFallen())
            {
                if (Game_Manager.Instance != null)
                {
                    Game_Manager.Instance.PinHit(false);
                }
                //Destroy(gameObject);
            }
        }
        
    }

    //private bool IsPinFallen()
    //{
    //    //float angle = Quaternion.Angle(transform.rotation, Quaternion.identity);
    //    //return angle > fallThreshold;
    //}
}

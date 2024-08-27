using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallInstantiate : MonoBehaviour
{
    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("BowlingBall"))
        {
            //if (gameObject.CompareTag("DestroyBall"))
            {
                Destroy(collision.gameObject);
            }
            if (Game_Manager.Instance != null)
            {
                if (collision.gameObject.name.Contains("Blue"))
                {
                    Game_Manager.Instance.Ball_Instantitaion("Blue");
                }
                else if (collision.gameObject.name.Contains("Purple"))
                {
                    Game_Manager.Instance.Ball_Instantitaion("Purple");
                }
                else if (collision.gameObject.name.Contains("Yellow"))
                {
                    Game_Manager.Instance.Ball_Instantitaion("Yellow");
                }
            }
        }
    }
}

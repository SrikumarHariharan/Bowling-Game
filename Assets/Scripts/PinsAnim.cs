using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinsAnim : MonoBehaviour
{
    public List<GameObject> PinList = new List<GameObject>(); 
   
    public void AnimateAndDestroy(GameObject obj)
    {
        foreach(var pin in PinList) 
        { 
            if(pin.gameObject.name == obj.name)
            {
                Destroy(pin.gameObject);
            }
        }
    }
    
}

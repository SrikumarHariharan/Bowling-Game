using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Panels : MonoBehaviour
{
  
    public float wobbleSpeed = 1f; // Speed of the wobble
    public float wobbleAmount = 5f; // Amount of wobble (degrees)
    public GameObject panels = null;
    public float audiodelay = 0f;
    public AudioSource Audio = null;
    
    private Quaternion initialRotation;
    
    void Start()
    {
        initialRotation = transform.rotation;
    }
    void Update()
    {
        
        float wobbleAngle = Mathf.Sin(Time.time * wobbleSpeed) * wobbleAmount;
        transform.rotation = initialRotation * Quaternion.Euler( wobbleAngle,0f, 0f);
        Audio.PlayDelayed(audiodelay);
        if(!(Audio.isPlaying))
        {
            panels.SetActive(true);
            this.gameObject.SetActive(false);
        }
        
    }
}





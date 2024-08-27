using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Wallpaperchange : MonoBehaviour
{
    public GameObject target  = null;
    private Keyboard keyboard = null;
    public GameObject Quad = null;
    public Material Macwallpaper2 = null;
    private AudioSource audioSource = null;
    public GameObject Cred = null;
    private Renderer keyRenderer = null;
    private Renderer Quadrendrer = null;
    public Material material = null;
    public List<GameObject> SetActive = new List<GameObject>();
   

    void Start()
    {
        keyboard = target.GetComponent<Keyboard>();
        keyRenderer = GetComponent<Renderer>();
        audioSource = GetComponent<AudioSource>();
        Quadrendrer = Quad.GetComponent<Renderer>();
    }
    public void ChangeWallpaper()
    {
        if (Quad != null && Macwallpaper2 != null)
        {
            Quadrendrer.material = Macwallpaper2;      
        }
    }
    public void ResetKey()
    {
        if(keyRenderer != null)
        {
            keyRenderer.material = material;

        }
    }
    void PlayAudio()
    {
        if (audioSource != null)
        {
            audioSource.Play();
        }
    }
    private void OnTriggerEnter(Collider c)
    {
        if(c.gameObject.CompareTag("Type"))
        {
            PlayAudio();
            ChangeWallpaper();
        }
        
    }
    private void OnTriggerExit(Collider other)
    {
        ResetKey();
        
        if (other.gameObject.CompareTag("Type"))
        {
            if (keyboard != null)
            {
                keyboard.enabled = true;
                Cred.SetActive(true);
            }
            foreach (GameObject gameobj in SetActive)
            {
                gameobj.SetActive(true);
            }
            this.enabled = false;
        }
    }
}

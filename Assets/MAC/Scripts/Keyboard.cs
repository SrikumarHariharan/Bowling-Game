using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Keyboard : MonoBehaviour
{
    public TextMeshPro PlayerNameOnMac = null;
    public string value = "";
    private AudioSource KeyClick = null;
    public GameObject WarningText = null;
    private Renderer keyRenderer = null;
    public Material material = null;
    public Material material2 = null;
    public GameObject Quad = null;
    public Material Macwallpaper1 = null;
    public Material Macwallpaper2 = null;
    private Renderer Quadrendrer = null;
    static bool Firsttime = false;
    static bool GameStartedAfterClick = true;


    void Start()
    {
        keyRenderer = GetComponent<Renderer>();
        KeyClick = GetComponent<AudioSource>();
        Quadrendrer = Quad.GetComponent<Renderer>();
    }
       
    public void HighlightKey()
    {
        if (keyRenderer != null)
        {
            keyRenderer.material = material;
        }
    }


    public void ResetKey()
    {
        if (keyRenderer != null)
        {
            keyRenderer.material = material2;
        }
    }

    void PlayAudio()
    {
        if (KeyClick != null)
        {
            KeyClick.Play();
        }
    }

    private void OnTriggerEnter(Collider c)
    {
        if (c.gameObject.CompareTag("Type"))
        {
            if (Firsttime && GameStartedAfterClick == true)
            {
                PlayAudio();
                HighlightKey();
                XRBaseController controller = c.GetComponent<XRBaseController>();
                if (controller != null)
                {
                    controller.SendHapticImpulse(0.5f, 0.1f); // (Amplitude, Duration)
                }
            }
            switch (gameObject.tag)
            {
                case "Enter":
                    if (Firsttime == false && GameStartedAfterClick == true)
                    {
                        FirstMove();
                    }
                    else
                    {
                        if(PlayerNameOnMac.text.Length != 0 && PlayerNameOnMac.text.Length <= 5)
                        {
                            ChangeWallpaper2();
                            //gameManager.GameStart(PlayerNameOnMac.text);
                            if (Game_Manager.Instance != null)
                            {
                                Game_Manager.Instance.GameStart(PlayerNameOnMac.text);
                            }
                            PlayerNameOnMac.text = "";
                            if (WarningText.activeInHierarchy)
                            {
                                WarningText.SetActive(false);

                            }
                            GameStartedAfterClick = false;
                        }
                    }
                    break;
                case "Space":
                    if (Firsttime && GameStartedAfterClick == true)
                    {
                        if (PlayerNameOnMac.text.Length <= 4)
                        {
                            PlayerNameOnMac.text = PlayerNameOnMac.text + " ";
                        }
                        else
                        {
                            if (!WarningText.activeInHierarchy)
                            {
                                WarningText.SetActive(false);
                            }
                        }
                        
                    }
                    break;
                case "Del":
                    if (Firsttime && GameStartedAfterClick == true)
                    {
                        if(PlayerNameOnMac.text.Length != 0)
                        {
                            PlayerNameOnMac.text = PlayerNameOnMac.text.Substring(0, PlayerNameOnMac.text.Length - 1);
                        }
                        if (PlayerNameOnMac.text.Length <= 4)
                        {
                            if(WarningText.activeInHierarchy)
                            {
                                WarningText.SetActive(false);
                            }

                        }
                    }
                    break;
                case "OtherKeys":
                    if (Firsttime && GameStartedAfterClick == true)
                    {
                        if (PlayerNameOnMac.text.Length <= 4)
                        {
                            PlayerNameOnMac.text = PlayerNameOnMac.text + value;
                        }
                        else
                        {
                            WarningText.SetActive(true);
                        }
                        if (PlayerNameOnMac.text == "")
                        {
                            PlayerNameOnMac.text = value;
                        }
                    }

                    break;
            }
            
            
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Type"))
        {
            ResetKey();
        }
    }
    void FirstMove()
    {
        Firsttime = true;
        PlayAudio();
        ResetKey();
        ChangeWallpaper1();
    }

    void ChangeWallpaper1()
    {
        if (Quad != null && Macwallpaper1 != null && Quadrendrer != null)
        {
                Quadrendrer.material = Macwallpaper2;
        }

    }
    void ChangeWallpaper2()
    {
        if (Quad != null && Macwallpaper1 != null && Quadrendrer != null)
        {
            Quadrendrer.material = Macwallpaper1;
        }

    }


}

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
    public TextMeshPro playerNameOnMac = null;
    public TextMeshPro passwordOnMac = null;
    public string value = "";
    private AudioSource keyClick = null;
    public GameObject WarningText = null;
    public GameObject WarningTextPassword = null;
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
        keyClick = GetComponent<AudioSource>();
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
        if (keyClick != null)
        {
            keyClick.Play();
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
                        if(playerNameOnMac.text.Length != 0 && playerNameOnMac.text.Length <= 5 && passwordOnMac.text == "password")
                        {
                            ChangeWallpaper2();
                            //gameManager.GameStart(playerNameOnMac.text);
                            if (Game_Manager.Instance != null)
                            {
                                Game_Manager.Instance.GameStart(playerNameOnMac.text);
                            }
                            playerNameOnMac.text = "";
                            if (WarningText.activeInHierarchy)
                            {
                                WarningText.SetActive(false);
                            }
                            if (WarningTextPassword.activeInHierarchy)
                            {
                                WarningTextPassword.SetActive(false);
                            }
                            GameStartedAfterClick = false;
                        }
                        else if(passwordOnMac.text != "password")
                        {
                            WarningTextPassword.SetActive(true);
                        }
                    }
                    break;
                case "Space":
                    if (Firsttime && GameStartedAfterClick == true)
                    {
                        if (playerNameOnMac.text.Length <= 4)
                        {
                            playerNameOnMac.text = playerNameOnMac.text + " ";
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
                        if(playerNameOnMac.text.Length != 0)
                        {
                            playerNameOnMac.text = playerNameOnMac.text.Substring(0, playerNameOnMac.text.Length - 1);
                        }
                        if (playerNameOnMac.text.Length <= 4)
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
                        if (playerNameOnMac.text.Length <= 4)
                        {
                            playerNameOnMac.text = playerNameOnMac.text + value;
                        }
                        else
                        {
                            WarningText.SetActive(true);
                        }
                        if (playerNameOnMac.text == "")
                        {
                            playerNameOnMac.text = value;
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

using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Game_Manager : MonoBehaviour
{
    public static Game_Manager Instance { get; private set; }
    int triesCounter = 1;
    int pinsHitCounter = 0;
    int Score;
    int temp;
    public TextMeshPro[] TriesUI;
    int TriesUIIndex = 0;
    public TextMeshPro[] SetTotalUI;
    int SetTotalUIIndex = 0;
    int Sets = 1;
    public TextMeshPro PlayerNameOnBoard = null;
    public TextMeshPro FinalScoreUI = null;
    public List<Transform> Pins_Spawn_Points = new List<Transform>();
    public GameObject Pin = null;
    public GameObject Initial_Bowlling_Balls = null;
    public GameObject ScoreBoardObjs = null;
    List<GameObject> AcvtivePinsInScene = new List<GameObject>();
    int finalScoreCounter = 0;
    public GameObject GameOver_Panel = null;

    //================== Ball Related Data ==========================
    public GameObject Blue_ball;
    public GameObject Yellow_ball;
    public GameObject Purple_ball;
    public Transform spawnPoint_blue;
    public Transform spawnPoint_Purple;
    public Transform spawnPoint_Yellow;
    public static int blue_Ball_Counter = 1;
    public static int yellow_Ball_Counter = 1;
    public static int purple_Ball_Counter = 1;



    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    public void GameStart(string PlayerName)
    {
        PlayerNameOnBoard.text = PlayerName;
        InstantiatNewPins();
        Initial_Bowlling_Balls.SetActive(true);
        ScoreBoardObjs.SetActive(true);
    }
    public void PinHit(bool EndTry)
    {
        if(triesCounter == 1)
        {
            HandleFirstTry(EndTry);
        }
        else
        {
            HandleSecondTry(EndTry);
        }
    }
    private void HandleFirstTry(bool EndTry)
    {
        if (Sets < 11)
        {
            if (!EndTry)
            {
                pinsHitCounter++;
            }
            if (EndTry)
            {
                int Try1Score = CalculateScore(pinsHitCounter);
                temp = Try1Score;
                var Try1UI = TriesUI[TriesUIIndex];
                Try1UI.text = Try1Score.ToString();
                TriesUIIndex++;
                var Try1SetTotalUI = SetTotalUI[SetTotalUIIndex];
                Try1SetTotalUI.text = Try1Score.ToString();
                UpdateFinalScore(Try1Score);
                pinsHitCounter = 0;
                triesCounter = 2;
                if (Try1Score == 30)
                {
                    InstantiatNewPins();
                }
            }
        }
    }
    private void HandleSecondTry(bool EndTry)
    {
        if (Sets < 11)
        {
            if (!EndTry)
            {
                pinsHitCounter++;
            }
            if (EndTry)
            {

                int Try2Score = CalculateScore(pinsHitCounter);
                var Try2UI = TriesUI[TriesUIIndex];
                Try2UI.text = Try2Score.ToString();
                TriesUIIndex++;
                var Try2SetTotalUI = SetTotalUI[SetTotalUIIndex];
                Try2SetTotalUI.text = (temp + Try2Score).ToString();
                SetTotalUIIndex++;
                UpdateFinalScore(Try2Score);
                temp = 0;
                pinsHitCounter = 0;
                triesCounter = 1;
                if (Sets == 10)
                {
                    GameOver();
                }
                else
                {
                    Sets++;
                    DestroyActivePins();
                    InstantiatNewPins();
                }
                
            }
            
        }
    }
    void GameOver()
    {
        DestroyActivePins();
        DestroyActiveBalls();
        GameOver_Panel.SetActive(true);
    }
    int CalculateScore(int PinsHitCounter)
    {
        //Score = 0;
        Score = PinsHitCounter*10;
        return Score;
    }
    void UpdateFinalScore(int FinalScore)
    {
        finalScoreCounter += FinalScore;
        FinalScoreUI.text = finalScoreCounter.ToString();
    }
    void InstantiatNewPins()
    {
        foreach (Transform swanpoint in Pins_Spawn_Points)
        {
            GameObject NewPin = Instantiate(Pin, swanpoint.position, swanpoint.rotation);
        }

    }
    void DestroyActivePins()
    {
        GameObject[]livePinsInScene = GameObject.FindGameObjectsWithTag("Pin");
        foreach(GameObject pins in livePinsInScene)
        {
            Destroy(pins);
        }
    }
    void DestroyActiveBalls()
    {
        GameObject[] livePinsInScene = GameObject.FindGameObjectsWithTag("BowlingBall");
        foreach (GameObject pins in livePinsInScene)
        {
            Destroy(pins);
        }
    }
    public void Ball_Instantitaion(string Ball_Colour)
    {
        if(Ball_Colour == "Blue")
        {
            blue_Ball_Counter--;
            if (blue_Ball_Counter < 2)
            {
                Instantiate(Blue_ball, spawnPoint_blue.position, spawnPoint_blue.rotation);
                blue_Ball_Counter++;
            }
        }
        else if(Ball_Colour == "Purple")
        {
            purple_Ball_Counter--;
            if (purple_Ball_Counter < 2)
            {
                Instantiate(Purple_ball, spawnPoint_Purple.position, spawnPoint_Purple.rotation);
                purple_Ball_Counter++;

            }
        }
        else if(Ball_Colour == "Yellow")
        {
            yellow_Ball_Counter--;
            if (yellow_Ball_Counter < 2)
            {
                Instantiate(Yellow_ball, spawnPoint_Yellow.position, spawnPoint_Yellow.rotation);
                yellow_Ball_Counter++;
            }
           
        }
    }
}


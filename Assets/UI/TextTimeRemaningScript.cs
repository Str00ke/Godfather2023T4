using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class TextTimeRemainingScript : MonoBehaviour
{
    public Text timeText;

    public GameObject gameplayManager;
    private GameplayManagerScript gameplayManagerScript;

    void Start()
    {   
        timeText.text = "Time : 00";  

        gameplayManagerScript = gameplayManager.GetComponent<GameplayManagerScript>();
    }

    void Update()
    {
        int timeRemaining = (int) gameplayManagerScript.GetTimeRemaining();

        if(timeRemaining < 100 && timeRemaining > 9){
            timeText.text = "Time : 0" + timeRemaining;
        }

        else if(timeRemaining > 100){
            timeText.text = "Time : " + timeRemaining;
        }
        
        else{
            timeText.text = "Time : 00" + Math.Max(timeRemaining, 0);
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ScoreGoal : MonoBehaviour
{
    public bool isPlayerA; //whether to show P1 or P2

    public Text scoreText;

    public GameObject gameplayManager;
    private GameplayManagerScript gameplayManagerScript;

    void Start()
    {   
        scoreText.text = "Goal : 0 / 0";  

        gameplayManagerScript = gameplayManager.GetComponent<GameplayManagerScript>();
    }

    void Update()
    {
        scoreText.text = "Goal : " + gameplayManagerScript.getScore().ToString() + " / " + gameplayManagerScript.getGoal();
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class TextCaughtScript : MonoBehaviour
{
    public Text scoreText;

    public GameObject gameplayManager;
    private GameplayManagerScript gameplayManagerScript;

    void Start()
    {   
        scoreText.text = "Cow Caught Goal: 0 / 0";  

        gameplayManagerScript = gameplayManager.GetComponent<GameplayManagerScript>();
    }

    void Update()
    {
        scoreText.text = "Cow Caught Goal:`\n" + gameplayManagerScript.GetScoreCaught().ToString() + " / " + gameplayManagerScript.GetGoalCaught();
    }
}

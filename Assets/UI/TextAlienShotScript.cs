using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class TextAlienShotScript : MonoBehaviour
{
    public Text scoreText;

    public GameObject gameplayManager;
    private GameplayManagerScript gameplayManagerScript;

    void Start()
    {   
        scoreText.text = "Alien Cow Show Goal: 0 / 0";  

        gameplayManagerScript = gameplayManager.GetComponent<GameplayManagerScript>();
    }

    void Update()
    {
        scoreText.text = "Alien Cow Shot Goal:`\n" + gameplayManagerScript.getScoreAlienShot().ToString() + " / " + gameplayManagerScript.getGoalAlienShot();
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class TextCowDiedScript : MonoBehaviour
{
    public Text scoreText;

    public GameObject gameplayManager;
    private GameplayManagerScript gameplayManagerScript;

    void Start()
    {   
        scoreText.text = "Cow Died: 0 / 0";  

        gameplayManagerScript = gameplayManager.GetComponent<GameplayManagerScript>();
    }

    void Update()
    {
        scoreText.text = "Cow Died:`\n" + gameplayManagerScript.GetCowDied().ToString() + " / " + gameplayManagerScript.GetDefeatCowDied();
    }
}

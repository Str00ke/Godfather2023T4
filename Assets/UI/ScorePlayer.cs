using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ScorePlayer : MonoBehaviour
{
    private int score;
    public bool isPlayerA; //whether to show P1 or P2

    public Text scoreText;
    String prefix; //"P1".tostring() or "P2".tostring()

    void Start()
    {
       
        if(isPlayerA){
            prefix = "P1 : ";
        }
        else{
            prefix = "P2 : ";
        }

        scoreText.text = "0";   
    }

    void Update()
    {
        scoreText.text = prefix + score.ToString();
    }

    public void AddPoint(int newScore){
        score += newScore;
    }

    public void ResetScore(){
        score = 0;
    }
}

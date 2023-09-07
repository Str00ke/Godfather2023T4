using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayManagerScript : MonoBehaviour
{
    public int goalCaught;
    public int goalAlienShot;
    public int defeatCowShot;

    public float timer; //runs out -> player looses

    private int scoreCaught = 0;

    private int scoreAlienShot = 0;

    private int ScoreCowDead = 0; 

    void Start()
    {
        
    }

    void Update()
    {
        if(scoreCaught >= goalCaught || scoreAlienShot >= goalAlienShot){
            //TODO: trigger win sequence

            Debug.Log("Player wins");
        }

        if(timer > 0){
            timer = Math.Max(timer - Time.deltaTime, 0);
        }
        if(timer == 0 || ScoreCowDead > defeatCowShot){
            //TODO: trigger loss sequence

            timer = -1;

            Debug.Log("Player loses");
        }
    }

    public int getGoalCaught(){
        return goalCaught;
    }

    public int getScoreCaught(){
        return scoreCaught;
    }
    
    
    public void AddPointCaught(int newScore){
        scoreCaught += newScore;
    }

    public void ResetScoreCaught(){
        scoreCaught = 0;
    }

    public int getGoalAlienShot(){
        return goalAlienShot;
    }

    public int getScoreAlienShot(){
        return scoreAlienShot;
    }
    
    
    public void AddPointAlienShot(int newScore){
        scoreAlienShot += newScore;
    }

    public void ResetScoreAlienShot(){
        scoreAlienShot = 0;
    }
}

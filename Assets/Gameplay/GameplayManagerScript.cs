using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayManagerScript : MonoBehaviour
{
    public int goal;

    private int score = 0;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public int getGoal(){
        return goal;
    }

    public int getScore(){
        return score;
    }
    
    
    public void AddPoint(int newScore){
        score += newScore;
    }

    public void ResetScore(){
        score = 0;
    }
}

using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameplayManagerScript : MonoBehaviour
{
    public GameObject prefabRegularCow;
    public GameObject prefabGoldenCow;
    public GameObject prefabAlienCow;

    public int goalCaught; //reach goal -> player wins
    public int goalAlienShot; //reach goal -> player wins
    public int defeatCowDied; //reach defeat -> player loses

    public float timer; //runs out -> player looses

    public float timerSpawn; //runs out -> spawns a cow

    private int scoreCaught = 0;

    private int scoreAlienShot = 0;

    private int ScoreCowDead = 0; 

    //cow spawning pos parameters
    public Vector2 boundUpLeft;
    public Vector2 boundDownRight;

    //cow spawning timer parameters
    public float sleepingBoundLow;
    public float sleepingBoundHigh;

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
        if(timer == 0 || ScoreCowDead > defeatCowDied){
            //TODO: trigger loss sequence

            timer = -1;

            Debug.Log("Player loses");
        }

        if(timerSpawn > 0){
            timerSpawn = Math.Max(timerSpawn - Time.deltaTime, 0);
        }
        
        if(timerSpawn == 0){
            int choice = Random.Range(0, 3);
            GameObject cowChoice = prefabRegularCow;

            switch(choice){
                case 0:
                    cowChoice = prefabRegularCow;
                    break;

                case 1:
                    cowChoice = prefabGoldenCow;
                    break;

                case 2:
                    cowChoice = prefabAlienCow;
                    break;

                default:
                    Debug.Log("Invalid cow choice to spawn from GameplayManager");
                    break;
            }

            Instantiate(cowChoice ,new Vector3(Random.Range(boundUpLeft.x, boundDownRight.x), Random.Range(boundUpLeft.y, boundDownRight.y)), Quaternion.identity);
        
            timerSpawn = Random.Range(sleepingBoundLow, sleepingBoundHigh);
        }

        if(Input.GetKeyDown(KeyCode.Z)){
            scoreCaught++;
        }

        if(Input.GetKeyDown(KeyCode.X)){
            scoreAlienShot++;
        }

        if(Input.GetKeyDown(KeyCode.C)){
            ScoreCowDead++;
        }
    }

    public float GetTimeRemaining(){
        return timer;
    }

    public int GetGoalCaught(){
        return goalCaught;
    }

    public int GetScoreCaught(){
        return scoreCaught;
    }
    
    
    public void AddPointCaught(int newScore){
        scoreCaught += newScore;
    }

    public void ResetScoreCaught(){
        scoreCaught = 0;
    }

    public int GetGoalAlienShot(){
        return goalAlienShot;
    }

    public int GetScoreAlienShot(){
        return scoreAlienShot;
    }
    
    
    public void AddPointAlienShot(int newScore){
        scoreAlienShot += newScore;
    }

    public void ResetScoreAlienShot(){
        scoreAlienShot = 0;
    }

    public int GetDefeatCowDied(){
        return defeatCowDied;
    }

    public int GetCowDied(){
        return ScoreCowDead;
    }
    
    public void AddCowDied(int newScore){
        ScoreCowDead += newScore;
    }

    public void ResetCowDied(){
        ScoreCowDead = 0;
    }
}

using System;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

public class CowScript : MonoBehaviour
{
    public GameObject self;
    public GameObject gameplayManager;
    protected GameplayManagerScript gameplayManagerScript;

    private Vector3 target = Vector3.zero; //cow follow target

    public float speed;

    //cow roaming parameters
    public Vector2 boundUpLeft;
    public Vector2 boundDownRight;

    private float sleepingTimer = -1; //when reached target, go to sleep for x amount of time
    private bool isSleeping;

    //cow sleeping parameters
    public float sleepingBoundLow;
    public float sleepingBoundHigh;

    void Start()
    {
        target = new Vector3(Random.Range(boundUpLeft.x, boundDownRight.x), Random.Range(boundUpLeft.y, boundDownRight.y));

        gameplayManagerScript = gameplayManager.GetComponent<GameplayManagerScript>();
    }

    void Update()
    {
        if(target == self.transform.localPosition && !isSleeping){
            sleepingTimer = Random.Range(sleepingBoundLow, sleepingBoundHigh);
            isSleeping = true;

            print("Target reached");
        }
        else{
            self.transform.localPosition = Vector3.MoveTowards(self.transform.localPosition, target, speed);
        }

        if (sleepingTimer == 0){
            sleepingTimer = -1;
            isSleeping = false;

            target = new Vector3(Random.Range(boundUpLeft.x, boundDownRight.x), Random.Range(boundUpLeft.y, boundDownRight.y));

            print("New Target : " + target.x + "," + target.y);
        }
        else if (sleepingTimer > 0){
            sleepingTimer = Math.Max(0, sleepingTimer - Time.deltaTime);
        }
    }

    public virtual void Shot(){
        gameplayManagerScript.AddPointAlienShot(1);
    }
}

using System;
using UnityEditor;
using UnityEngine;
using static Map;
using Random = UnityEngine.Random;

public class CowScript : MonoBehaviour
{
    public GameObject self;
    public GameObject cowChild;

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

    public bool IsPicked = false;

    public CowType cowType;

    void Start()
    {
        Transform t = null;
        int rand = Random.Range(0, Map.Instance.SpawnPoints.Count);
        t = Map.Instance.SpawnPoints[rand];
        target = t.position;//new Vector3(Random.Range(boundUpLeft.x, boundDownRight.x), Random.Range(boundUpLeft.y, boundDownRight.y));
    }

    void Update()
    {
        if (IsPicked) return;
        if(target == self.transform.localPosition && !isSleeping){
            sleepingTimer = Random.Range(sleepingBoundLow, sleepingBoundHigh);
            isSleeping = true;

            print("Target reached");
        }
        else{
            self.transform.localPosition = Vector3.MoveTowards(self.transform.localPosition, target, speed);
        }

        float s = Map.Instance.GetMinMaxScale(transform.position.y);
        transform.localScale = new Vector3(s, s, s);

        if (sleepingTimer == 0){
            sleepingTimer = -1;
            isSleeping = false;
            Transform t = null;

            int rand = Random.Range(0, Map.Instance.SpawnPoints.Count);
            t = Map.Instance.SpawnPoints[rand];
            target = t.position;//new Vector3(Random.Range(boundUpLeft.x, boundDownRight.x), Random.Range(boundUpLeft.y, boundDownRight.y));

            print("New Target : " + target.x + "," + target.y);
        }
        else if (sleepingTimer > 0){
            sleepingTimer = Math.Max(0, sleepingTimer - Time.deltaTime);
        }
    }

    public void SetTarget(bool enable)
    {
        if (enable)
            transform.localScale *= 2f; 
        else
            transform.localScale /= 2f;
    }


}

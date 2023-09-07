using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigameManager : MonoBehaviour
{
    enum SimonColor
    {
        R,
        G,
        B,
        Y
    }

    SimonColor[] pattern = new SimonColor[4];

    bool awaitPattern;
    int currAwait = 0;

    [SerializeField] float timeAwait;
    float currTime;
    void Start()
    {
        currTime = timeAwait;
    }

    void Update()
    {
        if (awaitPattern)
        {
            currTime -= Time.deltaTime;
            if (currTime <= 0.0f) Lose();

            if(Input.GetKeyDown(InputManager.Instance.QuadBlueButton) && pattern[currAwait] == SimonColor.B)
            {
                currAwait++;
            }
            else if (Input.GetKeyDown(InputManager.Instance.QuadRedButton) && pattern[currAwait] == SimonColor.R)
            {
                currAwait++;
            }
            else if (Input.GetKeyDown(InputManager.Instance.QuadGreenButton) && pattern[currAwait] == SimonColor.G)
            {
                currAwait++;
            }
            else if (Input.GetKeyDown(InputManager.Instance.QuadRYellowButton) && pattern[currAwait] == SimonColor.Y)
            {
                currAwait++;
            }

            if (currAwait >= pattern.Length) ValidatePattern();
        }
    }

    private void ValidatePattern()
    {
        
    }

    void Lose()
    {
        awaitPattern = false;
        currTime = timeAwait;
    }
    void GenPattern()
    {
        Array values = Enum.GetValues(typeof(SimonColor));
        for (int i = 0; i < 4; i++)
        {
            SimonColor randomCol= (SimonColor)values.GetValue(UnityEngine.Random.Range(0, 4));
            pattern[i] = randomCol;
        }
        Debug.Log(pattern[0] + "  " + pattern[1] + "  " + pattern[2] + "  " + pattern[3]);


        awaitPattern = true;
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CowScoreScript : MonoBehaviour
{
    public float lifetime; //effect: length of display
    public float speed; //effect: speed of text

    private float progress;

    private Text scoreText;

    public GameObject self;

    void Start()
    {
        scoreText = self.GetComponent<Text>();

        scoreText.CrossFadeAlpha(0, lifetime, false);
    }

    void Update()
    {
        progress += Time.deltaTime;

        if (progress > lifetime){
            Destroy(self);
        }

        self.transform.Translate(Vector3.up * speed);
    }

    //usially used right after instantiating
    public void setScore(int score){
        scoreText = self.GetComponent<Text>();

        scoreText.text = score.ToString();
    }

    public void setIsNegative(bool isNegative){
        if(isNegative){
            scoreText.CrossFadeColor(Color.red, 0, false, false);
        }
        else{
            scoreText.CrossFadeColor(Color.green, 0, false, false);
        }
    }
}
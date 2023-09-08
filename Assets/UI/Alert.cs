using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Alert : MonoBehaviour
{
    public Text AlertText;

    public int alertLoopCount = 3; //Times the text blinks
    private int alertLoopCurrent = 0; //Current loop in
    private float alertLoopProgress = 0; //Progress in current loop

    public float alertLoopLength; //lenght in time of alert loop in seconds
    
    public bool isLooping;

    void Start()
    {
        AlertText.CrossFadeAlpha(0,0,false);
        GetComponent<AudioSource>().Play();
    }

    public void Restart()
    {
        AlertText.CrossFadeAlpha(0, 0, false);
        GetComponent<AudioSource>().Play();
        alertLoopCurrent = 0;
        alertLoopProgress = 0;
        isLooping = true;
    }

    void Update()
    {
        if(isLooping){
            if(alertLoopProgress > alertLoopLength){
                alertLoopCurrent++;

                alertLoopProgress = 0;                
            }

            if(alertLoopProgress > alertLoopLength / 2){
                AlertText.CrossFadeAlpha(0, alertLoopLength / 2, false);
            }

            if(alertLoopCurrent >= alertLoopCount){
                isLooping = false;
                alertLoopCurrent = 0;
                alertLoopProgress = 0;
                GetComponent<AudioSource>().Stop();
                Invoke("StartLevel", 0.5f);
            }
 
            if(alertLoopProgress == 0 && isLooping){
                AlertText.CrossFadeAlpha(1, alertLoopLength / 2, false);
            }

            if(isLooping){
                alertLoopProgress += Time.deltaTime;
            }
        }
    }

    void StartLevel()
    {
        Map.Instance.InitLevel();
    }
}

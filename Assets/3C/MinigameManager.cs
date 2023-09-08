using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigameManager : MonoBehaviour
{
    public static MinigameManager Instance;

    private void Awake()
    {
        Instance = this;
    }

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

    [SerializeField] Transform startPoint;
    [SerializeField] Transform endPoint;
    [SerializeField] float flySpeed;
    [SerializeField] AnimationCurve flyAwayCurve;
    [SerializeField] GameObject ufo;
    [SerializeField] AudioSource soundSource;
    [SerializeField] AudioClip soundR;
    [SerializeField] AudioClip soundG;
    [SerializeField] AudioClip soundB;
    [SerializeField] AudioClip soundY;
    [SerializeField] AudioClip soundRestart;
    [SerializeField] AudioClip soundLose;
    [SerializeField] AudioClip soundWin;
    [SerializeField] AudioClip soundFlyIn;
    [SerializeField] AudioClip soundFlyAway;
    void Start()
    {
        currTime = timeAwait;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) GenGame();
        if (awaitPattern)
        {
            currTime -= Time.deltaTime;
            if (currTime <= 0.0f) Lose();

            if(Input.GetKeyDown(InputManager.Instance.QuadBlueButton))
            {
                soundSource.clip = soundB;
                soundSource.Play();
                if (pattern[currAwait] == SimonColor.B)
                    currAwait++;
                else RestartPattern();
            }
            else if (Input.GetKeyDown(InputManager.Instance.QuadRedButton))
            {
                soundSource.clip = soundR;
                soundSource.Play();
                if (pattern[currAwait] == SimonColor.R)
                    currAwait++;
                else RestartPattern();
            }
            else if (Input.GetKeyDown(InputManager.Instance.QuadGreenButton))
            {
                soundSource.clip = soundG;
                soundSource.Play();
                if (pattern[currAwait] == SimonColor.G)
                    currAwait++;
                else RestartPattern();
            }
            else if (Input.GetKeyDown(InputManager.Instance.QuadRYellowButton))
            {
                soundSource.clip = soundY;
                soundSource.Play();
                if (pattern[currAwait] == SimonColor.Y)
                    currAwait++;
                else RestartPattern();
            }

            if (currAwait >= pattern.Length) ValidatePattern();
        }
    }

    void RestartPattern()
    {
        currAwait = 0;
        soundSource.clip = soundRestart;
        soundSource.Play();
        Debug.Log("Restart");
    }

    private void ValidatePattern()
    {
        awaitPattern = false;
        currTime = timeAwait;
        soundSource.clip = soundWin;
        soundSource.Play();
        StartCoroutine(UfoFlyAway());
        Debug.Log("Win");
        Map.Instance.WinMiniGame();
    }

    public void Cancel()
    {
        awaitPattern = false;
        currTime = timeAwait;
        ufo.transform.position = startPoint.position;
    }
    void Lose()
    {
        awaitPattern = false;
        currTime = timeAwait;
        soundSource.clip = soundLose;
        soundSource.Play();
        StartCoroutine(UfoFlyAway());
        Debug.Log("Lose");
    }
    void GenPattern()
    {
        currAwait = 0;

        Array values = Enum.GetValues(typeof(SimonColor));
        for (int i = 0; i < 4; i++)
        {
            SimonColor randomCol= (SimonColor)values.GetValue(UnityEngine.Random.Range(0, 4));
            pattern[i] = randomCol;
        }
    }

    public void GenGame()
    {
        GenPattern();
        soundSource.clip = soundFlyIn;
        soundSource.Play();
        StartCoroutine(UfoFlyOnScreen());
    }

    IEnumerator UfoFlyOnScreen()
    {
        yield return new WaitForSeconds(0.5f);
        float t = 0.0f;
        float[] i = {0.1f, 0.25f, 0.50f, 0.75f };
        int currI = 0;
        while(t < 1.0f) 
        {
            Vector3 v = ufo.transform.position;
            v.x = Mathf.Lerp(startPoint.position.x, endPoint.position.x, t);
            ufo.transform.position = v;
            t += Time.deltaTime * flySpeed;
            if(currI <= 3 && t > i[currI])
            {
                Debug.Log(pattern[currI]);

                //PlaySoundandcolor
                switch (pattern[currI])
                {
                    case SimonColor.R:
                        ufo.GetComponent<SpriteRenderer>().color = new Color(255, 0, 0, 255);
                        soundSource.clip = soundR;
                        soundSource.Play();
                        break;
                    case SimonColor.G:
                        ufo.GetComponent<SpriteRenderer>().color = new Color(0, 255, 0, 255);
                        soundSource.clip = soundG;
                        soundSource.Play();
                        break;
                    case SimonColor.B:
                        ufo.GetComponent<SpriteRenderer>().color = new Color(0, 0, 255, 255);
                        soundSource.clip = soundB;
                        soundSource.Play();
                        break;
                    case SimonColor.Y:
                        ufo.GetComponent<SpriteRenderer>().color = new Color(255, 255, 0, 255);
                        soundSource.clip = soundY;
                        soundSource.Play();
                        break;
                    default:
                        ufo.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 255);
                        break;
                }
                currI++;
            }
            yield return null;
        }
        ufo.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 255);
        awaitPattern = true;
        yield return null;
    }

    IEnumerator UfoFlyAway()
    {
        float t = 0.0f;
        Vector3 s = ufo.transform.position;
        float eY = ufo.transform.position.y + 20f;
        Vector3 e = new Vector3(ufo.transform.position.x, eY, ufo.transform.position.z);
        while (t < 1.0f)
        {
            Vector3 v = ufo.transform.position;
            v.y = Mathf.Lerp(s.y, e.y, flyAwayCurve.Evaluate(t));
            ufo.transform.position = v;
            t += Time.deltaTime * flySpeed;
            yield return null;

        }
        Map.Instance.RestartMiniGameClock();
        yield return null;
    }
}

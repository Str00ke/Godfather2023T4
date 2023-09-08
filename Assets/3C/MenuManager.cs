using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    bool p1R = false;
    bool p2R = false;

    [SerializeField] Text p1Text; 
    [SerializeField] Text p2Text; 
    [SerializeField] AudioClip startGame;
    AudioSource source;
    void Start()
    {
        source = GetComponent<AudioSource>();
        p1Text.text = "Blue ready?";
        p2Text.text = "Red ready?";
    }

    void Update()
    {
        if(Input.GetKeyDown(InputManager.Instance.SidesButtonBlue) && !p1R)
        {
            p1R = true;
            source.Play();
            p1Text.text = "Ready!";
        }
        if (Input.GetKeyDown(InputManager.Instance.SidesButtonRed) && !p2R)
        {
            p2R = true;
            source.Play();
            p2Text.text = "Ready!";
        }

        if (p1R && p2R)
        {
            //source.clip = startGame;
            //source.Play();
            Invoke("StartGame", 0.5f);
        }
    }

    void StartGame()
    {
        SceneManager.LoadScene("Test3C");
    }
}

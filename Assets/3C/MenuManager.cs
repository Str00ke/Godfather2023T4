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

    void Start()
    {
        p1Text.text = "Blue ready?";
        p2Text.text = "Red ready?";
    }

    void Update()
    {
        if(Input.GetKeyDown(InputManager.Instance.SidesButtonBlue) && !p1R)
        {
            p1R = true;
            p1Text.text = "Ready!";
        }
        if (Input.GetKeyDown(InputManager.Instance.SidesButtonRed) && !p2R)
        {
            p2R = true;
            p2Text.text = "Ready!";
        }

        if (p1R && p2R) SceneManager.LoadScene("3C");
    }
}

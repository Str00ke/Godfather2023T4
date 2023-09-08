using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SixB : MonoBehaviour
{

    [SerializeField] AudioClip ac1;
    [SerializeField] AudioClip ac2;
    [SerializeField] AudioClip ac3;
    [SerializeField] AudioClip ac4;
    [SerializeField] AudioClip ac5;
    [SerializeField] AudioClip ac6;

    AudioSource aS;

    private void Awake()
    {
        aS = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (Input.GetKeyDown(InputManager.Instance.SixButtonTopLeft))
        {
            aS.clip = ac1;
            aS.Play();
        }
        else if (Input.GetKeyDown(InputManager.Instance.SixButtonTopMiddle))
        {
            aS.clip = ac2;
            aS.Play();
        }
        else if (Input.GetKeyDown(InputManager.Instance.SixButtonTopRight))
        {
            aS.clip = ac3;
            aS.Play();
        }
        else if (Input.GetKeyDown(InputManager.Instance.SixButtonBottomLeft))
        {
            aS.clip = ac4;
            aS.Play();
        }
        else if (Input.GetKeyDown(InputManager.Instance.SixButtonBottomMiddle))
        {
            aS.clip = ac5;
            aS.Play();
        }
        else if (Input.GetKeyDown(InputManager.Instance.SixButtonBottomRight))
        {
            aS.clip = ac6;
            aS.Play();
        }

    }
}

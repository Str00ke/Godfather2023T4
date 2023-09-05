using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]    Move p1;
    [SerializeField]    Move p2;
    [SerializeField]    float maxClamp;
    [SerializeField]    float minClamp;
    Transform tP1;
    Transform tP2;
    Camera cam;
    float maxClampCache;
    float minClampCache;
    float prevMag;

    private void Awake()
    {
        cam = GetComponent<Camera>();
        maxClampCache = maxClamp;
        minClampCache = minClamp;
    }

    void Start()
    {
        tP1 = p1.transform;
        tP2 = p2.transform;
    }


    void Update()
    {
        if (p1.IsMoving || p2.IsMoving) OnMove();
    }

    private void OnMove()
    {
        Vector3 v;
        Vector2 one = tP1.position;
        Vector2 two = tP2.position;
        //(x? + x?)/2, (y? + y?)/2
        v = new Vector3(((one.x + two.x) / 2), ((one.y + two.y) / 2), -10);
        cam.transform.position = v;
        float magV = Vector3.Magnitude(v);

        Debug.Log(magV);

        if(magV > prevMag && magV > minClampCache && magV > maxClampCache)
        {
            cam.orthographicSize += 0.01f;
        }
        else if (magV < prevMag && magV > minClampCache && magV > maxClampCache)
        {
            cam.orthographicSize -= 0.01f;
        }

        prevMag = magV;
        
    }
}

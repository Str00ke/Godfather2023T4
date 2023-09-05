using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    Camera cam;
    [SerializeField]
    Transform p1;
    [SerializeField]
    Transform p2;

    private void Awake()
    {
        cam = GetComponent<Camera>();
    }

    void Start()
    {
        
    }


    void Update()
    {
        Vector3 v;
        Vector2 one = p1.position;
        Vector2 two = p2.position;
        //(x? + x?)/2, (y? + y?)/2
        v = new Vector3(((one.x + two.x) / 2),  ((one.y + two.y) / 2), -10);
        cam.transform.position = v;
    }
}

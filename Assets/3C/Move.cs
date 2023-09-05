using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    float h, v;

    float z;

    Map map;

    public bool IsMoving { get; private set; }
    enum MoveState
    {
        WASD,
        NUM
    }
    [SerializeField]
    MoveState move;
    void Start()
    {
        z = transform.position.z;
        map = GameObject.Find("Map").GetComponent<Map>(); //Shit code, might fix it later
    }

    void Update()
    {
        if(move == MoveState.WASD)
        {
            h = Input.GetAxis("Horizontal");
            v = Input.GetAxis("Vertical");
        }
        else if (move == MoveState.NUM)
        {
            h = Input.GetAxis("Horizontal2");
            v = Input.GetAxis("Vertical2");
        }

        Vector3 fV = new Vector3(h, v, 0);
        float xPrime = transform.position.x + (h * Time.deltaTime * 5);
        float yPrime = transform.position.y + (v * Time.deltaTime * 5);
        if (xPrime >= map.MapLimits.max.x | xPrime <= map.MapLimits.min.x) fV.x = 0.0f;
        if (yPrime >= map.MapLimits.max.y | yPrime <= map.MapLimits.min.y) fV.y = 0.0f;
        transform.Translate(fV * Time.deltaTime * 5);
        IsMoving = fV.magnitude > 0.0f;
    }
}

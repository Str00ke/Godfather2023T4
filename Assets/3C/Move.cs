using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    float h, v;

    float z;

    
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


        transform.Translate(new Vector3(h, v, 0) * Time.deltaTime * 5);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    float h, v;

    float z;

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
        transform.Translate(fV * Time.deltaTime * 5);
        IsMoving = fV.magnitude > 0.0f;
    }
}

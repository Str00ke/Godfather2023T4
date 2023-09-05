using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grappling : MonoBehaviour
{

    [SerializeField] int jointsNbr;
    [SerializeField] float jointsDist;
    [SerializeField] GameObject jointsGo;

    List<GameObject> joints = new List<GameObject>();

    void Start()
    {
        for (int i = 0; i < jointsNbr; i++)
        {
            GameObject go = Instantiate(jointsGo, transform);
            go.transform.position = new Vector3(transform.position.x, transform.position.y - (jointsDist * (i + 1)), transform.position.z);
            if(i > 0)
            {
                go.transform.parent = joints[i - 1].transform;
                joints[i - 1].GetComponent<HingeJoint2D>().connectedBody = go.GetComponent<Rigidbody2D>();
            }
            joints.Add(go);

        }
        joints[0].GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
    }

    void Update()
    {
        
    }
}

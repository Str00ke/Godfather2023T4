using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    Bounds maxMapLimits;

    public Bounds MapLimits => maxMapLimits;

    private void Awake()
    {
        BoxCollider2D box = GetComponent<BoxCollider2D>();
        maxMapLimits = box.bounds;
    }
}

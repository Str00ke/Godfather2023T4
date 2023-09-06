using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    Bounds maxMapLimits;

    [SerializeField] float maxScaleClamp;
    [SerializeField] float minScaleClamp;

    float yMin;
    float yMax;

    public Bounds MapLimits => maxMapLimits;

    public float MaxScaleClamp => maxScaleClamp;
    public float MinScaleClamp => minScaleClamp;

    private void Awake()
    {
        BoxCollider2D box = GetComponent<BoxCollider2D>();
        maxMapLimits = box.bounds;
        yMin = maxMapLimits.min.y;
        yMax = maxMapLimits.max.y;
    }

    float CalcRange(float value)
    {
        var min = Mathf.Min(yMin, yMax);
        var max = Mathf.Max(yMin, yMax);

        var percentage = ((value - min) * 100) / (max - min);
        if (yMin > yMax) percentage = 100 - percentage;
        return percentage;
    }

    public float GetMinMaxScale(float yPosition)
    {
        return (Mathf.Lerp(minScaleClamp, maxScaleClamp, CalcRange(yPosition) / 100));
    }
}

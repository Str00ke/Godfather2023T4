using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    [SerializeField] GameObject target;
    Vector2 v;
    Map map;

    enum TargetState
    {
        WASD,
        NUM
    }
    [SerializeField] TargetState move;

    void Start()
    {
        map = Map.Instance;   
    }

    void Update()
    {
        if (move == TargetState.WASD)
        {
            v = InputManager.Instance.Player1CenterJoystick.AnalogVector;
            if (Input.GetKeyDown(InputManager.Instance.Player1CenterJoystick.topBtn)) ShootCol();
        }
        else if (move == TargetState.NUM)
        {
            v = InputManager.Instance.Player2CenterJoystick.AnalogVector;
            if (Input.GetKeyDown(InputManager.Instance.Player2CenterJoystick.topBtn)) ShootCol();
        }

        Vector3 fV = new Vector3(v.x, v.y, 0);
        float xPrime = transform.position.x + (v.x * Time.deltaTime * 5);
        float yPrime = transform.position.y + (v.y * Time.deltaTime * 5);
        if (xPrime >= map.MapLimits.max.x | xPrime <= map.MapLimits.min.x) fV.x = 0.0f;
        if (yPrime >= map.MapLimits.max.y | yPrime <= map.MapLimits.min.y) fV.y = 0.0f;
        transform.Translate(fV * Time.deltaTime * 5);

    }

    void ShootCol()
    {
        Collider2D[] tars = Physics2D.OverlapCircleAll(transform.position, 0.1f);
        if (tars.Length > 0)
        {
            foreach (var tar in tars)
                if (tar.TryGetComponent<CowScript>(out CowScript comp))
                {
                    comp.Shot();
                    return;
                }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, 0.1f);
    }
}

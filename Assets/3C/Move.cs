using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    float z;

    Map map;

    Vector2 v;

    bool isGrap = false;

    [SerializeField] GameObject Grap;
    [SerializeField] Transform GrapHolder;
    float grapHolderY;

    [SerializeField] float grapSpeed;
    [SerializeField] AnimationCurve grapDescendAnim;
    [SerializeField] AnimationCurve grapAscendAnim;
    public bool IsMoving { get; private set; }
    enum MoveState
    {
        WASD,
        NUM
    }
    [SerializeField]
    MoveState move;

    private void Awake()
    {
        
    }
    void Start()
    {
        z = transform.position.z;
        map = GameObject.Find("Map").GetComponent<Map>(); //Shit code, might fix it later
        grapHolderY = GrapHolder.transform.position.y;
    }

    void Update()
    {
        if(move == MoveState.WASD)
        {
            v = InputManager.Instance.Player1CenterJoystick.AnalogVector;
            if (Input.GetKeyDown(InputManager.Instance.Player1CenterJoystick.topBtn)) DescendGrap();

        }
        else if (move == MoveState.NUM)
        {
            v = InputManager.Instance.Player2CenterJoystick.AnalogVector;
            if (Input.GetKeyDown(InputManager.Instance.Player2CenterJoystick.topBtn)) DescendGrap();
        }

        Vector3 fV = new Vector3(v.x, v.y, 0);
        float xPrime = transform.position.x + (v.x * Time.deltaTime * 5);
        float yPrime = transform.position.y + (v.y * Time.deltaTime * 5);
        if (xPrime >= map.MapLimits.max.x | xPrime <= map.MapLimits.min.x) fV.x = 0.0f;
        if (yPrime >= map.MapLimits.max.y | yPrime <= map.MapLimits.min.y) fV.y = 0.0f;
        transform.Translate(fV * Time.deltaTime * 5);
        Vector3 grapVec = new Vector3(fV.x, 0.0f, 0.0f/*fV.y*/);
        GrapHolder.transform.Translate(grapVec * Time.deltaTime * 5);    
        IsMoving = fV.magnitude > 0.0f;

        float s = map.GetMinMaxScale(transform.position.y);
        transform.localScale = new Vector3(s, s, s);
        //Grap.transform.localScale = new Vector3(s, s, s);
    }

    void DescendGrap()
    {
        if (isGrap) return;
        StartCoroutine(DescendCor());
    }

    IEnumerator DescendCor()
    {
        isGrap = true;
        float t = 0.0f;
        float yStart = Grap.transform.position.y;
        float yTarget = transform.position.y;
        while (t < 1.0f)
        {
            float fY = Mathf.Lerp(yStart, yTarget, grapDescendAnim.Evaluate(t));
            Grap.transform.position = new Vector3(Grap.transform.position.x, fY, Grap.transform.position.z);
            t += Time.deltaTime * grapSpeed;
            yield return null;
        }
        yield return null;
        StartCoroutine(AscendCor(yStart));
    }

    void AscendGrap(float yTarget)
    {

    }

    IEnumerator AscendCor(float yTarget)
    {
        float t = 0.0f;
        float yStart = transform.position.y;
        while (t < 1.0f)
        {
            float fY = Mathf.Lerp(yStart, yTarget, grapAscendAnim.Evaluate(t));
            Grap.transform.position = new Vector3(Grap.transform.position.x, fY, Grap.transform.position.z);
            t += Time.deltaTime * grapSpeed;
            yield return null;
        }
        isGrap = false;
        yield return null;
    }

}

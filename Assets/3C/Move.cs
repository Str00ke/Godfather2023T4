using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class Move : MonoBehaviour
{
    float z;

    Map map;

    Vector2 v;

    bool isGrap = false;

    bool isLocked = false;
    float lockTimer = 5.0f;

    [SerializeField] GameObject Grap;
    [SerializeField] Transform GrapHolder;
    [SerializeField] RopeControllerSimple ropeController;
    float grapHolderY;

    [SerializeField] float grapSpeed;
    [SerializeField] AnimationCurve grapDescendAnim;
    [SerializeField] AnimationCurve grapAscendAnim;

    [SerializeField] Sprite openClaw;
    [SerializeField] Sprite closedClaw;
    [SerializeField] Sprite stuckClaw;

    SpriteRenderer sr;

    GameObject targetCow;
    GameObject pickedCow;

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
        sr = Grap.transform.GetChild(0).GetComponent<SpriteRenderer>();
        sr.sprite = openClaw;

    }
    void Start()
    {
        z = transform.position.z;
        map = GameObject.Find("Map").GetComponent<Map>(); //Shit code, might fix it later
        grapHolderY = GrapHolder.transform.position.y;
    }

    void Update()
    {
        if (isLocked)
        {
            lockTimer -= Time.deltaTime;
            Debug.Log(lockTimer);

            if (lockTimer <= 0.0f)
            {
                Debug.Log("Here");

                isLocked = false;
                sr.sprite = closedClaw;
                ropeController.SetSimu(true);
                StartCoroutine(AscendCor(Grap.transform.position.y));
            }
            return;
        }

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
        Vector3 grapVec = new Vector3(fV.x, 0.0f, fV.y);
        GrapHolder.transform.Translate(grapVec * Time.deltaTime * 5);    
        IsMoving = fV.magnitude > 0.0f;
        float s = map.GetMinMaxScale(transform.position.y);
        transform.localScale = new Vector3(s, s, s);
        //Grap.transform.localScale = new Vector3(s, s, s);

        //         var cows = Physics2D.OverlapCircleAll(transform.position, 0.25f);
        //         if(targetCow != null)
        //         {
        //             bool check = false;
        //             foreach (var cow in cows)
        //             {
        //                 if (cow.gameObject == targetCow.gameObject)
        //                 {
        //                     check = true;
        //                     break;
        //                 }
        //             }
        //             if (!check)
        //             {
        //                 targetCow.GetComponent<CowScript>().SetTarget(false);
        //                 targetCow = null;
        //             }
        //             else return;
        //         }
        //        
        //         foreach (var cow in cows)
        //         {
        //             if (cow.TryGetComponent<CowScript>(out CowScript comp))
        //             {
        //                 targetCow = cow.gameObject;
        //                 comp.SetTarget(true);
        //             }
        //         }

    }

    public void Lock()
    {
        isLocked = true;
        lockTimer = map.LockGrapTime;
        sr.sprite = stuckClaw;
        ropeController.SetSimu(false);
        Destroy(pickedCow);
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
        sr.sprite = closedClaw;
        Collider2D[] Cows = Physics2D.OverlapCircleAll(transform.position, 0.25f);
        if (Cows.Length > 0)
        {
            foreach (var cow in Cows)
                if (cow.TryGetComponent<CowScript>(out CowScript comp))
                {
                    pickedCow = cow.gameObject;
                    comp.IsPicked = true;
                }
        }
        if (pickedCow != null)
            ComputeCow(yStart);
        else
            StartCoroutine(AscendCor(yStart));
    }



    void ComputeCow(float yTarget)
    {
        CowScript cow = pickedCow.GetComponent<CowScript>();
        if (cow.cowType != CowType.TOXIQUE)
            StartCoroutine(AscendCor(yTarget));
        else
            Lock();

        map.AddCowToGoals(cow.cowType);
    }

    IEnumerator AscendCor(float yTarget)
    {
        float t = 0.0f;
        yTarget += 2f;
        float yStart = transform.position.y;
        while (t < 1.0f)
        {
            float fY = Mathf.Lerp(yStart, yTarget, grapAscendAnim.Evaluate(t));
            Grap.transform.position = new Vector3(Grap.transform.position.x, fY, Grap.transform.position.z);
            t += Time.deltaTime * grapSpeed;
            if (pickedCow != null)
                pickedCow.transform.position = Grap.transform.position;
            yield return null;
        }
        isGrap = false;
        sr.sprite = openClaw;
        if (pickedCow != null)
        {
            Destroy(pickedCow);
            pickedCow = null;
        }
        yield return null;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, 0.25f);
    }

}

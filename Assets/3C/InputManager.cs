using UnityEngine;

public class InputManager : MonoBehaviour
{
    static public InputManager Instance { get; private set; }


    private void Awake() => Instance = this;


    [System.Serializable]
   public struct Joystick
   {
        [HideInInspector] public Vector2 analog;
        public string analogHor;
        public string analogVert;
        public KeyCode topBtn;
        public KeyCode sideBtn;

        public Vector2 AnalogVector => new Vector2(Input.GetAxis(analogHor), Input.GetAxis(analogVert));

   }

    [Header("Joysticks")]
    [Header("Left Player")]
    [SerializeField] Joystick J1CenterJS;
    [SerializeField] Joystick J1SideJS;

    [HideInInspector] public Joystick Player1CenterJoystick => J1CenterJS;
    [HideInInspector] public Joystick Player1SideJoystick => J1SideJS;

    [Space(10)]

    [Header("Right Player")]
    [SerializeField] Joystick J2CenterJS;
    [SerializeField] Joystick J2SideJS;

    [HideInInspector] public Joystick Player2CenterJoystick => J2CenterJS;
    [HideInInspector] public Joystick Player2SideJoystick => J2SideJS;

    [Space(20)]

    [Header("Center Button")]
    [SerializeField] KeyCode CenterBtn;

    [HideInInspector] public KeyCode CenterButton => CenterBtn;


    [Space(20)]

    [Header("Quad color Left buttons")]
    [SerializeField] KeyCode quadBtnR;
    [SerializeField] KeyCode quadBtnG;
    [SerializeField] KeyCode quadBtnB;
    [SerializeField] KeyCode quadBtnY;

    [HideInInspector] public KeyCode QuadRedButton => quadBtnR;
    [HideInInspector] public KeyCode QuadGreenButton => quadBtnG;
    [HideInInspector] public KeyCode QuadBlueButton => quadBtnB;
    [HideInInspector] public KeyCode QuadRYellowButton => quadBtnY;


    [Space(20)]

    [Header("Six Middle Buttons")]
    [SerializeField] KeyCode sixBtnTL;
    [SerializeField] KeyCode sixBtnTM;
    [SerializeField] KeyCode sixBtnTR;
    [SerializeField] KeyCode sixBtnBL;
    [SerializeField] KeyCode sixBtnBM;
    [SerializeField] KeyCode sixBtnBR;

    [HideInInspector] public KeyCode SixButtonTopLeft => sixBtnTL;
    [HideInInspector] public KeyCode SixButtonTopMiddle => sixBtnTM;
    [HideInInspector] public KeyCode SixButtonTopRight => sixBtnTR;
    [HideInInspector] public KeyCode SixButtonBottomLeft => sixBtnBL;
    [HideInInspector] public KeyCode SixButtonBottomMiddle => sixBtnBM;
    [HideInInspector] public KeyCode SixButtonBottomRight => sixBtnBR;


    [Space(20)]

    [Header("Two Right Buttons")]
    [SerializeField] KeyCode sidesBtnRed;
    [SerializeField] KeyCode sidesBtnToBlue;

    [HideInInspector] public KeyCode SidesButtonRed => sidesBtnRed;
    [HideInInspector] public KeyCode SidesButtonBlue => sidesBtnToBlue;

}

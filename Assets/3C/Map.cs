using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms;
using UnityEngine.UI;

public class Map : MonoBehaviour
{
    static public Map Instance { get; private set; }

    Bounds maxMapLimits;

    [SerializeField] float maxScaleClamp;
    [SerializeField] float minScaleClamp;
    [SerializeField] float lockGrapTime;
    [SerializeField] List<LevelData> levels = new List<LevelData>();
    [SerializeField] List<Transform> spawnPoints = new();
    public List<Transform> SpawnPoints => spawnPoints;
    public float LockGrapTime => lockGrapTime;

    float yMin;
    float yMax;

    public Bounds MapLimits => maxMapLimits;

    public float MaxScaleClamp => maxScaleClamp;
    public float MinScaleClamp => minScaleClamp;

    float timeBeforeNextMinigame = 0.0f;
    [SerializeField] float timeBeforeNextMinigameMin;
    [SerializeField] float timeBeforeNextMinigameMax;
    bool isPlayMiniGame = false;

    int currLevel = 0;
    LevelData currLevelData;
    List<float> currLevelTimersCache = new List<float>();
    List<float> currLevelTimers = new List<float>();
    List<int> currLevelCowNbr = new List<int>();
    List<GameObject> spawnedCows = new List<GameObject>();

    [System.Serializable]
    public struct CowSprite
    {
        public CowType type;
        public Sprite sprite;
    }
    [SerializeField] List<CowSprite> cowSprites = new List<CowSprite>();
    [SerializeField] GameObject cowPrefab;

    public UnityEvent OnAlienShot;
    public UnityEvent OnNormalShot;
    public UnityEvent OnGoldenShot;
    public UnityEvent OnToxicShot;
    public UnityEvent OnMaladeShot;

    [System.Serializable]
    public struct CowUI
    {
        public Image img;
        public Text txt;
    }
    [SerializeField] CowUI NormalUI;
    [SerializeField] CowUI ToxicUI;
    [SerializeField] CowUI SickUI;
    [SerializeField] CowUI GoldUI;
    [SerializeField] CowUI AlienUI;

    [SerializeField] Text timeTxt;
    List<int> currC = new();
    List<int> maxC = new();

    bool levelInProgress = false;
    public void InitLevel()
    {
        NormalUI.txt.text = "0\n/\n0";
        ToxicUI.txt.text = "0\n/\n0";
        SickUI.txt.text = "0\n/\n0";
        GoldUI.txt.text = "0\n/\n0";
        AlienUI.txt.text = "0\n/\n0";
        for (int i = 0; i < levels.Count; i++)
        {
            if (levels[i].levelNbr == currLevel)
            {
                currLevelData = (LevelData)ScriptableObject.CreateInstance("LevelData");
                currLevelData.spawnData = levels[i].spawnData;
                currLevelData.goalData = levels[i].goalData;
                currLevelData.levelNbr = levels[i].levelNbr;
                currLevelData.totalTimeInSeconds = levels[i].totalTimeInSeconds;
                float time = currLevelData.totalTimeInSeconds;
                timeTxt.text = time.ToString();
                for (int j = 0; j < currLevelData.spawnData.Count; j++)
                {
                    currLevelTimersCache.Add(time / currLevelData.spawnData[j].tot);
                    currLevelTimers.Add(time / currLevelData.spawnData[j].tot);
                    currLevelCowNbr.Add(currLevelData.spawnData[j].tot);
                }

                for (int j = 0; j < currLevelData.goalData.Count; j++)
                {
                    switch (currLevelData.goalData[j].type)
                    {
                        case CowType.NORMAL:
                            NormalUI.txt.text = "0\n/\n" + currLevelData.goalData[j].tot;
                            break;
                        case CowType.MALADE:
                            SickUI.txt.text = "0\n/\n" + currLevelData.goalData[j].tot;
                            break;
                        case CowType.TOXIQUE:
                            ToxicUI.txt.text = "0\n/\n" + currLevelData.goalData[j].tot;
                            break;
                        case CowType.OR:
                            GoldUI.txt.text = "0\n/\n" + currLevelData.goalData[j].tot;
                            break;
                        case CowType.ALIEN:
                            AlienUI.txt.text = "0\n/\n" + currLevelData.goalData[j].tot;
                            break;

                    }
                    currC.Add(0);
                    maxC.Add(currLevelData.goalData[j].tot);
                }
                break;
            }
        }
        GetComponent<AudioSource>().Play();
        levelInProgress = true;
    }

    private void Awake()
    {
        Instance = this;
        EdgeCollider2D box = GetComponent<EdgeCollider2D>();
        maxMapLimits = box.bounds;
        yMin = maxMapLimits.min.y;
        yMax = maxMapLimits.max.y;
        timeBeforeNextMinigame = Random.Range(timeBeforeNextMinigameMin, timeBeforeNextMinigameMax);
    }

    private void Start()
    {
       // InitLevel();
    }

    private void Update()
    {
        if (!levelInProgress) return;
        if (!isPlayMiniGame)
        {
            timeBeforeNextMinigame -= Time.deltaTime;
            if (timeBeforeNextMinigame <= 0)
            {
                MinigameManager.Instance.GenGame();
                isPlayMiniGame = true;
            }

        }
        for (int i = 0; i < currLevelTimers.Count; i++)
        {
            if (currLevelCowNbr[i] > 0)
            {
                currLevelTimers[i] -= Time.deltaTime;
                if (currLevelTimers[i] <= 0.0f)
                {
                    currLevelTimers[i] = currLevelTimersCache[i];
                    SpawnCow(currLevelData.spawnData[i].type);
                    currLevelCowNbr[i] -= 1;
                }
            }
            
        }
        currLevelData.totalTimeInSeconds -= Time.deltaTime;
        timeTxt.text = ((int)currLevelData.totalTimeInSeconds).ToString();
        if (currLevelData.totalTimeInSeconds <= 0.0f) LevelEndRunOutTime();

    }

    public void RestartMiniGameClock()
    {
        timeBeforeNextMinigame = Random.Range(timeBeforeNextMinigameMin, timeBeforeNextMinigameMax);
        isPlayMiniGame = false;
    }
    void SpawnCow(CowType type)
    {
        Sprite spr = null;
        Transform t = null;
        foreach(var sprite in cowSprites)
        {
            if (sprite.type == type) spr = sprite.sprite;
        }
        int rand = Random.Range(0, spawnPoints.Count);
        t = spawnPoints[rand];
        GameObject cowGO = Instantiate(cowPrefab, t.position, Quaternion.identity);
        cowGO.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = spr;
        cowGO.GetComponent<CowScript>().cowType = type;
        spawnedCows.Add(cowGO);
    }

    void LevelEndRunOutTime()
    {
        SceneManager.LoadScene(0);
    }

    void LevelEndWin()
    {
        EraseLvl();
        levelInProgress = false;
        if(currLevel < 10)
            currLevel++;
        currC.Clear();
        maxC.Clear();
        currLevelTimersCache.Clear();
        currLevelTimers.Clear();
        currLevelCowNbr.Clear();
        spawnedCows.Clear();
        GetComponent<AudioSource>().Stop();
        FindObjectOfType<Alert>().Restart();
        FindObjectOfType<MinigameManager>().Cancel();
        Destroy(currLevelData);
    }

    void EraseLvl()
    {
        foreach(GameObject cow in spawnedCows) Destroy(cow);
    }

    public void AddCowToGoals(CowType type)
    {
        for (int i = 0; i < currLevelData.goalData.Count; i++)
        {
            if (currLevelData.goalData[i].type == type)
            {
                /*
                 List<MyStruct> list = {…};  
                 MyStruct ms = list[0];  
                 ms.Name = "MyStruct42";  
                 list[0] = ms;  
                 */
                int curr, tot;
                tot = maxC[i];
                currC[i]++;
                curr = currC[i];
                string txt = curr + "\n/\n" + tot;
                switch (type)
                {
                    case CowType.NORMAL:
                        NormalUI.txt.text = txt;
                        break;
                    case CowType.MALADE:
                        SickUI.txt.text = txt;
                        break;
                    case CowType.TOXIQUE:
                        ToxicUI.txt.text = txt;
                        break;
                    case CowType.OR:
                        GoldUI.txt.text = txt;
                        break;
                    case CowType.ALIEN:
                        AlienUI.txt.text = txt;
                        break;

                }
            }
        }

        

        bool check = true;
        for (int i = 0; i < currLevelData.goalData.Count; i++)
        {
            if (currC[i] < maxC[i]) check = false;
        }

        if (check) LevelEndWin();
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

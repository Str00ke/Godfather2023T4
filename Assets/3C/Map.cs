using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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


    public void InitLevel()
    {
        for (int i = 0; i < levels.Count; i++)
        {
            if (levels[i].levelNbr == currLevel)
            {
                currLevelData = new LevelData();
                currLevelData.spawnData = levels[i].spawnData;
                currLevelData.goalData = levels[i].goalData;
                currLevelData.levelNbr = levels[i].levelNbr;
                currLevelData.totalTimeInSeconds = levels[i].totalTimeInSeconds;
                float time = currLevelData.totalTimeInSeconds;

                for (int j = 0; j < currLevelData.spawnData.Count; j++)
                {
                    currLevelTimersCache.Add(time / currLevelData.spawnData[j].tot);
                    currLevelTimers.Add(time / currLevelData.spawnData[j].tot);
                    currLevelCowNbr.Add(currLevelData.spawnData[j].tot);
                }
                break;
            }
        }
    }

    private void Awake()
    {
        Instance = this;
        EdgeCollider2D box = GetComponent<EdgeCollider2D>();
        maxMapLimits = box.bounds;
        yMin = maxMapLimits.min.y;
        yMax = maxMapLimits.max.y;
    }

    private void Start()
    {
        InitLevel();
    }

    private void Update()
    {
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
        if (currLevelData.totalTimeInSeconds <= 0.0f) LevelEndRunOutTime();

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
    }

    void LevelEndWin()
    {
        EraseLvl();
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
                LevelData.CowData cd = currLevelData.goalData[i];
                cd.tot -= 1;
                currLevelData.goalData[i] = cd;
            }
        }

        bool check = true;
        for (int i = 0; i < currLevelData.goalData.Count; i++)
        {
            if (currLevelData.goalData[i].tot > 0) check = false;
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

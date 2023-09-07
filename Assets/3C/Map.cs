using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    Bounds maxMapLimits;

    [SerializeField] float maxScaleClamp;
    [SerializeField] float minScaleClamp;
    [SerializeField] float lockGrapTime;
    [SerializeField] List<LevelData> levels;

    public float LockGrapTime => lockGrapTime;

    float yMin;
    float yMax;

    public Bounds MapLimits => maxMapLimits;

    public float MaxScaleClamp => maxScaleClamp;
    public float MinScaleClamp => minScaleClamp;

    int currLevel = 0;
    LevelData currLevelData;
    List<float> currLevelTimersCache;
    List<float> currLevelTimers;
    List<GameObject> spawnedCows;

    public void InitLevel()
    {
        for (int i = 0; i < levels.Count; i++)
        {
            if (levels[i].levelNbr == currLevel)
            {
                currLevelData = levels[i];
                float time = currLevelData.totalTimeInSeconds;
                for (int j = 0; j < currLevelData.spawnData.Count; j++)
                {
                    currLevelTimersCache.Add(currLevelData.spawnData[i].tot / time);
                    currLevelTimers.Add(currLevelData.spawnData[i].tot / time);
                }
                break;
            }
        }
    }

    private void Awake()
    {
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
            currLevelTimers[i] -= Time.deltaTime;
            if(currLevelTimers[i] <= 0.0f)
            {
                currLevelTimers[i] = currLevelTimersCache[i];
                SpawnCow(currLevelData.spawnData[i].type);
            }
        }
        currLevelData.totalTimeInSeconds -= Time.deltaTime;
        if (currLevelData.totalTimeInSeconds <= 0.0f) LevelEndRunOutTime();

    }

    void SpawnCow(CowType type)
    {
        spawnedCows.Add(null);
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

    void AddCowToGoals(CowType type)
    {
        for (int i = 0; i < currLevelData.goalData.Count; i++)
        {
            if (currLevelData.goalData[i].type == type)
            {
                var curr = currLevelData.goalData[i];
                curr.tot -= 1;
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

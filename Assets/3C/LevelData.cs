using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "LevelData")]
public class LevelData : ScriptableObject
{
    [System.Serializable]
    public struct CowData
    {
        public CowType type;
        public int tot;
    }
    public int levelNbr;
    public float totalTimeInSeconds;
    public List<CowData> spawnData;
    public List<CowData> goalData;
        
}

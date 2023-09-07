using System;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

public class AlienCowScript : CowScript
{
    public override void Shot(){
        gameplayManagerScript.AddPointAlienShot(1);
    }
}

using System;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

public class RegularCowScript : CowScript
{

    public override void Shot(){
        gameplayManagerScript.AddPointAlienShot(1);
    }
}

using System;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

public class GoldenCowScript : CowScript
{

    public override void Shot(){
        gameplayManagerScript.AddPointCaught(3);
    }
}

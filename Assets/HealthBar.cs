using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public int healthDefault; //Used to know what max health is for rendering purposes
    public int health;

    public Image healthBarRender;

    void Start()
    {
        health = healthDefault;
    }

    void Update()
    {
        healthBarRender.color = new Color(1 - (float)health / (float)healthDefault,health / (float)healthDefault, 0); //blend green to red (temp)

        healthBarRender.rectTransform.transform.localScale = new Vector3((float)health / (float)healthDefault, 1, 1); //set bar size
    }

    public void resetHealth(){
        health = healthDefault;
    }

    public void addHealth(int newHealth){
        health += newHealth;
    }
}

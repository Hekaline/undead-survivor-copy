using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    public enum InfoType
    {
        Exp,
        Level,
        Kill,
        Time,
        Health,
        
    }

    public InfoType type;
    private Text myText;
    private Slider mySlider;
    private GameManager gm;

    private void Awake()
    {
        myText = GetComponent<Text>();
        mySlider = GetComponent<Slider>();
    }

    private void Start()
    {
        gm = GameManager.instance;
    }

    private void LateUpdate()
    {
        switch (type)
        {
            case InfoType.Exp:
                float currExp = gm.exp;
                float maxExp = gm.nextExp[gm.level];
                mySlider.value = currExp / maxExp;
                break;
            case InfoType.Health:
                float currHealth = gm.health;
                float maxHealth = gm.maxHealth;
                mySlider.value = currHealth / maxHealth;
                break;
            case InfoType.Kill:
                myText.text = $"{gm.kill}";
                break;
            case InfoType.Level:
                myText.text = $"Lv. {gm.level}";
                break;
            case InfoType.Time:
                float remainingTime = gm.maxGameTime - gm.gameTime;
                int min = Mathf.FloorToInt(remainingTime / 60);
                int sec = Mathf.FloorToInt(remainingTime % 60);
                myText.text = string.Format("{0:D2}:{1:D2}", min, sec);
                break;
        }
    }
}

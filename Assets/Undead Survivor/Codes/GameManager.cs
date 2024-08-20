using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    
    [Header("# Game Control")]
    
    public float gameTime;
    public float maxGameTime = 20f;

    [Header("# Player Info")] 
    
    public int health;
    public int maxHealth = 100;
    public int level;
    public int kill;
    public int exp;
    public int[] nextExp = { 10, 30, 60, 100, 150, 210, 360, 450, 600 };

    [Header("# GameObject")]
    
    public PoolManager poolMgr;
    public Player player;

    private void Awake()
    {
        instance = this;
        
        if (poolMgr == null)
            poolMgr = FindObjectOfType<PoolManager>(); 
    }

    private void Start()
    {
        health = maxHealth;
    }

    private void Update()
    {
        gameTime += Time.deltaTime;

        if (gameTime > maxGameTime)
        {
            gameTime = maxGameTime;
        }
    }

    public void GetExp()
    {
        exp += 1;

        if (exp == nextExp[level])
        {
            level += 1;
            exp = 0;
        }
    }
}

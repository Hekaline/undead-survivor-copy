using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public float gameTime;
    public float maxGameTime = 20f;

    public int level;
    public int kill;
    public int exp;
    public int[] nextExp = { 10, 30, 60, 100, 150, 210, 360, 450, 600 };

    public PoolManager poolMgr;
    public Player player;

    private void Awake()
    {
        instance = this;
        
        if (poolMgr == null)
            poolMgr = FindObjectOfType<PoolManager>(); 
    }

    private void Update()
    {
        gameTime += Time.deltaTime;

        if (gameTime > maxGameTime)
        {
            gameTime = maxGameTime;
        }
    }
}

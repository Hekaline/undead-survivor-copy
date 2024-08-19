using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{
    public Transform[] spawnPoints;
    public SpawnData[] spawnDatas;

    private int level;
    private float timer;

    private void Awake()
    {
        spawnPoints = GetComponentsInChildren<Transform>();
    }

    private void Update()
    {
        timer += Time.deltaTime;
        level = Mathf.Min(Mathf.FloorToInt(GameManager.instance.gameTime / 10f), spawnDatas.Length - 1);

        if (timer > spawnDatas[level].spawnTime)
        {
            timer = 0f;
            Spawn();
        }
    }

    private void Spawn()
    {
        GameObject enemy = GameManager.instance.poolMgr.Get(0);
        
        // Random.Range(1~): 자기 자신 미포함 (GetComponentsInChildren은 자기도 포함)
        enemy.transform.position = spawnPoints[Random.Range(1, spawnPoints.Length)].position;
        enemy.GetComponent<Enemy>().Init(spawnDatas[level]);
    }
}

[Serializable]
public class SpawnData
{
    public float spawnTime;
    
    public int spriteType;
    public int health;
    public float speed;
}
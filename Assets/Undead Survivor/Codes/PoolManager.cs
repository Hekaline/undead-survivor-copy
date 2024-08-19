using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    public GameObject[] prefabs;
    private List<GameObject>[] pools;

    private void Awake()
    {
        pools = new List<GameObject>[prefabs.Length];
        for (var i = 0; i < pools.Length; i++)
        {
            pools[i] = new();
        }
        
        Debug.Log($"pool length: {pools.Length}");
    }

    public GameObject Get(int index)
    {
        GameObject select = null;

        foreach (var item in pools[index])
        {
            if (!item.activeSelf)
            {
                select = item;
                item.SetActive(true);
                break;
            }
        }

        if (select == null)
        {
            select = Instantiate(prefabs[index], transform);
            pools[index].Add(select);
        }
        
        return select;
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage;
    
    /// <summary>
    /// Penetration count.
    /// </summary>
    public int per;

    private Rigidbody2D rigid;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    public void Init(float damage, int per, Vector3 dir)
    {
        this.damage = damage;
        this.per = per;

        if (per > -1)
        {
            rigid.velocity = dir * 15f;
            StartCoroutine(InvokeDisable(4f));
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Enemy") || per == -1)
        {
            return;
        }

        per -= 1;
        if (per == -1)
        {
            rigid.velocity = Vector2.zero;
            gameObject.SetActive(false);
        }
    }

    private IEnumerator InvokeDisable(float seconds)
    {
        yield return new WaitForSeconds(seconds);

        rigid.velocity = Vector2.zero;
        gameObject.SetActive(false);
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class Enemy : MonoBehaviour
{
    public float speed;
    public float health;
    public float maxHealth;
    public RuntimeAnimatorController[] animCons;
    public Rigidbody2D target;

    private bool isLive = true;
    private Rigidbody2D rigid;
    private Collider2D coll;
    private SpriteRenderer spriter;
    private Animator anim;
    private WaitForFixedUpdate wait;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();
        spriter = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        wait = new WaitForFixedUpdate();
    }

    

    private void FixedUpdate()
    {
        if (!isLive || anim.GetCurrentAnimatorStateInfo(0).IsName("Hit"))
        {
            return;
        }
        
        Vector2 dirVec = target.position - rigid.position;
        Vector2 nextVec = dirVec.normalized * (speed * Time.fixedDeltaTime);

        rigid.MovePosition(rigid.position + nextVec);
        rigid.velocity = Vector2.zero;
    }

    private void LateUpdate()
    {
        spriter.flipX = target.position.x < transform.position.x;
    }
    
    private void OnEnable()
    {
        target = GameManager.instance.player.GetComponent<Rigidbody2D>();
        isLive = true;
        coll.enabled = true;
        rigid.simulated = true;
        spriter.sortingOrder = 2;
        anim.SetBool("Dead", false);
        health = maxHealth;
    }

    public void Init(SpawnData data)
    {
        anim.runtimeAnimatorController = animCons[data.spriteType];
        speed = data.speed;
        maxHealth = data.health;
        health = data.health;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Bullet"))
        {
            return;
        }

        health -= other.GetComponent<Bullet>().damage;
        StartCoroutine(KnockBack());

        if (health > 0f)
        {
            // still alive, hit action
            anim.SetTrigger("Hit");
        }
        else
        {
            // going to die
            isLive = false;
            coll.enabled = false;
            rigid.simulated = false;
            spriter.sortingOrder = 1;
            anim.SetBool("Dead", true);
            // Dead();
        }
    }

    private IEnumerator KnockBack()
    {
        yield return wait;
        Vector3 playerPos = GameManager.instance.player.transform.position;
        Vector3 dirVec = transform.position - playerPos;
        rigid.AddForce(dirVec.normalized * 10f, ForceMode2D.Impulse);
        print(dirVec);
    }
    
    private void Dead()
    {
        gameObject.SetActive(false);
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public Vector2 inputVec;
    public float speed;
    public Scanner scanner;
    public Hand[] hands;
    public RuntimeAnimatorController[] animCons;

    private Rigidbody2D rigid;
    private SpriteRenderer spriter;
    private Animator anim;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriter = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        scanner = GetComponent<Scanner>();

        hands = GetComponentsInChildren<Hand>(true);
    }

    private void OnEnable()
    {
        speed *= Character.Speed;
        anim.runtimeAnimatorController = animCons[GameManager.instance.playerId];
    }

    // private void Update()
    // {
    //     if (GameManager.instance.isLive == false)
    //     {
    //         return;
    //     }
    // }

    private void OnMove(InputValue value)
    {
        inputVec = value.Get<Vector2>();
    }

    private void FixedUpdate()
    {
        // 이동
        // 1. Rigidbody.AddForce
        // 2. Rigidbody.velocity
        // 3. Rigidbody.MovePosition

        if (GameManager.instance.isLive == false)
        {
            return;
        }
        
        Vector2 nextVec = inputVec * (speed * Time.fixedDeltaTime);
        rigid.MovePosition(rigid.position + nextVec);
    }

    private void LateUpdate()
    {
        if (GameManager.instance.isLive == false)
        {
            return;
        }
        anim.SetFloat("Speed", inputVec.magnitude);
        
        if (inputVec.x != 0)
        {
            spriter.flipX = inputVec.x < 0;
        }
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        GameManager gm = GameManager.instance;
        if (gm.isLive == false)
        {
            return;
        }

        gm.health -= Time.deltaTime * 10f;

        if (gm.health < 0)
        {
            for (int index = 2; index < transform.childCount; index++)
            {
                transform.GetChild(index).gameObject.SetActive(false);
            }

            anim.SetTrigger("Dead");
            gm.GameOver();
        }
    }
}

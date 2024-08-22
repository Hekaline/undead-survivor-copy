using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    public bool isLeft;
    public SpriteRenderer spriter;

    private SpriteRenderer player;
    private Vector3 rightPos = new Vector3(0.35f, -0.15f, 0f); 
    private Vector3 rightPosReverse = new Vector3(-0.15f, -0.15f, 0f);
    private Quaternion leftRotate = Quaternion.Euler(0f, 0f, -35);
    private Quaternion leftRotReverse = Quaternion.Euler(0f, 0f, -135);

    private void Awake()
    {
        player = GetComponentsInParent<SpriteRenderer>()[1];
    }

    private void LateUpdate()
    {
        bool isReversed = player.flipX;

        // 근접 무기
        if (isLeft)
        {
            transform.localRotation = isReversed ? leftRotReverse : leftRotate;
            spriter.flipY = isReversed;
            spriter.sortingOrder = isReversed ? 4 : 6;
        }
        // 원거리 무기
        else
        {
            transform.localPosition = isReversed ? rightPosReverse : rightPos;
            spriter.flipX = isReversed;
            spriter.sortingOrder = isReversed ? 6 : 4;
        }
    }
}

﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrefabs : PrefabsBase
{
    private Rigidbody2D rigidbody2d;
    private int waitTimer;


    private void Awake()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D target)
    {
        if (target.gameObject.CompareTag("Enemy"))
        {
            target.GetComponent<EnemyController>().UpdateHp(DataManager.Instance.Atk);
            rigidbody2d.AddForce(new Vector2(transform.localScale.x, 0) * 550f);
            Invoke("DestroySelf", 0.7f);
        }
    }

    private void OnTriggerStay2D(Collider2D target)
    {
        if (target.gameObject.CompareTag("Enemy"))
        {
            waitTimer++;
            if (waitTimer == 16)
            {
                target.GetComponent<EnemyController>().UpdateHp(DataManager.Instance.Atk);
                waitTimer = 0;
            }
        }
    }



}

﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    protected Enemy enemy;
    protected Animator animator;



    protected virtual void Update()
    {
        Dead();
    }

    protected void Dead()
    {
        if (enemy.Hp <= 0)
        {
            Destroy(gameObject);
        }
    }

}

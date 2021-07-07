using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    protected Enemy enemy;
    protected Animator animator;

    [Header("敌人id")]
    public int id;

    protected virtual void Start()
    {
        enemy = EnemyManager.Instance.GetEnemyById(id);
        animator = GetComponent<Animator>();
    }

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

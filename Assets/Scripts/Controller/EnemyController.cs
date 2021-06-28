using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : EnemyBase
{

    [Header("敌人id")]
    public int id;


    private Status status;
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        enemy = EnemyManager.Instance.GetEnemyById(id);
        UIGameEnemy.Instance.Show();
        status = Status.idle;
    }

    protected override void Update()
    {
        base.Update();
        UIGameEnemy.Instance.Refresh(enemy);
        ChangeStatus();
        if (enemy.Hp == 1000)
        {
           
        }
        else if (enemy.Hp > 960)
        {
            Debug.Log("阶段一");
            animator.speed = 2;
        }
        else if (enemy.Hp > 940)
        {
            Debug.Log("阶段二");
            animator.speed = 3;
        }
        else if (enemy.Hp > 400 && status != Status.combat)
        {
            Debug.Log("阶段三");
            animator.speed = 1;
            status = Status.start;
        }
        else if (enemy.Hp > 0 && enemy.Hp<=400)
        {
            Debug.Log("阶段四");
            status = Status.final_shape;
        }
    }

    public void UpdateHp(float damage)
    {
        enemy.Hp -= Mathf.Max(damage - enemy.Defense, 0);
    }

    private void ChangeStatus()
    {
        switch (status)
        {
            case Status.idle:
                animator.Play("Base Layer.wait");
                break;
            case Status.start:
                animator.Play("Base Layer.start");
                if (animator.GetCurrentAnimatorStateInfo(0).IsName("start"))
                {
                    Debug.Log("start");
                    status = Status.combat;
                }
                break;
            case Status.combat:
                break;
            case Status.final_shape:
                animator.Play("Base Layer.status_2");
                break;
        }
    }

    public void Shoot()
    {

    }

}
public enum Status
{
    idle,start,combat, final_shape
}

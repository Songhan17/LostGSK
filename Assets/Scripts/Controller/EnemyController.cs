using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : EnemyBase
{

    [Header("敌人id")]
    public int id;

    private GameObject shootGO;
    public Status status;
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
            animator.speed = 2;
        }
        else if (enemy.Hp > 940)
        {
            animator.speed = 3;
        }
        else if (enemy.Hp > 400 && status != Status.combat)
        {
            animator.speed = 1;
            status = Status.start;
        }
        else if (enemy.Hp > 0 && enemy.Hp<=400)
        {
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
        shootGO = Instantiate(Resources.Load<GameObject>("Prefabs/Shoot_1"),
            transform.Find("Shoot").transform.position, Quaternion.identity);
        shootGO.transform.SetParent(transform.Find("Shoot").transform);
    }

    public void Stg_01()
    {
        StartCoroutine(Spherical());
    }

    public Enemy GetSelf()
    {
        return enemy;
    }

    IEnumerator Spherical()
    {
        Vector3 fireDir = transform.up;
        Quaternion startQua = Quaternion.AngleAxis(10, Vector3.forward);
        for (int i = 0; i < 15; i++)
        {
            for (int j = 0; j < 36; j++)
            {
                GameObject go = Instantiate(Resources.Load<GameObject>("Prefabs/Stg_01"));
                go.transform.SetParent(transform);
                go.transform.position = transform.position;
                go.transform.rotation = Quaternion.Euler(fireDir);
                fireDir = startQua * fireDir;
            }
            yield return new WaitForSeconds(1f);
        }

        yield return 0;
    }

}
public enum Status
{
    idle,start,combat, final_shape
}

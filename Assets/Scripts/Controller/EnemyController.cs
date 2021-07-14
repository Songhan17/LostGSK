using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : EnemyBase
{
    private GameObject shootGO;
    public Status status;

    protected override void Start()
    {
        base.Start();
        UIGameEnemy.Instance.Show();
        status = Status.idle;
        GameObjectPoolManager.Instance.Register("Stg_01", Resources.Load<GameObject>("Prefabs/Stg_01")
            , go => go.SetActive(true), go => go.SetActive(false)).PreLoad(5);
        GameObjectPoolManager.Instance.Register("Shoot_1", Resources.Load<GameObject>("Prefabs/Shoot_1")
            , go => go.SetActive(true), go => go.SetActive(false));
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
        else if (enemy.Hp > 0 && enemy.Hp <= 400)
        {
            status = Status.final_shape;
        }
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
        shootGO = GameObjectPoolManager.Instance.Get("Shoot_1");
        shootGO.transform.position = transform.Find("Shoot").transform.position;
        shootGO.transform.localScale = transform.localScale;
        shootGO.transform.SetParent(transform.Find("Shoot").transform);
    }

    public void ShootDIY(float euler, float offsetY, int max)
    {
        StartCoroutine(DoShootDIY(euler, offsetY, max));
    }

    IEnumerator DoShootDIY(float euler, float offsetY, int max)
    {
        var temp = transform.Find("Shoot").transform;
        for (int i = 0; i < max; i++)
        {
            shootGO = GameObjectPoolManager.Instance.Get("Shoot_1");
            shootGO.transform.position = new Vector2(temp.position.x - ((i + 1) * 2), temp.position.y + offsetY);
            shootGO.transform.localScale = transform.localScale;
            shootGO.transform.localEulerAngles = new Vector3(0, 0, euler);
            shootGO.transform.SetParent(temp);
            yield return new WaitForSeconds(0.5f);
        }
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
                GameObject go = GameObjectPoolManager.Instance.Get("Stg_01");
                go.transform.SetParent(transform);
                go.transform.position = transform.position;
                go.transform.rotation = Quaternion.Euler(fireDir);
                fireDir = startQua * fireDir;
            }
            yield return new WaitForSeconds(1f);
        }

        yield return 0;
    }

    public void LookAtPlayer()
    {
        transform.localScale = new Vector2(transform.position.x > PlayerController.Instance.transform.position.x ? 1 : -1, 1);
    }



}
public enum Status
{
    idle, start, combat, final_shape
}

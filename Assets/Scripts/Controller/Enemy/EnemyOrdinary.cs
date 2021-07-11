using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyOrdinary : EnemyBase
{
    private float coolTime;

    protected override void Update()
    {
        base.Update();
        UIGameEnemy.Instance.Refresh(enemy);
        if (coolTime > 0)
        {
            coolTime -= Time.deltaTime;
        }
    }

    public void AtkPlayer()
    {
        if (coolTime > 0)
        {
            return;
        }
        animator.Play("atk_black");
        coolTime = 3f;
    }

    private void OnTriggerStay2D(Collider2D target)
    {
        if (target.CompareTag("Player"))
        {
            PlayerController.Instance.HitPlayer(enemy.Damage);
        }
    }


}

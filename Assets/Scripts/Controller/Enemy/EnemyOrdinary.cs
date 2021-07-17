using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyOrdinary : EnemyBase
{
    private float coolTime;

    protected override void Update()
    {
        base.Update();

        UIGameUI.Instance.UIGameEnemy.Refresh(enemy);
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
        transform.GetComponent<Rigidbody2D>().velocity = new Vector2();
        animator.Play("atk_black");
        coolTime = 3f;
    }
}

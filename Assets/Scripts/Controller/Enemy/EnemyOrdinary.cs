using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyOrdinary : EnemyBase
{

    public void AtkPlayer()
    {
        animator.Play("atk_black");
    }

    private void OnTriggerStay2D(Collider2D target)
    {
        if (target.CompareTag("Player"))
        {
            PlayerController.Instance.HitPlayer(enemy.Damage);
        }
    }
}

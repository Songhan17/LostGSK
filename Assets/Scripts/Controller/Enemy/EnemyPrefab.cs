using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPrefab : PrefabsBase
{
    private Enemy from;
 
    private void Start()
    {
        from = GetComponentInParent<EnemyController>().GetSelf();
        transform.SetParent(null);
    }

    private void OnTriggerEnter2D(Collider2D target)
    {
        if (target.gameObject.CompareTag("Player"))
        {
            DataManager.Instance.CurrentHp -= Mathf.Max(from.Damage- DataManager.Instance.Def,0);
        }
    }

    public void DestroySelf()
    {
        Destroy(gameObject);
    }

}

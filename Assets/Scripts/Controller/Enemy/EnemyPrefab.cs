using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPrefab : PrefabsBase
{
    private Enemy from;
    protected override void Start()
    {
        base.Start();
        from = GetComponentInParent<EnemyController>().GetSelf();
        transform.SetParent(null);
        Invoke("ChangeDict", 1f);
    }

    protected override void Update()
    {
        if (gameObject.name == "Stg_01(Clone)")
        {
            MoveOnTime();
        }
    }

    private void OnTriggerEnter2D(Collider2D target)
    {
        if (target.gameObject.CompareTag("Player"))
        {
            DataManager.Instance.CurrentHp -= Mathf.Max(from.Damage- DataManager.Instance.Def,0);
        }
    }


    public void MoveOnTime()
    {
        transform.Translate(new Vector3(transform.rotation.x, transform.rotation.y, transform.position.z)
            * 400f * Time.deltaTime, Space.World);
    }

    public void ChangeDict()
    {
        transform.rotation = Quaternion.AngleAxis(2f,new Vector2(-transform.rotation.x, -transform.rotation.y));
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPrefab : PrefabsBase
{
    private Enemy from;

    private bool to_1;

    protected override void Start()
    {
        base.Start();
        //from = GetComponentInParent<EnemyController>().GetSelf();
        transform.SetParent(null);
        Invoke("ChangeDict", 1f);
        Invoke("To_1", 1f);
    }

    protected override void Update()
    {
        if (gameObject.name == "Stg_01(Clone)")
        {
            MoveOnTime();
            Do_1();
        }
    }

    //private void OnTriggerEnter2D(Collider2D target)
    //{
    //    if (target.gameObject.CompareTag("Player"))
    //    {
    //        DataManager.Instance.CurrentHp -= Mathf.Max(from.Damage- DataManager.Instance.Def,0);
    //    }
    //}


    public void MoveOnTime()
    {
        if (to_1)
        {
            return;
        }
        transform.Translate(new Vector3(transform.rotation.x, transform.rotation.y, transform.position.z)
            * 400f * Time.deltaTime, Space.World);
    }

    public void ChangeDict()
    {
        //transform.rotation *= Quaternion.AngleAxis(2f,new Vector2(-transform.rotation.x, -transform.rotation.y));
    }

    public void Do_1()
    {
        if (!to_1)
        {
            return;
        }
        transform.position = Vector2.MoveTowards(transform.position, new Vector2(0, 0), Time.deltaTime*5);
    }


    public void To_1()
    {
        to_1 = true;
    }

}

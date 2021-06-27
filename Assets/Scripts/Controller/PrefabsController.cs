using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabsController : MonoBehaviour
{
    private Rigidbody2D rigidbody2d;

    private float AtkTimer;


    private void Awake()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Debug.Log(AtkTimer);
        Destroy(gameObject, 3f);
        if (AtkTimer > 0)
        {
            AtkTimer -= Time.deltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D target)
    {
        if (target.gameObject.CompareTag("Enemy"))
        {
            rigidbody2d.AddForce(new Vector2(transform.localScale.x, 0) * 550f);
            Destroy(gameObject, 0.7f);
        }
       

    }

    private void OnTriggerStay2D(Collider2D target)
    {
        if (target.gameObject.CompareTag("Enemy"))
        {
            ScheduleManager.Instance.Add(1f, () =>
             {
                 target.GetComponent<EnemyController>().UpdateHp(DataManager.Instance.Atk);
             });
        }
    }

    private void OnTriggerExit2D(Collider2D target)
    {
        
    }


}

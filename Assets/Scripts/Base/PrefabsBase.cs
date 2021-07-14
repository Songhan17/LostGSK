using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabsBase : MonoBehaviour
{

    protected virtual void Start()
    {
        Invoke("DestroySelf", 10f);
    }
    protected virtual void Update()
    {

    }

    protected void OnTriggerExit2D(Collider2D target)
    {
        if (target.CompareTag("Limit"))
        {
            DestroySelf();
        }
    }

    protected void DestroySelf()
    {
        GameObjectPoolManager.Instance.Recycle(gameObject);
    }

    private void OnDisable()
    {
        transform.localEulerAngles = new Vector3();
    }

}

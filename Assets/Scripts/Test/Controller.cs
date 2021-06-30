using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{

    private void Start()
    {
        GameObjectPoolManager.Instance.Register("Stg_01", Resources.Load<GameObject>("Prefabs/Stg_01")
            , go => go.SetActive(true), go => go.SetActive(false)).PreLoad(300);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(Spherical());
        }
    }

    IEnumerator Spherical()
    {
        Vector3 fireDir = transform.up;
        Quaternion startQua = Quaternion.AngleAxis(10, Vector3.forward);
            for (int j = 0; j < 36; j++)
            {
                GameObject go = GameObjectPoolManager.Instance.Get("Stg_01");
                go.transform.SetParent(transform);
                go.transform.position = transform.position;
                go.transform.rotation = Quaternion.Euler(fireDir);
                fireDir = startQua * fireDir;
            }
        yield return 0;
    }

}

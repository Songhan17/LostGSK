using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabsBase : MonoBehaviour
{

    protected virtual void Update()
    {
        Destroy(gameObject, 3f);
    }
}

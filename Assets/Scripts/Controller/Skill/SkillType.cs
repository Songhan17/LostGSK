using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillType : MonoBehaviour
{
    private bool Move;
    private void Start()
    {
        Invoke("MoveToPlayer", 0.3f);
    }
    void Update()
    {
        if (!Move)
        {
            transform.Translate(Vector3.up * 3 * Time.deltaTime);
        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position,
                PlayerController.Instance.transform.position, 20f * Time.deltaTime);
            if (Vector2.Distance(transform.position, PlayerController.Instance.transform.position) <= 0.2f)
            {
                GameObjectPoolManager.Instance.Recycle(gameObject);
            }
        }
    }

    void MoveToPlayer()
    {
        Move = true;
        StateManager.Instance.SetState(GameState.Running);
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillType : MonoBehaviour
{
    private bool Move;
    void Update()
    {
        if (!Move)
        {
            transform.Translate(Vector3.up * 3 * Time.deltaTime);
        }
        Invoke("MoveToPlayer", 0.3f);
    }

    void MoveToPlayer()
    {
        Move = true;
        transform.position = Vector2.MoveTowards(transform.position,
            PlayerController.Instance.transform.position, 20f * Time.deltaTime);
        if (Vector2.Distance(transform.position, PlayerController.Instance.transform.position) <= 0.2f)
        {
            GameObjectPoolManager.Instance.Recycle(gameObject);
            StateManager.Instance.SetState(GameState.Running);

        }
    }

}

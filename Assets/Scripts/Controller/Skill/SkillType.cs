using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillType : PrefabsBase
{
    protected override void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, 
            PlayerController.Instance.transform.position, 4f * Time.deltaTime);
        if (Vector2.Distance(transform.position, PlayerController.Instance.transform.position) <= 0.2f)
        {
            DestroySelf();
            StateManager.Instance.SetState(GameState.Running);

        }
    }
}

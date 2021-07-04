using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[JuiPanel(UiPath ="UI",IsPreBind =false)]
public class UIGameEnemy : JuiSingletonExtension<UIGameEnemy>
{
    private Text text;

    protected override void OnCreate()
    {
        base.OnCreate();
        text = transform.Find("EnemyHp").GetComponent<Text>();
    }

    protected override void OnShow()
    {
        base.OnShow();
    }

    public void Refresh(Enemy enemy)
    {
        text.text = enemy.Name+":"+Mathf.Max(enemy.Hp,0)+"@ 妖梦："+DataManager.Instance.CurrentHp;
        if (enemy.Hp <= 0)
        {
            Hide();
        }
    }
}

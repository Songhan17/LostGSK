﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIGameEnemy : JuiSingletonExtension<UIGameEnemy>
{
    public override string uiPath => "UI";

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
        text.text = enemy.Name+":"+Mathf.Max(enemy.Hp,0);
        if (enemy.Hp <= 0)
        {
            Hide();
        }
    }
}

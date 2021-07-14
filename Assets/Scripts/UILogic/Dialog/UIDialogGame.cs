using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[JuiPanel(UiPath = "Dialog/GameMsg", EnableUpdate = true, IsPreBind = false)]
public class UIDialogGame : JuiSingletonExtension<UIDialogGame>
{
    protected override void OnUpdate()
    {
        base.OnUpdate();
        if (IsFocus)
        {
            if (Input.GetKeyDown(KeyCode.X))
            {
                GameManager.Instance.LoadMain();
            }
        }
    }
}

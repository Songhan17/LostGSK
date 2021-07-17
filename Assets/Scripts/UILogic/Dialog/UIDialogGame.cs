using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class UIDialogGame : JuiSubBase
{
    protected override void OnUpdate()
    {
        base.OnUpdate();
        if (IsFocus)
        {
            if (Input.GetKeyDown(KeyCode.X))
            {
                Hide();
                GameManager.Instance.LoadMain();
            }
        }
    }
}

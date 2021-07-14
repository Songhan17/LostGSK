using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[JuiPanel(UiPath = "Dialog/SkillMsg", EnableUpdate = true, IsPreBind = false)]
public class UIDialogSkill : JuiSingletonExtension<UIDialogSkill>
{
    private Text text;

    protected override void OnCreate()
    {
        base.OnCreate();
        text = transform.Find("Text").GetComponent<Text>();
    }

    protected override void OnShow()
    {
        base.OnShow();
        StateManager.Instance.SetState(GameState.Context);

    }

    protected override void OnHide()
    {
        base.OnHide();
        StateManager.Instance.SetState(GameState.Running);
    }

    protected override void OnUpdate()
    {
        base.OnUpdate();
        if (IsFocus)
        {
            if (Input.GetKeyDown(KeyCode.X))
            {
                Hide();
            }
        }
    }

    public void Refresh(Skill skill)
    {
        text.text = "获得：" + skill.Name + "\t" + "效果：" + skill.Description;
    }
}

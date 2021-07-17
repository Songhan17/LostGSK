using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[JuiPanel(Name = "Dialog", EnableUpdate = true, IsPreBind = true)]
public class UIDialog : JuiBase<UIDialog>
{

    [JuiElementSubPanel(EnableUpdate = true)]
    private UIDialogGame GameMsg = default;
    public UIDialogGame UIDialogGame { get => GameMsg; }

    [JuiElementSubPanel(EnableUpdate = true)]
    private UIDialogSkill SkillMsg = default;
    public UIDialogSkill UIDialogSkill { get => SkillMsg; }

}

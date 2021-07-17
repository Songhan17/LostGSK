using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[JuiPanel(Name = "UI", EnableUpdate = true, IsPreBind = true)]
public class UIGameUI : JuiBase<UIGameUI>
{

    [JuiElementSubPanel(EnableUpdate = true)]
    private UIGameEnemy EnemyHp = default;
    public UIGameEnemy UIGameEnemy { get => EnemyHp; }

    [JuiElementSubPanel(EnableUpdate = true)]
    private UIGamePlayerStatus Status = default;
    public UIGamePlayerStatus UIGamePlayerStatus { get => Status; }

}

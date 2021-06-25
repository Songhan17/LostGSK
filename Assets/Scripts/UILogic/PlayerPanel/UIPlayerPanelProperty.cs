using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPlayerPanelProperty : JuiSingletonExtension<UIPlayerPanelProperty>
{
    public override string uiPath => "MenuPanel/PlayerPanel/property";

    private PlayerController playerController;
    private int maxItemCount;



    protected override void OnCreate()
    {
        base.OnCreate();
        maxItemCount = transform.childCount;
        playerController = GameObject.Find("PlayerA").GetComponent<PlayerController>();
    }

    protected override void OnShow()
    {
        base.OnShow();
        //Refresh(playerController.PlayerData());
    }

    public void Refresh(Player player)
    {
        string[] data = player.ToString().Split(';');

        for (int i = 0; i < data.Length; i++)
        {
            transform.GetChild(i).GetComponent<Text>().text = data[i];
        }
    }

}

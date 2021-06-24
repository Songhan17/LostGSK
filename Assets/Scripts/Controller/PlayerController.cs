﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : PlayerBase
{

    private void Awake()
    {
        playerData = PlayerManager.Instance.GetPlayerById(1);
    }
    void Start()
    {
        UIGameMenu.Instance.Show();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            UIGameMenu.Instance.Show();
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            PlayerPrefs.DeleteAll();
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            for (int i = 0; i < 15; i++)
            {
                SkillController.Instance.GetSkill(Random.Range(1, 15),false);
            }
        }
    }

    public Player PlayerData()
    {
        return playerData;
    }

}

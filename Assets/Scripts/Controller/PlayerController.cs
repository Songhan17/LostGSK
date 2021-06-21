using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private SkillController skillController;

    void Start()
    {
        skillController = GameObject.Find("Player").GetComponent<SkillController>();
        UIGameMenu.Instance.Show();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            for (int i = 0; i < 15; i++)
            {
                skillController.GetSkill(Random.Range(1, 15));
            }
        }
    }
}

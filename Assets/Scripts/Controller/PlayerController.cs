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

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            skillController.GetSkill(Random.Range(1,15));
        }
    }
}

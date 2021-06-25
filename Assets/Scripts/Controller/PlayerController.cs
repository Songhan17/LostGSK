using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : PlayerBase
{

    private Rigidbody2D rigidbody2d;
    private float hori;
    private bool isPause;
    [Header("速度")]
    public int moveSpeed = 5;
    private void Awake()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
    }
    void Start()
    {
        isPause = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            UIGameMenu.Instance.Show();
        }

        if (UIGameMenu.Instance.IsShow)
        {
            isPause = true;
        }
        else
        {
            isPause = false;
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            PlayerPrefs.DeleteAll();
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            for (int i = 0; i < 15; i++)
            {
                SkillController.Instance.GetSkill(Random.Range(1, 15), false);
            }
        }

        if (!isPause)
        {
            PlayerMove();
            PlayerJump();
        }
    }

    public void PlayerMove()
    {
        hori = Input.GetAxisRaw("Horizontal");
        rigidbody2d.velocity = new Vector2(hori * moveSpeed, rigidbody2d.velocity.y);
        //设置自身缩放的值
        if (hori > 0)
        {
            transform.localScale = new Vector2(-1f, 1f);
        }
        else if (hori < 0)
        {
            transform.localScale = new Vector2(1f, 1f);
        }
        else
        {
            rigidbody2d.velocity = new Vector2(0, rigidbody2d.velocity.y);
        }
    }

    // 跳跃动画
    public void PlayerJump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rigidbody2d.velocity = new Vector2(rigidbody2d.velocity.x, 8);
        }
    }
}

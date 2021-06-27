using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : PlayerBase
{

    private bool facingRight = false;
    private Collision coll;
    private Animator animator;

    private GameObject skillGameObject;

    private Rigidbody2D rigidbody2d;
    private bool isPause;
    private float jumpTimer;
    private float AtkTimer;
    private float SkillTimer;
    private bool groundTouch;

    [Space]
    [Header("Stats")]
    public int moveSpeed = 10;
    public float jumpForce = 50;

    [Space]
    [Header("Booleans")]
    public bool canMove;
    public bool doubleJumped;

    [Header("Layers")]
    public LayerMask groundLayer;

    [Space]
    public bool onGround;

    [Space]
    [Header("AblitiesSwitch")]
    public bool canDoubleJump;

    private void Awake()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collision>();
        animator = GetComponent<Animator>();
    }
    void Start()
    {
        isPause = false;
    }

    void Update()
    {

        //读取方向输入
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        //读取即时方向输入
        float xRaw = Input.GetAxisRaw("Horizontal");
        float yRaw = Input.GetAxisRaw("Vertical");
        Vector2 dir = new Vector2(x, y);

        Walk(dir);

        if (jumpTimer > 0)
        {
            jumpTimer -= Time.deltaTime;
        }
        if (AtkTimer > 0)
        {
            AtkTimer -= Time.deltaTime;
        }
        if (SkillTimer > 0)
        {
            SkillTimer -= Time.deltaTime;
        }

        if (Input.GetButtonDown("Jump"))
        {
            canDoubleJump = DataManager.Instance.WhiteSkill != null && DataManager.Instance.WhiteSkill.Id == 16;
            if (coll.onGround)
                Jump(Vector2.up, false);

            if (!coll.onGround && !doubleJumped && canDoubleJump)
            {
                doubleJumped = true;
                Jump(Vector2.up, false);
            }
        }

        if (coll.onGround && !groundTouch)
        {
            GroundTouch();
            groundTouch = true;
        }

        if (!coll.onGround && groundTouch)
        {
            groundTouch = false;
        }

        //Flip Check
        if (x > 0 && !facingRight)
        {
            // ... flip the player.
            Flip();
        }
        // Otherwise if the input is moving the player left and the player is facing right...
        else if (x < 0 && facingRight)
        {
            // ... flip the player.
            Flip();
        }

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
                SkillController.Instance.GetSkill(Random.Range(1, 18), false);
            }
        }

        if (Input.GetKeyDown(KeyCode.R) && AtkTimer <= 0)
        {
            AtkTimer = 0.3f;
            animator.SetTrigger("IsAtk");
        }
        
        if (Input.GetKeyDown(KeyCode.E) && SkillTimer <= 0)
        {
            SkillTimer = 0.5f;
            PlayerSkill();
        }


    }

    private void PlayerSkill()
    {
        if (DataManager.Instance.RedSkill.Id != 1)
        {
            return;
        }
        skillGameObject = Instantiate(Resources.Load<GameObject>(DataManager.Instance.RedSkill.AnimId),
            transform.Find("Skill").transform.position, Quaternion.identity);
        skillGameObject.transform.localScale = new Vector3(transform.localScale.x,1,1);
        skillGameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(-transform.localScale.x,0) * 600);
    }

    private void Walk(Vector2 dir)
    {
        if (!canMove)
            return;
        rigidbody2d.velocity = new Vector2(dir.x * moveSpeed, rigidbody2d.velocity.y);

    }

    private void Jump(Vector2 dir, bool wall)
    {
        //二段跳
        if (doubleJumped)
        {
            rigidbody2d.velocity = new Vector2(rigidbody2d.velocity.x, 0);
            rigidbody2d.velocity += dir * jumpForce;
            jumpTimer = 0.2f;
        }
        else
        {
            rigidbody2d.velocity = new Vector2(rigidbody2d.velocity.x, 0);
            rigidbody2d.velocity += dir * jumpForce;
            jumpTimer = 0.2f;
        }
    }

    private void Flip()
    {
        // Switch the way the player is labelled as facing.
        facingRight = !facingRight;

        // Multiply the player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    void GroundTouch()
    {
        doubleJumped = false;
    }

    private void OnTriggerEnter2D(Collider2D target)
    {
        if (target.gameObject.name == "door1-2")
        {
            var pos = StageController.Instance.ChangeStage(2);
            transform.position = new Vector2(pos.x+0.8f, pos.y);
        }
        if (target.gameObject.name == "door1-1")
        {
            var pos = StageController.Instance.ChangeStage(1);
            transform.position = new Vector2(pos.x- 0.4f, pos.y);
        }
    }

}

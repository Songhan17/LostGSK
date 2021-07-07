using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoSingleton<PlayerController>
{

    private bool facingRight = true;
    private Collision coll;
    private Animator animator;

    private GameObject skillGameObject;

    private Rigidbody2D rigidbody2d;
    private bool isPause;
    private float jumpTimer;
    private float atkTimer;
    private float skillTimer;
    private float invincibleTimer;
    private bool groundTouch;
    private bool isAtk;
    private bool isSkill;
    private bool isSit;
    private bool isSlide;
    private bool isJump;
    private bool isIdle;

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
        GameObjectPoolManager.Instance.Register("蓄力_0", Resources.Load<GameObject>("Prefabs/蓄力_0")
            , go => go.SetActive(true), go => go.SetActive(false)).PreLoad(5);
        UIGamePlayerStatus.Instance.Show();
    }


    void Update()
    {

        isAtk = animator.GetCurrentAnimatorStateInfo(0).IsName("atk");
        isSkill = animator.GetCurrentAnimatorStateInfo(0).IsName("spellcard");

        if (animator.GetCurrentAnimatorStateInfo(0).IsName("idle") && coll.onGround)
        {
            rigidbody2d.constraints = RigidbodyConstraints2D.FreezePositionX;
            rigidbody2d.freezeRotation = true;
        }
        else
        {
            rigidbody2d.constraints = RigidbodyConstraints2D.FreezeRotation;
        }

        Debug.Log(invincibleTimer);
        if (invincibleTimer > 0)
        {
            invincibleTimer -= Time.deltaTime;
            float remainder = invincibleTimer % 0.3f;
            transform.GetComponent<SpriteRenderer>().enabled = remainder > 0.15f;
        }
        else
        {
            transform.GetComponent<SpriteRenderer>().enabled = true;
        }
        if (jumpTimer > 0)
        {
            jumpTimer -= Time.deltaTime;
        }
        if (atkTimer > 0)
        {
            atkTimer -= Time.deltaTime;
        }
        if (skillTimer > 0)
        {
            skillTimer -= Time.deltaTime;
        }


        if (Input.GetButtonDown("Jump"))
        {
            if (isSit)
            {
                animator.Play("slideShovel");
                isSlide = true;
                return;
            }
            canDoubleJump = DataManager.Instance.WhiteSkill != null && DataManager.Instance.WhiteSkill.Id == 16;
            if (coll.onGround)
                isJump = true;

            if (!coll.onGround && !doubleJumped && canDoubleJump)
            {
                doubleJumped = true;
                isJump = true;
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

        if (Input.GetKeyDown(KeyCode.R) && atkTimer <= 0)
        {
            if (isSit || isSlide)
                return;
            animator.Play("atk");
        }

        if (Input.GetKeyDown(KeyCode.E) && skillTimer <= 0)
        {
            if (isSit || isSlide)
                return;
            if (DataManager.Instance.RedSkill.Id != 1)
            {
                return;
            }
            skillTimer = 0.5f;
            animator.Play("spellcard");
            StartCoroutine(PlayerSkill());
        }

        if (Input.GetKey(KeyCode.S))
        {
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("slideShovel"))
                return;
            Sit();
        }
        else
        {
            isSit = false;
        }

        if (rigidbody2d.velocity.y > 1.5f)
        {
            if (isAtk)
                return;
            if (isSkill)
                return;
            animator.Play("jump");
        }
        else if (rigidbody2d.velocity.y < -1.5f)
        {
            if (isAtk)
                return;
            if (isSkill)
                return;
            animator.Play("fail");
        }

        if (DataManager.Instance.CurrentHp == 0)
        {
            Dead();
            this.enabled = false;
        }

    }

    private void FixedUpdate()
    {
        //读取方向输入
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        //读取即时方向输入
        float xRaw = Input.GetAxisRaw("Horizontal");
        float yRaw = Input.GetAxisRaw("Vertical");
        Vector2 dir = new Vector2(x, y);
        Vector2 dirRaw = new Vector2(xRaw, yRaw);

        Walk(dir, dirRaw, isIdle);

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

        if (isSlide)
        {
            StartCoroutine(SitSlide());
        }

        Jump(Vector2.up, isJump);

    }

    IEnumerator PlayerSkill()
    {
        yield return new WaitForSeconds(0.3f);
        skillGameObject = GameObjectPoolManager.Instance.Get("蓄力_0");
        skillGameObject.transform.position = transform.Find("Skill").transform.position;
        skillGameObject.transform.localScale = new Vector3(transform.localScale.x, 1, 1);
        skillGameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(transform.localScale.x, 0) * 600);
    }

    private void Walk(Vector2 dir, Vector2 dirRaw,bool isIdle)
    {
        if (!canMove || isSlide)
            return;
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("atk") || animator.GetCurrentAnimatorStateInfo(0).IsName("spellcard")
            || isSit|| animator.GetCurrentAnimatorStateInfo(0).IsName("hitUpper"))
        {
            if (coll.onGround)
            {
                rigidbody2d.velocity = new Vector2(0, rigidbody2d.velocity.y);
            }
        }
        else
        {
            if (rigidbody2d.velocity.y > 1f && rigidbody2d.velocity.y < -1f)
            {
                return;
            }
            rigidbody2d.velocity = new Vector2(dirRaw.x * moveSpeed, rigidbody2d.velocity.y);
            if (!coll.onGround)
            {
                return;
            }
            if (dirRaw.x != 0)
            {
                if (!isIdle)
                    return;
                animator.Play("run");
            }
            else
            {
                if (!isIdle)
                    return;
                rigidbody2d.velocity = new Vector2(0, rigidbody2d.velocity.y);
                animator.Play("idle");
            }

        }
    }

    private void Jump(Vector2 dir, bool wall)
    {
        if (!wall)
            return;
        //二段跳
        if (doubleJumped)
        {
            rigidbody2d.velocity = new Vector2(rigidbody2d.velocity.x, 0);
            rigidbody2d.velocity += dir * jumpForce;
            jumpTimer = 0.2f;
            isJump = false;
        }
        else
        {
            rigidbody2d.velocity = new Vector2(rigidbody2d.velocity.x, 0);
            rigidbody2d.velocity += dir * jumpForce;
            jumpTimer = 0.2f;
            isJump = false;
        }
    }

    public void Sit()
    {

        if (!coll.onGround)
            return;
        isSit = true;
        animator.Play("sit");
    }

    IEnumerator SitSlide()
    {
        rigidbody2d.AddForce(new Vector2(transform.localScale.x, 0) * 30);
        yield return new WaitForSeconds(0.3f);
        isSlide = false;
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

    public void Dead()
    {
        animator.SetTrigger("IsDead");
    }

    public void HitPlayer(int damage)
    {
        if (invincibleTimer<=0)
        {
            DataManager.Instance.CurrentHp -= Mathf.Max(damage - DataManager.Instance.Def, 0);
            animator.Play("hitUpper");
            invincibleTimer = 3f;
        }
    }

    private void OnTriggerEnter2D(Collider2D target)
    {
        if (target.gameObject.name == "door1-2")
        {
            var pos = StageController.Instance.ChangeStage(2);
            transform.position = new Vector2(pos.x + 1f, pos.y);
        }
        if (target.gameObject.name == "door1-1")
        {
            var pos = StageController.Instance.ChangeStage(1);
            transform.position = new Vector2(pos.x - 0.5f, pos.y);
        }
    }

    private void OnTriggerStay2D(Collider2D target)
    {
        if (target.gameObject.layer == 9)
        {
            isIdle = false;
        }
        else
        {
            isIdle = true;
        }
    }

}

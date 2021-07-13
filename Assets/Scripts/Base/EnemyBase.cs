using BehaviorDesigner.Runtime;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    protected Enemy enemy;
    protected Animator animator;
    protected Vector2 startPos;

    private float invincibleTimer;
    private float timer = 0;

    [Header("敌人id")]
    public int id;

    protected virtual void Start()
    {
        enemy = EnemyManager.Instance.GetEnemyById(id);
        animator = GetComponent<Animator>();
    }

    protected virtual void Update()
    {
        if (StateManager.Instance.GetState() != GameState.Running)
        {
            return;
        }
        Dead();
        if (invincibleTimer > 0)
        {
            invincibleTimer -= Time.deltaTime;
        }
    }

    public void UpdateHp(int damage)
    {
        var text = GameObjectPoolManager.Instance.Get("DamageText");
        text.GetComponentInChildren<HudPrefab>().HUD(DataManager.Instance.Atk - enemy.Defense);
        text.transform.position = transform.position;
        text.transform.localScale = new Vector2(1, 1);
        enemy.Hp -= Mathf.Max(damage - enemy.Defense, 1);
    }

    protected void Dead()
    {
        if (enemy.Hp <= 0)
        {
            StageController.Instance.FocusView(transform);
            GetComponent<BehaviorTree>().enabled = false;
            timer += Time.deltaTime;
            transform.GetComponent<SpriteRenderer>().color = new Color(1, 0, 0, 1 - timer);
            if (transform.GetComponent<SpriteRenderer>().color.a <= 0)
            {
                var go = GameObjectPoolManager.Instance.Get("Red");
                go.transform.position = transform.GetChild(0).transform.position;
                gameObject.SetActive(false);
                StateManager.Instance.SetState(GameState.Pause);
                SkillController.Instance.AddSkill(enemy.Drop, false);
            }
        }
    }
    private void OnEnable()
    {
        startPos = transform.position;
        GetComponent<BehaviorTree>().enabled = true;
    }

    private void OnDisable()
    {
        transform.position = startPos;
        enemy = EnemyManager.Instance.GetEnemyById(id);
    }

    protected virtual void OnTriggerEnter2D(Collider2D target)
    {
        if (target.CompareTag("AtkRange") && invincibleTimer <= 0)
        {
            UpdateHp(DataManager.Instance.Atk);
            invincibleTimer = 0.5f;
        }
    }
}

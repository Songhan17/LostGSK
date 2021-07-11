using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    protected Enemy enemy;
    protected Animator animator;

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
        enemy.Hp -= Mathf.Max(damage - enemy.Defense, 1);
    }

    public void UpdateHp(int damage, Collider2D target)
    {
        var text = GameObjectPoolManager.Instance.Get("DamageText");
        text.GetComponentInChildren<HudPrefab>().HUD(DataManager.Instance.Atk - enemy.Defense);
        text.transform.position = transform.position;
        text.transform.localScale = new Vector2(target.transform.localScale.x, 1);
        enemy.Hp -= Mathf.Max(damage - enemy.Defense, 1);
    }

    protected void Dead()
    {
        if (enemy.Hp <= 0)
        {
            timer += Time.deltaTime;
            transform.GetComponent<SpriteRenderer>().color = new Color(1, 0, 0, 1 - timer);
            if (transform.GetComponent<SpriteRenderer>().color.a <= 0)
            {
                var go = GameObjectPoolManager.Instance.Get("Red");
                go.transform.position = transform.GetChild(0).transform.position;
                Destroy(gameObject);
                StateManager.Instance.SetState(GameState.Pause);
                SkillController.Instance.AddSkill(enemy.Drop, false);
            }
        }
    }
    protected virtual void OnTriggerEnter2D(Collider2D target)
    {
        if (target.CompareTag("AtkRange") && invincibleTimer <= 0)
        {
            UpdateHp(DataManager.Instance.Atk, target);
            invincibleTimer = 0.5f;
        }
    }
}

using UnityEngine;

public class CH45MonsterCombat : MonoBehaviour
{
    [Header("CH10")]
    public Ch10Health ch10Health;
    public CombatSystem oldCombatSystem;

    [Header("Monster")]
    public MonsterHealth monsterHealth;

    [Header("Attack Settings")]
    public int damage = 5;
    public float attackCooldown = 0.8f;

    private Animator animator;
    private float nextAttackTime;

    private readonly int hitImpactHash =
        Animator.StringToHash("Base Layer.Hit Impact");

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (ch10Health == null || monsterHealth == null)
            return;

        // Pertarungan monster dimulai setelah CH10 kalah.
        if (ch10Health.currentHealth > 0)
            return;

        // Matikan sistem serangan lama agar tidak bentrok.
        if (oldCombatSystem != null &&
            oldCombatSystem.enabled)
        {
            oldCombatSystem.enabled = false;
        }

        if (monsterHealth.currentHealth <= 0)
            return;

        if (Input.GetKeyDown(KeyCode.F) &&
            Time.time >= nextAttackTime)
        {
            AttackMonster();
        }
    }

    void AttackMonster()
    {
        nextAttackTime = Time.time + attackCooldown;

        if (animator != null)
        {
            animator.CrossFade(
                hitImpactHash, 0.1f, 0, 0f
            );
        }

        monsterHealth.TakeDamage(damage);

        Debug.Log(
            "CH45 menyerang monster! HP monster: " +
            monsterHealth.currentHealth
        );
    }
}
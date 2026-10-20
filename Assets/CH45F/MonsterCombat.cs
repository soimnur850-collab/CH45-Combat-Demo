using UnityEngine;

public class MonsterCombat : MonoBehaviour
{
    [Header("Player")]
    public Transform player;
    public Ch45Health playerHealth;
    public Animator playerAnimator;

    [Header("Monster")]
    public Animator monsterAnimator;

    [Header("Combat Settings")]
    public float attackRange = 2.5f;
    public int damage = 20;
    public float attackCooldown = 2f;

    private float nextAttackTime;
    private bool playerIsDead = false;

    void Awake()
    {
        if (monsterAnimator == null)
            monsterAnimator = GetComponent<Animator>();
    }

    void Update()
    {
        if (player == null || playerHealth == null ||
            monsterAnimator == null)
            return;

        if (playerIsDead)
            return;

        if (playerHealth.currentHealth <= 0)
        {
            playerIsDead = true;
            return;
        }

        float distance = Vector3.Distance(
            transform.position,
            player.position
        );

        if (distance <= attackRange &&
            Time.time >= nextAttackTime)
        {
            AttackPlayer();
        }
    }

    void AttackPlayer()
    {
        nextAttackTime = Time.time + attackCooldown;

        monsterAnimator.SetTrigger("Attack");

        playerHealth.TakeDamage(damage);

        Debug.Log(
            "Monster menyerang CH45! HP CH45: " +
            playerHealth.currentHealth
        );
    }

}
using UnityEngine;

public class MonsterHealth : MonoBehaviour
{
    [Header("Health Settings")]
    public int maxHealth = 100;
    public int currentHealth = 100;

    [Header("Animation")]
    public Animator animator;

    private bool isDead = false;

    void Awake()
    {
        currentHealth = maxHealth;

        if (animator == null)
            animator = GetComponent<Animator>();
    }

    public void TakeDamage(int damage)
    {
        if (isDead)
            return;

        currentHealth = Mathf.Clamp(
            currentHealth - damage, 0, maxHealth
        );

        Debug.Log("Monster terkena serangan! HP: " + currentHealth);

        if (currentHealth <= 0)
        {
            isDead = true;
            Debug.Log("Monster kalah!");
        }
        else if (animator != null)
        {
            animator.SetTrigger("Hit Impact");
        }
    }
}
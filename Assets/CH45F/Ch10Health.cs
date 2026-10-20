using UnityEngine;

public class Ch10Health : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth = 100;

    private Animator animator;
    public RectTransform healthBarFill;

    private bool isDead = false;

    void Awake()
    {
        currentHealth = maxHealth;
        animator = GetComponentInChildren<Animator>();
    }

    void Start()
    {
        UpdateHealthBar();

        if (animator != null)
        {
            animator.SetTrigger("Combat");
            Debug.Log("CH10 masuk Combat Stance!");
        }
        else
        {
            Debug.LogWarning("Animator CH10 tidak ditemukan!");
        }
    }

    public void TakeDamage(int damage)
    {
        if (isDead || currentHealth <= 0)
            return;

        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        UpdateHealthBar();

        Debug.Log("CH10 terkena serangan! HP: " + currentHealth);

        if (currentHealth <= 0)
        {
            isDead = true;

            if (animator != null)
            {
                animator.ResetTrigger("Hit");
                animator.ResetTrigger("Combat");
                animator.SetTrigger("Death");
            }

            Debug.Log("HP CH10 habis! Death dipicu.");
        }
        else
        {
            if (animator != null)
            {
                animator.ResetTrigger("Hit");
                animator.SetTrigger("Hit");
            }

            Debug.Log("CH10 memainkan Hit Reaction!");
        }
    }

    void UpdateHealthBar()
    {
        if (healthBarFill != null && maxHealth > 0)
        {
            float healthPercent = (float)currentHealth / maxHealth;
            healthBarFill.localScale =
                new Vector3(healthPercent, 1f, 1f);
        }
    }

}
using UnityEngine;

public class Ch45Health : MonoBehaviour
{
    [Header("Health Settings")]
    public int maxHealth = 100;
    public int currentHealth = 100;

    [Header("Health Bar")]
    public RectTransform healthBarFill;

    [Header("Death Animation")]
    public Animator animator;

    private bool isDead = false;

    void Awake()
    {
        currentHealth = maxHealth;

        if (animator == null)
            animator = GetComponent<Animator>();
    }

    void Start()
    {
        UpdateHealthBar();
    }

    public void TakeDamage(int damage)
    {
        if (isDead || currentHealth <= 0)
            return;

        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        UpdateHealthBar();

        Debug.Log("CH45 terkena serangan! HP: " + currentHealth);

        if (currentHealth <= 0)
        {
            isDead = true;
            Debug.Log("CH45 kalah!");

            if (animator != null)
            {
                animator.ResetTrigger("Death");
                animator.SetTrigger("Death");
            }
            else
            {
                Debug.LogWarning("Animator CH45 belum terhubung!");
            }
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
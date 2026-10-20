using UnityEngine;
using System.Collections;

public class CombatController : MonoBehaviour
{
    public Ch10Health opponentHealth;
    public StaminaSystem staminaSystem;

    public int damage = 20;
    public int totalHits = 5;
    public float attackDelay = 0.5f;
    public float staminaCost = 10f;

    private Animator myAnimator;
    private bool isAttacking = false;

    void Awake()
    {
        myAnimator = GetComponent<Animator>();
    }

    void Start()
    {
        Debug.Log("COMBAT CONTROLLER AKTIF!");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && !isAttacking)
        {
            if (staminaSystem == null)
            {
                Debug.LogWarning("StaminaSystem CH45 belum terhubung!");
                return;
            }

            if (staminaSystem.currentStamina < staminaCost)
            {
                Debug.Log("Stamina CH45 tidak cukup!");
                return;
            }

            staminaSystem.UseStamina(staminaCost);
            StartCoroutine(AttackOpponent());
        }
    }

    IEnumerator AttackOpponent()
    {
        isAttacking = true;

        for (int i = 0; i < totalHits; i++)
        {
            if (opponentHealth == null)
            {
                Debug.LogWarning("Health CH10 belum terhubung!");
                break;
            }

            if (myAnimator != null)
            {
                myAnimator.CrossFade("Base Layer.Hit Impact", 0.1f);
            }

            yield return new WaitForSeconds(attackDelay);

            opponentHealth.TakeDamage(damage);

            Debug.Log("Damage diberikan ke CH10!");
            Debug.Log("Serangan ke-" + (i + 1) +
                      " | Damage: " + damage);

            if (i < totalHits - 1)
            {
                yield return new WaitForSeconds(0.5f);
            }
        }

        isAttacking = false;
    }

}
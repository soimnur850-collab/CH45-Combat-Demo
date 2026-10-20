using UnityEngine;
using System.Collections;

public class CombatSystem : MonoBehaviour
{
    [Header("Target")]
    public Ch10Health opponentHealth;

    [Header("Attack Settings")]
    public int damage = 20;

    [Header("Attack Sound")]
    public AudioSource punchAudioSource;

    private Animator animator;
    private bool isAttacking = false;

    private static readonly int HitImpactHash =
        Animator.StringToHash("Base Layer.Hit Impact");

    void Awake()
    {
        animator = GetComponent<Animator>();

        if (punchAudioSource == null)
        {
            punchAudioSource = GetComponent<AudioSource>();
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && !isAttacking)
        {
            StartCoroutine(AttackRoutine());
        }
    }

    IEnumerator AttackRoutine()
    {
        if (opponentHealth == null)
        {
            Debug.LogWarning("CH10 belum terhubung!");
            yield break;
        }

        isAttacking = true;

        if (animator != null)
        {
            animator.CrossFade(HitImpactHash, 0.1f, 0, 0f);
        }

        if (punchAudioSource != null &&
    punchAudioSource.clip != null)
        {
            yield return new WaitForSeconds(0.2f);
            punchAudioSource.PlayOneShot(punchAudioSource.clip);
        }

        opponentHealth.TakeDamage(damage);

        Debug.Log("CH45 menyerang CH10! Damage: " + damage);

        if (animator != null)
        {
            yield return null;

            while (animator.IsInTransition(0) ||
                   animator.GetCurrentAnimatorStateInfo(0).fullPathHash
                   == HitImpactHash)
            {
                yield return null;
            }
        }

        isAttacking = false;
    }

}
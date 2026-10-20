using UnityEngine;
using System.Collections;

public class Ch10EnemyAI : MonoBehaviour
{
    [Header("Target")]
    public Transform player;
    public Ch45Health playerHealth;

    [Header("Movement")]
    public float moveSpeed = 2f;
    public float attackRange = 1.8f;
    public float rotationSpeed = 8f;

    [Header("Attack")]
    public int damage = 10;
    public bool canAttack = false;
    public float attackCooldown = 3f;

    private Animator animator;
    private CharacterController characterController;
    private float nextAttackTime;
    private bool isDead = false;

    void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        if (player == null || isDead)
            return;

        float distance = Vector3.Distance(
            transform.position, player.position);

        Vector3 direction = player.position - transform.position;
        direction.y = 0f;

        if (direction.sqrMagnitude > 0.001f)
        {
            Quaternion targetRotation =
                Quaternion.LookRotation(direction);

            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                targetRotation,
                rotationSpeed * Time.deltaTime);
        }

        if (distance > attackRange)
        {
            Vector3 moveDirection = direction.normalized;

            if (characterController != null)
            {
                characterController.SimpleMove(
                    moveDirection * moveSpeed);
            }
            else
            {
                transform.position +=
                    moveDirection * moveSpeed * Time.deltaTime;
            }
        }
        else if (Time.time >= nextAttackTime)
        {
            StartCoroutine(AttackRoutine());
        }
    }

    IEnumerator AttackRoutine()
    {
        nextAttackTime = Time.time + attackCooldown;

   

        yield return new WaitForSeconds(0.5f);

        if (!isDead && playerHealth != null && canAttack)
        {
            playerHealth.TakeDamage(damage);
        }
    }

    public void Die()
    {
        isDead = true;
    }

}
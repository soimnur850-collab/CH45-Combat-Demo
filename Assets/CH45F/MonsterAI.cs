using UnityEngine;

public class MonsterAI : MonoBehaviour
{
    [Header("Target")]
    public Transform player;

    [Header("Movement")]
    public float walkSpeed = 2f;
    public float runSpeed = 5f;
    public float attackRange = 1.8f;
    public float rotationSpeed = 8f;

    [Header("Animation")]
    public Animator animator;

    private bool isAttacking = false;

    void Update()
    {
        if (player == null || animator == null)
            return;

        Vector3 direction = player.position - transform.position;
        direction.y = 0f;

        float distance = direction.magnitude;

        if (distance > attackRange)
        {
            isAttacking = false;

            if (direction.sqrMagnitude > 0.001f)
            {
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Slerp(
                    transform.rotation,
                    targetRotation,
                    rotationSpeed * Time.deltaTime
                );
            }

            float currentSpeed = distance > 5f ? runSpeed : walkSpeed;

            transform.position += transform.forward * currentSpeed * Time.deltaTime;

            animator.SetFloat("Speed", currentSpeed);
        }
        else
        {
            animator.SetFloat("Speed", 0f);

            if (!isAttacking)
            {
                if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Hit Impact"))
                {
                    animator.SetTrigger("Attack");
                    isAttacking = true;
                }
            }
        }
    }

}
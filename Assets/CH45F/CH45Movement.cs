using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class CH45MovementController : MonoBehaviour
{
    public float walkSpeed = 2f;
    public float runSpeed = 5f;
    public float jumpHeight = 2f;
    public float gravity = -9.81f;
    public float rotationSpeed = 540f;

    private CharacterController controller;
    private Animator animator;
    private Vector3 velocity;

    private int idleState;
    private int walkState;
    private int runState;
    private int currentLocomotionState = -1;

    void Awake()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();

        idleState = Animator.StringToHash("Base Layer.Idle");
        walkState = Animator.StringToHash("Base Layer.Walk");
        runState = Animator.StringToHash("Base Layer.Run");
    }

    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Debug.Log("Horizontal: " + horizontal + " | Vertical: " + vertical);
        Vector3 inputDirection = new Vector3(horizontal, 0f, vertical);
        inputDirection = Vector3.ClampMagnitude(inputDirection, 1f);

        bool isMoving = inputDirection.magnitude > 0.1f;
        bool isRunning = Input.GetKey(KeyCode.LeftShift) && isMoving;

        if (isMoving)
        {
            Quaternion targetRotation =
                Quaternion.LookRotation(inputDirection, Vector3.up);

            transform.rotation = Quaternion.RotateTowards(
                transform.rotation,
                targetRotation,
                rotationSpeed * Time.deltaTime
            );
        }

        if (controller.isGrounded && velocity.y < 0f)
        {
            velocity.y = -1f;
        }

        if (Input.GetKeyDown(KeyCode.Space) && controller.isGrounded)
        {
            Debug.Log("SPACE TERDETEKSI! Grounded: " + controller.isGrounded);

            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);

            if (animator != null)
            {
                animator.SetTrigger("Jump");
            }
        }

        velocity.y += gravity * Time.deltaTime;

        float currentSpeed = isRunning ? runSpeed : walkSpeed;

        Vector3 movement =
            transform.forward * inputDirection.magnitude * currentSpeed;

        movement.y = velocity.y;

        controller.Move(movement * Time.deltaTime);

        if (animator == null)
        {
            return;
        }

        int targetState;

        if (!isMoving)
        {
            targetState = idleState;
        }
        else if (isRunning)
        {
            targetState = runState;
        }
        else
        {
            targetState = walkState;
        }

        if (targetState != currentLocomotionState)
        {
            animator.CrossFade(targetState, 0.15f);
            currentLocomotionState = targetState;
        }
    }
}
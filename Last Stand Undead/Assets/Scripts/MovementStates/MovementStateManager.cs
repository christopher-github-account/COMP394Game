using UnityEngine;

public class MovementStateManager : MonoBehaviour
{
    #region Movement
    [SerializeField] public float currentMoveSpeed = 3;
    public float walkSpeed = 3, walkBackSpeed = 2;
    public float runSpeed = 7, runBackSpeed = 5;
    public float crouchSpeed = 2, crouchBackSpeed = 1;
    public float airSpeed = 1.5f;

    [HideInInspector] public Vector3 direction;
    [SerializeField] CharacterController controller;
    #endregion

    #region GroundCheck
    [SerializeField] float groundYOffset;
    [SerializeField] LayerMask groundMask;
    Vector3 spherePos;
    #endregion

    #region Gravity
    [SerializeField] float gravity = -9.81f;
    [SerializeField] float jumpForce = 10f;
    [HideInInspector] public bool jumped;
    Vector3 velocity;
    #endregion

    #region States
    public MovementBaseState previousState;
    public MovementBaseState currentState;

    public IdleState Idle = new IdleState();
    public WalkState Walk = new WalkState();
    public CrouchState Crouch = new CrouchState();
    public RunState Run = new RunState();
    public JumpState Jump = new JumpState();
    #endregion

    [SerializeField] public Animator animator;

    [HideInInspector] public float hzInput, vInput;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gravity *= 2;
        animator = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
        SwitchState(Idle);
    }

    // Update is called once per frame
    void Update()
    {
        GetDirectionAndMove();
        Gravity();
        Falling();

        animator.SetFloat("hzInput", hzInput);
        animator.SetFloat("vInput", vInput);

        currentState.UpdateState(this);
    }

    public void SwitchState(MovementBaseState state)
    {
        currentState = state;
        currentState.EnterState(this);
    }

    void GetDirectionAndMove()
    {
        hzInput = Input.GetAxis("Horizontal");
        vInput = Input.GetAxis("Vertical");
        Vector3 airDirection = Vector3.zero;
        if (!IsGrounded()) { airDirection = transform.forward * vInput + transform.right * hzInput; }
        else { direction = transform.forward * vInput + transform.right * hzInput; }

        controller.Move((direction.normalized * currentMoveSpeed + airDirection.normalized * airSpeed) * Time.deltaTime);

        animator.SetFloat("hzInput", hzInput);
        animator.SetFloat("vInput", vInput);
    }

    public bool IsGrounded()
    {
        spherePos = new Vector3(transform.position.x, transform.position.y - groundYOffset, transform.position.z);
        if (Physics.CheckSphere(spherePos, controller.radius - 0.05f, groundMask)) { return true; }
        return false;
    }

    void Gravity()
    {
        if (!IsGrounded()) { velocity.y += gravity * Time.deltaTime; }
        else if (velocity.y < 0) { velocity.y = -2; }

        controller.Move(velocity * Time.deltaTime);
    }

    void Falling() { animator.SetBool("Falling", !IsGrounded()); }

    private void OnDrawGizmos()
    {
        if (spherePos != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(spherePos, controller.radius - 0.05f);
        }
    }

    public void JumpForce() { velocity.y += jumpForce; }

    public void Jumped() { jumped = true; }
}

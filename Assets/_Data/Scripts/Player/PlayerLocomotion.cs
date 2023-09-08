
using UnityEngine;

public class PlayerLocomotion : PlayerAbstract
{
    //public OnEventAnimator OnAnimatorMove;

    [Header("Movement Flag")]
    public bool IsJumping;
    public bool IsSprinting;
    public bool IsGrounded;
    public bool IsWalking;
    public bool Is1D = false;

    [Header("Movement Speed")]
    [SerializeField] private float rotationSpeed = 360f;
    [SerializeField] private float rotationSpeedTPS = 15f;
    [SerializeField] private float speed;
    [SerializeField] private float jumpDamp;
    [SerializeField] private float airControl;
    [SerializeField] private float stepDown;
    [SerializeField] private float jumpheight;
    [SerializeField] private float gravity;
    [SerializeField] private float speedDecrease;
    private Vector3 movementDirection;
    [SerializeField] private Vector3 rootMotion;
    [SerializeField] public Vector3 velocity;

    /// đoạn code sau chưa phân code 
    //protected override void Awake()
    //{
    //    base.Awake();
    //    this.OnAnimatorMove = playerCtrl.Character.OnEventAnimator;
    //    this.Is1D = false;
    //}

    //private void Start()
    //{
    //    if (OnAnimatorMove != null)
    //    {
    //        OnAnimatorMove.OnAnimatorMoveEvent += HandleAnimatorMoveEvent;
    //    }
    //}

    //public void SetOnEventAnimator(OnEventAnimator onEventAnimator)
    //{
    //    this.rootMotion = Vector3.zero;
    //    onEventAnimator.OnAnimatorMoveEvent += HandleAnimatorMoveEvent;
    //}

    private void OnDisable()
    {
        this.ResetLocomotion();
    }

    public void HanldeUpdateAllMovement()
    {
        HandleJump();
        Handle1DMode();
       // HandleSprinting();
    }

    public void HanldeAllMovementFix()
    {

        this.HandleRotation();
        this.HandleUpdateMove();
        // this.SetSpeed();
    }

    public void SetSpeed()
    {
        if (playerCtrl.PlayerWeapon.PlayerWeaponActive.IsFiring)
        {
            speed /= speedDecrease;
        }
    }
    private void Handle1DMode()
    {
        this.playerCtrl.Animator.SetFloat("TypeMove", this.Is1D ? 0 : 1);
        if (Input.GetKeyDown(KeyCode.Q) && playerCtrl.PlayerCamera.IsTPSCamera)
        {
            this.Is1D = !this.Is1D;
        }
        if (Is1D)
        {
            HandleMovement1D();
        }
    }

    private void HandleMovement1D()
    {
        this.movementDirection = new Vector3(this.playerCtrl.PlayerInput.MovementInput.x, 0f, this.playerCtrl.PlayerInput.MovementInput.y);
        this.movementDirection = Quaternion.AngleAxis(this.playerCtrl.PlayerCamera.MainCamera.transform.rotation.eulerAngles.y, Vector3.up) * movementDirection;
        this.movementDirection.Normalize();

        Vector3 velocity = this.movementDirection;
        this.playerCtrl.CharacterController.Move(this.speed * velocity * Time.deltaTime);
    }

    public void SetIsSprinting(bool SprintingInput)
    {
        bool canSprint = (playerCtrl.PlayerInput.MovementInput != Vector2.zero) && 
            (!playerCtrl.PlayerWeapon.PlayerWeaponActive.IsFiring) && (!playerCtrl.PlayerAim.IsAim) && 
            (!playerCtrl.PlayerWeapon.PlayerWeaponReload.IsReload);
        if (canSprint && SprintingInput )
        {
            this.IsSprinting = true;
        }
        else
        {
            this.IsSprinting= false;
        }
    }

    private void HandleJump()
    {
        if (playerCtrl.PlayerInput.JumpInput)
        {
            Jump();
            playerCtrl.PlayerInput.JumpInput = false;
        }
    }

    private void HandleUpdateMove()
    {
        if (IsJumping)
        {
            UpdateInAir();
        }
        else
        {
            UpdateOnGround();
        }
    }

    private void HandleRotation()
    {
        if (this.Is1D)
        {
            if (this.movementDirection != Vector3.zero || this.movementDirection == Vector3.forward)
            {
                //Debug.Log("Hello");
                Quaternion toRatation = Quaternion.LookRotation(movementDirection, Vector3.up);
                this.playerCtrl.PlayerTransform.rotation = Quaternion.RotateTowards(this.playerCtrl.PlayerTransform.rotation, toRatation, this.rotationSpeed * Time.deltaTime);
            }
        }
        else
        {
            float yawCamera = this.playerCtrl.PlayerCamera.MainCamera.transform.eulerAngles.y;
            this.playerCtrl.PlayerTransform.rotation = Quaternion.Slerp(this.playerCtrl.PlayerTransform.rotation, Quaternion.Euler(0, yawCamera, 0), rotationSpeedTPS * Time.fixedDeltaTime);

        }

    }


    private Vector3 AdjustVelocityToSlope(Vector3 velocity)
    {
        var ray = new Ray(this.playerCtrl.PlayerTransform.position, Vector3.down);
        if (Physics.Raycast(ray, out RaycastHit hitInfo, 0.2f))
        {
            var slopeRotation = Quaternion.FromToRotation(Vector3.up, hitInfo.normal);
            var adjustedVelocity = slopeRotation * velocity;

            if (adjustedVelocity.y < 0)
            {
                return adjustedVelocity;
            }
        }

        return velocity;
    }
    //

    public Vector3 CalculateAircontrol()
    {
        return ((this.playerCtrl.PlayerTransform.forward * playerCtrl.PlayerInput.MovementInput.y) + (this.playerCtrl.PlayerTransform.right * playerCtrl.PlayerInput.MovementInput.x)) * (airControl / 100);
    }

    //private void HandleAnimatorMoveEvent()
    //{
    //    rootMotion += playerCtrl.Animator.deltaPosition;
    //}

    private void UpdateInAir()
    {
        velocity.y -= gravity * Time.fixedDeltaTime;
        Vector3 displacement = velocity * Time.fixedDeltaTime;
        displacement += CalculateAircontrol();
        playerCtrl.CharacterController.Move(displacement);
        IsJumping = !this.playerCtrl.CharacterController.isGrounded;
        rootMotion = Vector3.zero;
        this.playerCtrl.Animator.SetBool("IsJumping", IsJumping);
    }
    private void UpdateOnGround()
    {
        Vector3 stepForwardAmount = rootMotion * speed;
        Vector3 stepDownAmount = Vector3.down * stepDown;
        playerCtrl.CharacterController.Move(stepForwardAmount + stepDownAmount);
        rootMotion = Vector3.zero;

        if (!this.playerCtrl.CharacterController.isGrounded) //falling
        {
            SetInAir(0);
        }
    }
    public void Jump()
    {
        if (!IsJumping)
        {
            float jumpvelocity = Mathf.Sqrt(2 * gravity * jumpheight);
            SetInAir(jumpvelocity);
        }
    }

    private void SetInAir(float jumpvelocity)
    {
        IsJumping = true;
        velocity = playerCtrl.Animator.velocity * jumpDamp * speed;
        velocity.y = jumpvelocity;
        this.playerCtrl.Animator.SetBool("IsJumping", true);
    }

    public void ResetLocomotion()
    {
        this.IsJumping = false;
        this.IsSprinting = false;
    }
}

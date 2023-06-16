using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLocomotion : PlayerAbstract
{
    [Header("Movement Flag")]
    public bool IsSprinting;
    public bool IsJumping;
    public bool IsGrounded;
    public bool Is1D;

    [Header("Movement Speed")]
    [SerializeField] private float walkingSpeed = 0.5f;     //Multiplication
    [SerializeField] private float runningSpeed = 1f;       //Multiplication
    [SerializeField] private float sprintingSpeed = 1.4f;   //Multiplication
    [SerializeField] private float rotationSpeed = 360f;
    [SerializeField] private float jumpSpeed = 5f;
    private Vector3 movementDirection;
    private float motionSpeed;
    private float ySpeed;

    [Header("Jump")]
    [SerializeField] private float jumpHeight = 0.2f;
    [SerializeField] private float jumpButtonGracePeriod = 0.2f;
    private float? lastButtonPressedTime;
    private float? lastGroundedTime;
    private float originalStepOffset;

    protected override void Awake()
    {
        base.Awake();
        this.Is1D = false;
        this.playerCtrl.Animator.SetFloat("TypeMove", this.Is1D ? 0 : 1);
        this.originalStepOffset = this.playerCtrl.CharacterController.stepOffset;
        this.IsGrounded = this.playerCtrl.CharacterController.isGrounded;
    }

    private void Update()
    {
<<<<<<< HEAD
        //Debug.Log(playerCtrl.PlayerCamera.TPSCam.activeInHierarchy +"TPS");
        //Debug.Log(playerCtrl.PlayerCamera.FPSCam.activeInHierarchy +"FPS");
=======
>>>>>>> main
        if (Input.GetKeyDown(KeyCode.Q) && !playerCtrl.PlayerCamera.FPSCam.activeInHierarchy)
        {
            this.Is1D = !this.Is1D;
            this.playerCtrl.Animator.SetFloat("TypeMove", this.Is1D ? 0 : 1);
        }
    }

    public void HanldeAllMovement()
    {
        this.HandleMovement();
        this.HandleRotation();
        this.HandleSprinting();
        this.HandleJumping();
    }

    private void HandleMovement()
    {
        //this.moveSpeed = this.IsSprinting ? this.sprintingSpeed : this.runningSpeed;
        this.movementDirection = new Vector3(this.playerCtrl.PlayerInput.MovementInput.x, 0f, this.playerCtrl.PlayerInput.MovementInput.y);
        this.ySpeed += Physics.gravity.y * Time.deltaTime;

        float magnitude = Mathf.Clamp01(movementDirection.magnitude);

        this.movementDirection = Quaternion.AngleAxis(this.playerCtrl.CameraTransform.rotation.eulerAngles.y, Vector3.up) * movementDirection;
        this.movementDirection.Normalize();

        Vector3 velocity = this.movementDirection * magnitude;
        velocity = this.AdjustVelocityToSlope(velocity);
        velocity.y += this.ySpeed;

        this.playerCtrl.CharacterController.Move(velocity * Time.deltaTime);
    }

    private void HandleRotation()
    {
        if (this.Is1D)
        {
            if (this.movementDirection != Vector3.zero || this.movementDirection == Vector3.forward)
            {
                Quaternion toRatation = Quaternion.LookRotation(movementDirection, Vector3.up);
                this.playerCtrl.PlayerTransform.rotation = Quaternion.RotateTowards(this.playerCtrl.PlayerTransform.rotation, toRatation, this.rotationSpeed * Time.deltaTime);
            }
        }
        else
        {
            //if (this.IsHoldingRotateCamera) return;
            float yawCamera = this.playerCtrl.CameraTransform.transform.eulerAngles.y;
            this.playerCtrl.PlayerTransform.rotation = Quaternion.Slerp(this.playerCtrl.PlayerTransform.rotation, Quaternion.Euler(0, yawCamera, 0), 15 * Time.fixedDeltaTime);
        }

    }

    private void HandleSprinting()
    {
        if (this.movementDirection != Vector3.zero)
        {
            if (this.IsSprinting)
            {
                this.motionSpeed = this.sprintingSpeed;
            }
            else
            {
                this.motionSpeed = this.runningSpeed;
            }
            this.playerCtrl.Animator.SetFloat("MotionSpeed", this.motionSpeed);
        }
    }

    private void HandleJumping()
    {
        

        if (this.IsJumping)
        {
            this.IsJumping = false;
            this.lastButtonPressedTime = Time.time;
        }

        if (this.playerCtrl.CharacterController.isGrounded)
        {
            this.lastGroundedTime = Time.time;
        }

        if (Time.time - this.lastGroundedTime <= this.jumpButtonGracePeriod)
        {
            this.ySpeed = -0.5f;
            this.playerCtrl.CharacterController.stepOffset = this.originalStepOffset;

            this.playerCtrl.Animator.SetBool("IsGrounded", true);
            this.IsGrounded = true;
            this.playerCtrl.Animator.SetBool("IsJumping", false);
            this.IsJumping = false;
            this.playerCtrl.Animator.SetBool("IsFalling", false);

            if (Time.time - this.lastButtonPressedTime <= this.jumpButtonGracePeriod)
            {
                this.ySpeed = this.jumpSpeed;

                this.playerCtrl.Animator.SetBool("IsJumping", true);
                this.IsJumping = true;

                this.lastGroundedTime = null;
                this.lastButtonPressedTime = null;
            }
        }
        else
        {
            this.playerCtrl.CharacterController.stepOffset = 0;
            this.playerCtrl.Animator.SetBool("IsGrounded", false);
            this.IsGrounded = false;

            if ((this.IsJumping && this.ySpeed < 0) || this.ySpeed < -2)
            {
                this.playerCtrl.Animator.SetBool("IsFalling", true);
            }
        }
    }

    private Vector3 AdjustVelocityToSlope(Vector3 velocity)
    {
        var ray = new Ray(transform.position, Vector3.down);
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
}

using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLocomotion : PlayerAbstract
{
    public PlayerWeaponActiveOld weaponActive;
    public Animator animatorPlayer;
    public Animator rigLayer;
    public OnEventAnimator Onanimatormove;
    public Transform cameraLookat;
    public AxisState xAxis;
    public AxisState yAxis;
    //

    [Header("Movement Flag")]
    public bool isJumping;
    public bool IsSprinting;
    public bool IsGrounded;
    public bool Is1D;

    [Header("Movement Speed")]
    [SerializeField] private float rotationSpeed = 360f;
    [SerializeField] private float speed;
    [SerializeField] private float jumpDamp;
    [SerializeField] private float airControl;
    [SerializeField] private float stepDown;
    [SerializeField] private float jumpheight;
    [SerializeField] private float gravity;
    private Vector3 movementDirection;
    [SerializeField] private Vector3 rootMotion;
    [SerializeField] private Vector3 velocity;

    /// đoạn code sau chưa phân code 
    protected override void Awake()
    {
        base.Awake();
        this.Onanimatormove = FindObjectOfType<OnEventAnimator>();
        this.Is1D = false;
    }

    private void Start()
    {
        if (Onanimatormove != null)
        {
            Onanimatormove.OnAnimatorMoveEvent += HandleAnimatorMoveEvent;
        }
    }


    public void HanldeAllMovementUpdate()
    {
        HandleJump();
        Handle1DMode();
        HandleSprinting();
    }

    public void HanldeAllMovementFix()
    {

        this.HandleRotation();
        this.HandleUpdateMove();
    }


    private void Handle1DMode()
    {
        this.playerCtrl.Animator.SetFloat("TypeMove", this.Is1D ? 0 : 1);
        if (Input.GetKeyDown(KeyCode.Q) && !playerCtrl.PlayerCamera.FPSCam.gameObject.activeInHierarchy)
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
        //this.moveSpeed = this.IsSprinting ? this.sprintingSpeed : this.runningSpeed;
        this.movementDirection = new Vector3(this.playerCtrl.PlayerInput.MovementInput.x, 0f, this.playerCtrl.PlayerInput.MovementInput.y);
        //float ySpeed = Physics.gravity.y * Time.deltaTime;
        this.movementDirection = Quaternion.AngleAxis(this.playerCtrl.CameraTransform.rotation.eulerAngles.y, Vector3.up) * movementDirection;
        this.movementDirection.Normalize();

        Vector3 velocity = this.movementDirection;
        //velocity = this.AdjustVelocityToSlope(velocity);
        //velocity.y += ySpeed;

        this.playerCtrl.CharacterController.Move(velocity * Time.deltaTime);
    }
    private void HandleSprinting()
    {
        RaycastWeapon currentweapon = weaponActive.GetActiveWeapon();
        
        if(currentweapon != null)
        {
            rigLayer.SetBool("isSprinting", IsSprinting);
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
        if (isJumping)
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
                Quaternion toRatation = Quaternion.LookRotation(movementDirection, Vector3.up);
                this.playerCtrl.PlayerTransform.rotation = Quaternion.RotateTowards(this.playerCtrl.PlayerTransform.rotation, toRatation, this.rotationSpeed * Time.deltaTime);
            }
        }
        else
        {
            //if (this.IsHoldingRotateCamera) return;
            xAxis.Update(Time.fixedDeltaTime);
            yAxis.Update(Time.fixedDeltaTime);
            cameraLookat.eulerAngles = new Vector3(yAxis.Value, yAxis.Value, 0);

            float yawCamera = this.playerCtrl.CameraTransform.transform.eulerAngles.y;
            this.playerCtrl.PlayerTransform.rotation = Quaternion.Slerp(this.playerCtrl.PlayerTransform.rotation, Quaternion.Euler(0, yawCamera, 0), 15 * Time.fixedDeltaTime);
        }

    }

    /*   private void HandleSprinting()
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
       }*/

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
    //

    public Vector3 CalculateAircontrol()
    {
        return ((transform.forward * playerCtrl.PlayerInput.MovementInput.y) + (transform.right * playerCtrl.PlayerInput.MovementInput.x)) * (airControl / 100);
    }

    private void HandleAnimatorMoveEvent()
    {
        rootMotion += playerCtrl.Animator.deltaPosition;
    }

    private void UpdateInAir()
    {
        velocity.y -= gravity * Time.fixedDeltaTime;
        Vector3 displacement = velocity * Time.fixedDeltaTime;
        displacement += CalculateAircontrol();
        playerCtrl.CharacterController.Move(displacement);
        isJumping = !this.playerCtrl.CharacterController.isGrounded;
        rootMotion = Vector3.zero;
        animatorPlayer.SetBool("isJumping", isJumping);
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
        if (!isJumping)
        {
            float jumpvelocity = Mathf.Sqrt(2 * gravity * jumpheight);
            SetInAir(jumpvelocity);
        }
    }

    private void SetInAir(float jumpvelocity)
    {
        isJumping = true;
        velocity = playerCtrl.Animator.velocity * jumpDamp * speed;
        velocity.y = jumpvelocity;
        animatorPlayer.SetBool("isJumping", true);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLocomotion : PlayerAbstract
{
    [Header("Movement Flag")]
    public bool IsSprinting;
    public bool IsGrounded;
    public bool Is1D = true;

    [Header("Movement Speed")]
    [SerializeField] private float walkingSpeed = 2f;
    [SerializeField] private float runningSpeed = 4f;
    [SerializeField] private float sprintingSpeed = 7f;
    [SerializeField] private float rotationSpeed = 360f;
    [SerializeField] private float jumpSpeed = 5f;
    private Vector3 movementDirection;
    private float moveSpeed;
    private float ySpeed;

    protected override void Awake()
    {
        base.Awake();
        this.Is1D = true;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            this.Is1D = !this.Is1D;
            this.playerCtrl.Animator.SetFloat("TypeMove", this.Is1D ? 0 : 1);
        }

    }

    public void HanldeAllMovement()
    {
        this.HandleMovement();
        this.HandleRotation();
        this.HandleJumping();
    }

    private void HandleMovement()
    {
        this.moveSpeed = this.IsSprinting ? this.sprintingSpeed : this.runningSpeed;
        this.movementDirection = new Vector3(this.playerCtrl.PlayerInput.MovementInput.x, 0f, this.playerCtrl.PlayerInput.MovementInput.y);
        this.ySpeed += Physics.gravity.y * Time.deltaTime;

        float magnitude = Mathf.Clamp01(movementDirection.magnitude) * this.moveSpeed;

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
            Debug.Log("1D");
            if (this.movementDirection != Vector3.zero || this.movementDirection == Vector3.forward)
            {
                Quaternion toRatation = Quaternion.LookRotation(movementDirection, Vector3.up);
                this.playerCtrl.PlayerTransform.rotation = Quaternion.RotateTowards(this.playerCtrl.PlayerTransform.rotation, toRatation, this.rotationSpeed * Time.deltaTime);
            }
        }
        else
        {
            Debug.Log("2D");
            //if (this.IsHoldingRotateCamera) return;
            float yawCamera = this.playerCtrl.CameraTransform.transform.eulerAngles.y;
            this.playerCtrl.PlayerTransform.rotation = Quaternion.Slerp(this.playerCtrl.PlayerTransform.rotation, Quaternion.Euler(0, yawCamera, 0), 15 * Time.fixedDeltaTime);
        }
    }

    private void HandleJumping()
    {

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

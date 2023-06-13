using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : PlayerAbstract
{
    public Vector2 MovementInput;
    public bool SprintInput;
    public bool JumpInput;
    public bool AimInput;
    public bool AttackInput;

    private PlayerControls playerControls;
    //private InputActionReference

    private void OnEnable()
    {
        if (this.playerControls == null)
        {
            this.playerControls = new PlayerControls();
            this.playerControls.PlayerMovement.Movement.performed += i => this.MovementInput = i.ReadValue<Vector2>();

            //this.playerControls.PlayerAction.Sprint.performed += i => this.sprintInput = true;
            //this.playerControls.PlayerAction.Sprint.canceled += i => this.sprintInput = false;
            //this.playerControls.PlayerAction.Jump.performed += i => this.jumpInput = true;

            //this.playerControls.PlayerAction.Aim.performed += i => this.AimInput = true;
            //this.playerControls.PlayerAction.Aim.canceled += i => this.AimInput = false;

            //this.playerControls.PlayerAction.Attack.performed += i => this.AttackInput = true;
        }

        this.playerControls.Enable();
    }

    private void OnDisable()
    {
        this.playerControls?.Disable();
    }

    public void HandleAllInput()
    {
        this.HandleMovementInput();
        this.HandleSprintInput();
        //this.HandleJumpInput();
        //this.HandleAimInput();
        //this.HandleAttackInput();
    }

    private void HandleMovementInput()
    {
        float horizontalInput = this.MovementInput.x;
        float verticalInput = this.MovementInput.y;
        float moveAmount = Mathf.Clamp01(Mathf.Abs(horizontalInput) + Mathf.Abs(verticalInput));

        this.playerCtrl.PlayerAnimation.UpdateValuesAnimation("MoveState", moveAmount);
        this.playerCtrl.PlayerAnimation.UpdateValuesAnimation("InputX", horizontalInput);
        this.playerCtrl.PlayerAnimation.UpdateValuesAnimation("InputY", verticalInput);
    }

    private void HandleSprintInput() 
    {

    }

    private void OnApplicationFocus(bool focus)
    {
        if (focus)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }
}

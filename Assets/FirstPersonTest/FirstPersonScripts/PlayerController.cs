
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    private float playerSpeed = 2.0f;
    private float jumpHeight = 1.0f;
    private float gravityValue = -9.81f;
    private Vector3 move;
    private Transform cameraTransform;

    private void Start()
    {
        controller = gameObject.GetComponent<CharacterController>();
        cameraTransform = Camera.main.transform;
    }

    void Update()
    {
        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        if (InputManager.HasInstance)
        {
            Vector2 movement = InputManager.Instance.GetPlayerMovement();
            move = new Vector3(movement.x, 0, movement.y);
            if (InputManager.Instance.PlayerTrumpedThisFrame() && groundedPlayer)
            {
                playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
            }
        }
        move = cameraTransform.forward*move.z + cameraTransform.right*move.x;
        move.y = 0f;

      

        controller.Move(move * Time.deltaTime * playerSpeed);



        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    }
}

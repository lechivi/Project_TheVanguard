
using Unity.VisualScripting;
using UnityEngine;
public class InputManager : BaseManager<InputManager>
{
    private Vector2 m_Position;
    private PlayerControll playerControll;

    protected override void Awake()
    {
        base.Awake();
        playerControll = new PlayerControll ();
    }


    private void OnEnable()
    {
        playerControll.Enable();
    }

    private void OnDisable()
    {
        playerControll.Disable();
    }

    public Vector2 GetPlayerMovement()
    {
        return playerControll.Player.Movement.ReadValue<Vector2>();
    } 

    public Vector2 GetMouseDelta()
    {
        return playerControll.Player.Look.ReadValue<Vector2>();
    }

    public bool PlayerTrumpedThisFrame()
    {
        return playerControll.Player.Jump.triggered;
    }

}

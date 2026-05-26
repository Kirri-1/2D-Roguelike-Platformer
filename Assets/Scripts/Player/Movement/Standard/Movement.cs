using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Dash))]
public class Movement : MonoBehaviour
{
    PlayerMovement playerMovement;
    InputAction moveAction;
    public float moveSpeed = 5f;
    private Rigidbody2D playerRb;
    Dash dash;

    private void Awake()
    {
        playerMovement = new PlayerMovement();
        dash = GetComponent<Dash>();
        moveAction = playerMovement.Player.Movement;
    }
    private void OnEnable()
    {
        moveAction.Enable();
    }
    private void OnDisable()
    {
        moveAction.Disable();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerRb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        Move();
    }


    private void Move()
    {
        if (dash.isDashing)
            return;

        Vector2 moveInput = moveAction.ReadValue<Vector2>();
        Vector2 moveVelocity = moveInput * moveSpeed;

        playerRb.linearVelocity = new Vector2(moveVelocity.x, playerRb.linearVelocity.y);
    }
}

using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(GroundCheck))]
public class Jump : MonoBehaviour
{
    GroundCheck groundCheck;
    Rigidbody2D playerRb;
    PlayerMovement playerMovement;
    InputAction jumpAction;
    Coroutine jumpCoroutine;

    public float jumpForce = 10f;
    public MovementStruct jumpStruct;

    public bool jumpRequested = false;

    private void Awake()
    {
        playerRb = GetComponent<Rigidbody2D>();
        groundCheck = GetComponent<GroundCheck>();
        playerMovement = new PlayerMovement();
        jumpAction = playerMovement.Player.Jump;
    }

    private void OnEnable()
    {
        jumpAction.Enable();
    }
    private void OnDisable()
    {
        jumpAction.Disable();
    }

    void Update()
    {
        if(jumpAction.triggered)
        {
            jumpRequested = true;
            return;
        }

        ResetJump();
    }

    private void FixedUpdate()
    {
        if(jumpRequested)
        {
            JumpVoid();
        }
    }

    void JumpVoid()
    {
        if (!jumpStruct.HasCharges)
        {
            jumpRequested = false;
            return;
        }
        jumpStruct.currentCharge--;
        playerRb.linearVelocity = new Vector2(playerRb.linearVelocity.x, 0f);
        playerRb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        jumpRequested = false;
        if (DebugMode.DebugModeActive)
            Debug.Log("Player Jumped");
    }

    void ResetJump()
    {
        if(groundCheck.isGrounded)
            jumpStruct.ResetCharges();
    }
}

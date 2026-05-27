using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static CancelMovementEnums;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(GroundCheck))]
[RequireComponent(typeof(CancelMovementEnums))]
public class Jump : MonoBehaviour
{
    GroundCheck groundCheck;
    Rigidbody2D playerRb;
    CancelMovementEnums cancelMovementEnums;
    PlayerMovement playerMovement;
    InputAction jumpAction;
    Coroutine jumpCoroutine;

    public float jumpForce = 10f;
    public MovementStruct jumpStruct;

    public bool jumpRequested = false;

    HashSet<CancelMovementEnums.CancelMovementType> jumpCancelEnums = new HashSet<CancelMovementEnums.CancelMovementType>()
    {
        CancelMovementEnums.CancelMovementType.Attack,
        CancelMovementEnums.CancelMovementType.Stun
    };

    private void Awake()
    {
        playerRb = GetComponent<Rigidbody2D>();
        cancelMovementEnums = GetComponent<CancelMovementEnums>();
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
        if (jumpRequested && !cancelMovementEnums.HasAnyFlag(jumpCancelEnums))
        {
            if(jumpStruct.HasCharges)
                JumpVoid();
        }
    }

    void JumpVoid()
    {
        playerRb.linearVelocity = new Vector2(playerRb.linearVelocity.x, 0f);
        playerRb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        jumpRequested = false;
        jumpStruct.ConsumeCharge();
        if (DebugMode.DebugModeActive)
            Debug.Log("Player Jumped");
    }

    void ResetJump()
    {
        if(groundCheck.isGrounded)
            jumpStruct.ResetCharges();
    }
    
}

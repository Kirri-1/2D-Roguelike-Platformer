using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(GroundCheck))]
[RequireComponent(typeof(CancelMovementEnums))]
[RequireComponent(typeof(PlayerData))]
public class Jump : MonoBehaviour
{
    GroundCheck groundCheck;
    Rigidbody2D playerRb;
    CancelMovementEnums cancelMovementEnums;

    PlayerData playerData;
    PlayerMovement playerMovement;
    InputAction jumpAction;

    public bool jumpRequested = false;

    HashSet<CancelMovementEnums.CancelMovementType> jumpCancelEnums = new HashSet<CancelMovementEnums.CancelMovementType>()
    {
        CancelMovementEnums.CancelMovementType.Attack,
        CancelMovementEnums.CancelMovementType.Stun
    };

    private void Awake()
    {
        playerData = GetComponent<PlayerData>();
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
        if (jumpRequested && !cancelMovementEnums.HasAnyFlag(jumpCancelEnums)
            && playerData.jumpData.jumpStruct.IsUnlocked)
        {
            if(playerData.jumpData.jumpStruct.HasCharges)
                JumpVoid();
            jumpRequested = false;
        }
    }

    void JumpVoid()
    {
        playerRb.linearVelocity = new Vector2(playerRb.linearVelocity.x, 0f);
        playerRb.AddForce(Vector2.up * playerData.jumpData.JumpForce, ForceMode2D.Impulse);
        jumpRequested = false;
        playerData.jumpData.jumpStruct.ConsumeCharge();
        if (DebugMode.DebugModeActive)
            Debug.Log("Player Jumped");
    }

    void ResetJump()
    {
        if(groundCheck.isGrounded)
            playerData.jumpData.jumpStruct.ResetCharges();
    }
    public void IncreaseJumpCharge(int amount = 1) => playerData.jumpData.jumpStruct.IncreaseCharge(amount);
    public void ModifyJumpForce(float amount = 1f) => playerData.jumpData.ModifyJumpForce(amount);
}

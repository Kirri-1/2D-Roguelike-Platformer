using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Player.Checks;
using DebugN;
using Player.Movement.SharedProperties;
using Level.Rules;

namespace Player.Movement.Standard
{
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

    //    HashSet<CancelMovementEnums.CancelMovementType> jumpCancelEnums = new HashSet<CancelMovementEnums.CancelMovementType>()
    //{
    //    CancelMovementEnums.CancelMovementType.Attack,
    //    CancelMovementEnums.CancelMovementType.Stun
    //};
    //         ^ potential future stuff? Keeping it just in case

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
            if (jumpAction.triggered && !jumpRequested)
            {
                jumpRequested = true;
                return;
            }

            ResetJump();
        }

        private void FixedUpdate()
        {
            if (!jumpRequested)
                return;
            var levelData = LevelRulesScript.Instance.MovementStruct().jumpData;
            if (!playerData.jumpData.jumpStruct.CanUseAbility(levelData.MaxCharges()))
            {
                jumpRequested = false;
                return;
            }
            if(cancelMovementEnums.cancelMovementType != CancelMovementEnums.CancelMovementType.None)
            {
                jumpRequested = false;
                return;
            }
            float jumpForce = Mathf.Min(playerData.jumpData.TotalJumpForce(), levelData.JumpForce);
            JumpVoid(jumpForce);
            jumpRequested = false;
        }


        void JumpVoid(float jumpForce)
        {
            playerRb.linearVelocity = new Vector2(playerRb.linearVelocity.x, 0f);
            playerRb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            jumpRequested = false;
            playerData.jumpData.jumpStruct.ConsumeCharge();
            if (DebugMode.DebugModeActive)
                Debug.Log("Player Jumped");
        }

        void ResetJump()
        {
            if (groundCheck.isGrounded)
                playerData.jumpData.jumpStruct.ResetCharges();
        }
        public void IncreaseJumpCharge(int amount = 1) => playerData.jumpData.jumpStruct.IncreaseCharge(amount);
        public void ModifyJumpForce(float amount = 1f) => playerData.jumpData.ModifyJumpForce(amount);
    }
}

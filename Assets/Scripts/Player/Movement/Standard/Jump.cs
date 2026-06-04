using UnityEngine;
using UnityEngine.InputSystem;
using DebugN;
using Player.Movement.SharedProperties;
using Level.Rules;
using Player.Movement.Core;

namespace Player.Movement.Standard
{
    public class Jump : AbstractPlayerAbilities
    {
        InputAction jumpAction;

        public bool jumpRequested = false;

        //HashSet<CancelMovementEnums.CancelMovementType> jumpCancelEnums = new HashSet<CancelMovementEnums.CancelMovementType>()
        //{
        //    CancelMovementEnums.CancelMovementType.Attack,
        //    CancelMovementEnums.CancelMovementType.Stun,
        //    CancelMovementEnums.CancelMovementType.Knockback,
        //    CancelMovementEnums.CancelMovementType.Blink
        //};
        //         ^ potential future stuff? Keeping it just in case

        protected override void Awake()
        {
            base.Awake();
            jumpAction = inputManager.PlayerInput.Player.Jump;
        }

        void Update()
        {
            if (jumpAction.triggered && !jumpRequested)
            {
                jumpRequested = true;
                return;
            }

            if (jumpAction.WasReleasedThisFrame() && playerRb.linearVelocity.y > 0 && !gravity.JumpCut)
                gravity.SetJumpCut(true);

            if (playerRb.linearVelocity.y <= 0 && gravity.JumpCut)
                gravity.SetJumpCut(false);

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
            //if(cancelMovementEnums.cancelMovementType != CancelMovementEnums.CancelMovementType.None)
            //{
            //    jumpRequested = false;
            //    return;
            //}
            float jumpForce = Mathf.Min(playerData.jumpData.TotalJumpForce(), levelData.JumpForce);
            JumpVoid(jumpForce);
            jumpRequested = false;
        }


        void JumpVoid(float jumpForce)
        {
            cancelMovementEnums.RemoveCancelMovementType(CancelMovementEnums.CancelMovementType.Dash);
            Debug.Log("Removed Dash");
            playerRb.linearVelocity = new Vector2(playerRb.linearVelocity.x, 0f);
            playerRb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            jumpRequested = false;
            playerData.jumpData.jumpStruct.ConsumeCharge();
            //if (DebugMode.DebugModeActive)
            //    Debug.Log("Player Jumped");
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

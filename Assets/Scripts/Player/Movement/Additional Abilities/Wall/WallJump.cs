using UnityEngine;
using Player.Data;
using UnityEngine.InputSystem;

namespace Player.Movement.AdditionalAbilities
{
    public class WallJump : AbstractWallComponent
    {
        [SerializeField]
        float wallJumpForce = 15f;
        Vector2 wallNormal;
        [SerializeField]
        bool wallJumpRequested;
        InputAction jumpAction;
        [SerializeField]
        private int consumeCharge = 0; //TODO: make this integer a static number that actually consumes a decent amount of stamina

        protected override void Awake()
        {
            base.Awake();
            jumpAction = base.inputManager.PlayerInput.Player.Jump;
        }


        protected override void OnCollisionEnter2D(Collision2D collision)
        {
            base.OnCollisionEnter2D(collision);
            wallNormal = collision.contacts[0].normal;
        }

        protected override void OnCollisionExit2D(Collision2D collision)
        {
            base.OnCollisionExit2D(collision);
        }

        protected override void Update()
        {
            if (jumpAction.triggered)
                wallJumpRequested = true;
            base.Update();
        }

        private void FixedUpdate()
        {
            if (!wallJumpRequested)
                return;
            if (currentWall == null)
            {
                wallJumpRequested = false;
                return;
            }
            if (IsCorrectWallState(WallStateComponent.WallStateType.Hold))
            {
                wallJumpRequested = false;
                return;
            }
            if (playerData.staminaData.HasStamina())
            {
                wallJumpRequested = false;
                return;
            }
            HandleWallJump();
            wallJumpRequested = false;
        }

        void HandleWallJump()
        {
            playerRb.linearVelocity = Vector2.zero;
            wallState.UpdateWallState(WallStateComponent.WallStateType.Jump);
            Vector2 playerFacingDirection = base.moveAction.ReadValue<Vector2>();
            Vector2 faceDirection;
            bool pressingAwayFromWall = Vector2.Dot(playerFacingDirection, wallNormal) > 0;

            if (pressingAwayFromWall)
                faceDirection = (wallNormal + Vector2.up).normalized;
            else
                faceDirection = Vector2.up;

            playerRb.AddForce(faceDirection * wallJumpForce, ForceMode2D.Impulse);
            playerData.staminaData.staminaStruct.ConsumeCharge(consumeCharge);
            ResetWallState();
        }

        void ResetWallState()
        {
            wallState.UpdateWallState(WallStateComponent.WallStateType.None);
        }
    }
}

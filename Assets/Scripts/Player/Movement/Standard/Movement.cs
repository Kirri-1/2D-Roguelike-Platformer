using Level.Rules;
using Player.Movement.SharedProperties;
using UnityEngine;
using UnityEngine.InputSystem;
using Player.Movement.Core;

namespace Player.Movement.Standard
{    
    public class Movement : AbstractPlayerAbilities
    {
        InputAction moveAction;
        [SerializeField] float deceleration = 50f;
        protected override void Awake()
        {
            base.Awake();
            moveAction = inputManager.PlayerInput.Player.Movement;
        }

        private void FixedUpdate()
        {
            if (cancelMovementEnums.cancelMovementType != CancelMovementEnums.CancelMovementType.None
                && cancelMovementEnums.cancelMovementType != CancelMovementEnums.CancelMovementType.Jump)
                return;
            Move();
        }


        private void Move()
        {
            float playerSpeed = playerData.movementData.TotalSpeed();
            Vector2 moveInput = moveAction.ReadValue<Vector2>();
            float targetX = moveInput.x * playerSpeed;

            if (Mathf.Abs(playerRb.linearVelocity.x) > playerSpeed)
            {
                playerRb.linearVelocity = new Vector2(
                    Mathf.MoveTowards(playerRb.linearVelocity.x, targetX, deceleration * Time.fixedDeltaTime),
                    playerRb.linearVelocity.y
                );
            }
            else
            {
                playerRb.linearVelocity = new Vector2(targetX, playerRb.linearVelocity.y);
            }
        }

        public void ModifyMovementSpeed(float amount) => playerData.movementData.ModifyMovementSpeed(amount);
        public void SetMovementSpeed(float newSpeed) => playerData.movementData.SetMovementSpeed(newSpeed);
    }
}

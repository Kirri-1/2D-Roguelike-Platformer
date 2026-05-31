using Level.Rules;
using Player.Movement.SharedProperties;
using UnityEngine;
using UnityEngine.InputSystem;
using Player.Data;
using Player.InputManagerN;
using Player.Movement.Core;

namespace Player.Movement.Standard
{    
    public class Movement : AbstractPlayerAbilities
    {
        InputAction moveAction;

        protected override void Awake()
        {
            base.Awake();
            moveAction = inputManager.PlayerInput.Player.Movement;
        }

        private void FixedUpdate()
        {
            if (cancelMovementEnums.cancelMovementType != CancelMovementEnums.CancelMovementType.None)
                return;
            Move();
        }


        private void Move()
        {
            var levelData = LevelRulesScript.Instance.MovementStruct().movementData;
            float levelSpeed = Mathf.Min(playerData.movementData.TotalSpeed(), levelData.MovementSpeed);

            Vector2 moveInput = moveAction.ReadValue<Vector2>();
            Vector2 moveVelocity = moveInput * levelSpeed;

            playerRb.linearVelocity = new Vector2(moveVelocity.x, playerRb.linearVelocity.y);
        }

        public void ModifyMovementSpeed(float amount) => playerData.movementData.ModifyMovementSpeed(amount);
        public void SetMovementSpeed(float newSpeed) => playerData.movementData.SetMovementSpeed(newSpeed);
    }
}

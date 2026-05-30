using Level.Rules;
using Player.Movement.SharedProperties;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player.Movement.Standard
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(CancelMovementEnums))]
    [RequireComponent(typeof(PlayerData))]
    public class Movement : MonoBehaviour
    {
        PlayerMovement playerMovement;
        InputAction moveAction;

        PlayerData playerData;

        private Rigidbody2D playerRb;
        CancelMovementEnums cancelMovementEnums;

        private void Awake()
        {
            playerData = GetComponent<PlayerData>();
            playerMovement = new PlayerMovement();
            moveAction = playerMovement.Player.Movement;
            cancelMovementEnums = GetComponent<CancelMovementEnums>();
            playerRb = GetComponent<Rigidbody2D>();
        }
        private void OnEnable()
        {
            moveAction.Enable();
        }
        private void OnDisable()
        {
            moveAction.Disable();
        }

        private void FixedUpdate()
        {
            if (cancelMovementEnums.cancelMovementType != CancelMovementEnums.CancelMovementType.None)
                return;
            Move();
        }


        private void Move()
        {
            var levelData = LevelRulesScript.Instance.LevelRules.LevelData.modifyMovementStruct.movementData;
            float levelSpeed = Mathf.Min(playerData.movementData.TotalSpeed(), levelData.MovementSpeed);

            Vector2 moveInput = moveAction.ReadValue<Vector2>();
            Vector2 moveVelocity = moveInput * levelSpeed;

            playerRb.linearVelocity = new Vector2(moveVelocity.x, playerRb.linearVelocity.y);
        }

        public void ModifyMovementSpeed(float amount) => playerData.movementData.ModifyMovementSpeed(amount);
        public void SetMovementSpeed(float newSpeed) => playerData.movementData.SetMovementSpeed(newSpeed);
    }
}

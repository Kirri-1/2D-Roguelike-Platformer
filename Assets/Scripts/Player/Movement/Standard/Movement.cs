using UnityEngine;
using UnityEngine.InputSystem;
using Player.Movement.DashN;
using Player.Movement.SharedProperties;

namespace Player.Movement.Standard
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(Dash))]
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
            if (cancelMovementEnums.cancelMovementType != CancelMovementEnums.CancelMovementType.None)
                return;

            Vector2 moveInput = moveAction.ReadValue<Vector2>();
            Vector2 moveVelocity = moveInput * playerData.movementData.MovementSpeed;

            playerRb.linearVelocity = new Vector2(moveVelocity.x, playerRb.linearVelocity.y);
        }

        public void ModifyMovementSpeed(float amount) => playerData.movementData.ModifyMovementSpeed(amount);
        public void SetMovementSpeed(float newSpeed) => playerData.movementData.SetMovementSpeed(newSpeed);
    }
}

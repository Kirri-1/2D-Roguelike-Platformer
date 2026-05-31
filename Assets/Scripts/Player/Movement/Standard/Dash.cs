using Level.Rules;
using Player.Checks;
using Player.Movement.SharedProperties;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using Player.Data;
using Player.InputManagerN;
using Player.Movement.Core;

namespace Player.Movement.DashN
{
    public class Dash : AbstractPlayerAbilities
    {
        InputAction dashAction;
        InputAction moveAction;

        bool dashRequested = false;

        [Header("Dash Settings")]
        [SerializeField]
        [Tooltip("The speed at which the player will dash.")]
        float dashSpeed = 20f;
        public float DashSpeed => dashSpeed;
        [SerializeField]
        [Tooltip("The duration of the dash action.")]
        float dashDuration = 0.15f;
        public float DashDuration => dashDuration;

        Coroutine dashCoroutine;

        protected override void Awake()
        {
            base.Awake();
            dashAction = inputManager.PlayerInput.Player.Dash;
            moveAction = inputManager.PlayerInput.Player.Movement;
        }

        private void Update()
        {
            if (dashAction.triggered)
            {
                dashRequested = true;
                return;
            }
            ResetDash();
        }
        private void FixedUpdate()
        {
            if (!dashRequested)
                return;

            if (!playerData.dashData.dashStruct.CanUseAbility(LevelRulesScript.Instance.MovementStruct().dashData.MaxCharges()))
            {
                dashRequested = false;
                return;
            }

            if (cancelMovementEnums.cancelMovementType != CancelMovementEnums.CancelMovementType.None)
            {
                dashRequested = false;
                return;
            }
            DashVoid();
            dashRequested = false;
        }

        void DashVoid()
        {
            cancelMovementEnums.AddCancelMovementType(CancelMovementEnums.CancelMovementType.Dash);
            if (dashCoroutine != null)
            {
                StopCoroutine(dashCoroutine);
            }
            var levelData = LevelRulesScript.Instance.MovementStruct().dashData;
            float dashSpeed = Mathf.Min(playerData.dashData.TotalSpeed(), levelData.DashSpeed);
            float dashDuration = Mathf.Min(playerData.dashData.TotalDuration(), levelData.DashDuration);
            dashCoroutine = StartCoroutine(DashCoroutine(dashSpeed, dashDuration));
            playerData.dashData.dashStruct.ConsumeCharge();
        }

        void ResetDash()
        {
            if (groundCheck.isGrounded)
            {
                playerData.dashData.dashStruct.ResetCharges();
            }
        }

        public IEnumerator DashCoroutine(float dashSpeed, float dashDuration)
        {
            Vector2 dashDirection = moveAction.ReadValue<Vector2>();
            if (dashDirection == Vector2.zero) dashDirection = Vector2.right;
            dashDirection = dashDirection.normalized;

            float elapsed = 0f;
            while (elapsed < dashDuration)
            {
                playerRb.linearVelocity = dashDirection * dashSpeed;
                elapsed += Time.fixedDeltaTime;
                yield return new WaitForFixedUpdate();
            }

            float gravityDir = playerRb.gravityScale >= 0 ? playerRb.gravityScale : 1f;
            if (dashDirection.y * gravityDir > 0)
                playerRb.linearVelocity = new Vector2(playerRb.linearVelocity.x, 0);

            cancelMovementEnums.RemoveCancelMovementType(CancelMovementEnums.CancelMovementType.Dash);
        }

        public void IncreaseDashCharge(int amount = 1) => playerData.dashData.dashStruct.IncreaseCharge(amount);
        public void IncreaseDashDuration(float amount = 0.5f) => playerData.dashData.ModifyDuration(amount);
        public void IncreaseDashSpeed(float amount = 1f) => playerData.dashData.ModifySpeed(amount);
    }
}

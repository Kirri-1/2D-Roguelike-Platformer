using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using DebugN;
using Player.Checks;
using Player.Movement.SharedProperties;
using Player.PowerUps.Blockers;
using Level.Rules;
using Player.Data;
using Player.InputManagerN;
using Player.Movement.Core;

namespace Player.PowerUps
{
    [RequireComponent(typeof(CapsuleCollider2D))]
    public class Blink : AbstractPlayerAbilities
    {
        CapsuleCollider2D playerCollider;

        InputAction blinkAction;
        InputAction moveAction;
        bool blinkRequested = false;

        Coroutine blinkCoroutine;

        private RaycastHit2D[] blinkHits = new RaycastHit2D[64];
        private ContactFilter2D blinkFilter = ContactFilter2D.noFilter;

        protected override void Awake()
        {
            base.Awake();
            playerCollider = GetComponent<CapsuleCollider2D>();
            blinkAction = inputManager.PlayerInput.Player.Blink;
            moveAction = inputManager.PlayerInput.Player.Movement;
            blinkFilter.useTriggers = true;
        }

        void Update()
        {
            if (blinkAction.triggered)
            {
                blinkRequested = true;
                return;
            }
            ResetBlink();
        }

        private void FixedUpdate()
        {
            if (!blinkRequested)
                return;
            var levelData = LevelRulesScript.Instance.MovementStruct().blinkData.blinkStruct;
            if (!playerData.blinkData.blinkStruct.CanUseAbility(levelData.MaxCharges))
            {
                blinkRequested = false;
                return;
            }
            if(cancelMovementEnums.cancelMovementType != CancelMovementEnums.CancelMovementType.None)
            {
                blinkRequested = false;
                return;
            }

            BlinkVoid(SetBlinkDistance());
            blinkRequested = false;
        }

        float SetBlinkDistance()
        {
            var levelData = LevelRulesScript.Instance.MovementStruct().blinkData;
            return Mathf.Min(playerData.blinkData.TotalDistance(), levelData.BlinkDistance);
        }

        void BlinkVoid(float blinkDistance)
        {
            if (DebugMode.DebugModeActive)
                Debug.Log("Blink attempted");

            cancelMovementEnums.AddCancelMovementType(CancelMovementEnums.CancelMovementType.Blink);
            Vector2 moveInput = moveAction.ReadValue<Vector2>();

            Vector2 blinkDirection = GetBlinkDirection(moveInput);
            Vector2 moveDirection = blinkDirection.normalized;

            Vector2 castSize = new Vector2(playerCollider.bounds.size.x, playerCollider.bounds.size.y * 0.9f);
            float castDistance = blinkDistance;

            int hitCount = Physics2D.BoxCast(transform.position, castSize, 0f, moveDirection, blinkFilter, blinkHits, castDistance);

            RaycastHit2D closestBlocker = new RaycastHit2D();
            bool foundBlocker = false;
            bool foundHardBlocker = false;
            float shortestDistance = float.MaxValue;

            for (int i = 0; i < hitCount; i++)
            {
                if (blinkHits[i].collider == playerCollider) continue;

                if (blinkHits[i].collider.TryGetComponent(out BlinkBlocker blinkBlocker))
                {
                    if (blinkBlocker.CompletelyBlock)
                    {
                        if(blinkHits[i].distance < shortestDistance)
                        {
                            shortestDistance = blinkHits[i].distance;
                            closestBlocker = blinkHits[i];
                            foundBlocker = false;
                            foundHardBlocker = true;
                        }
                        if (DebugMode.DebugModeActive)
                            Debug.Log("Hard BlinkBlocker hit", blinkHits[i].collider.gameObject);
                        continue;
                    }

                    if (blinkHits[i].distance < shortestDistance)
                    {
                        shortestDistance = blinkHits[i].distance;
                        closestBlocker = blinkHits[i];
                        foundBlocker = true;
                    }
                }
            }
            if (!foundBlocker && foundHardBlocker)
            {
                cancelMovementEnums.RemoveCancelMovementType(CancelMovementEnums.CancelMovementType.Blink);
                return;
            }

            Vector2 newLocation;
            if (foundBlocker)
            {
                newLocation = (Vector2)transform.position + moveDirection * shortestDistance;
                if (shortestDistance < playerData.blinkData.BlinkDistanceCheck)
                {
                    cancelMovementEnums.RemoveCancelMovementType(CancelMovementEnums.CancelMovementType.Blink);
                    if (DebugMode.DebugModeActive)
                        Debug.Log("Blink cancelled due to short distance");
                    return;
                }
                transform.position = newLocation;
            }
            else
            {
                newLocation = (Vector2)transform.position + moveDirection * castDistance;
                transform.position = newLocation;
            }
            playerRb.linearVelocity = Vector2.zero;

            if (DebugMode.DebugModeActive)
                Debug.Log("Blinked in direction: " + moveDirection);

            if (blinkCoroutine != null)
            {
                StopCoroutine(blinkCoroutine);
            }
            blinkCoroutine = StartCoroutine(BlinkCoroutine());
            playerData.blinkData.blinkStruct.ConsumeCharge();
        }

        void ResetBlink()
        {
            if (groundCheck.isGrounded)
            {
                playerData.blinkData.blinkStruct.ResetCharges();
            }
        }

        IEnumerator BlinkCoroutine()
        {
            yield return new WaitForSeconds(playerData.blinkData.BlinkDuration);
            cancelMovementEnums.RemoveCancelMovementType(CancelMovementEnums.CancelMovementType.Blink);
        }
        Vector2 GetBlinkDirection(Vector2 moveInput)
        {
            if (moveInput == Vector2.zero)
                return Vector2.right;
            return moveInput;
        }

        public void IncreaseBlinkCount(int amount = 1) => playerData.blinkData.blinkStruct.IncreaseCharge(amount);
        public void ModifyBlinkDistance(float amount = 1f) => playerData.blinkData.ModifyDistance(amount);
    }
}
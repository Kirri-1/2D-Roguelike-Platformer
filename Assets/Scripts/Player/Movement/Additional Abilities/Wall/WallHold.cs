using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

namespace Player.Movement.AdditionalAbilities.WallAbilities.Hold
{
    public class WallHold : AbstractWallComponent
    {
        InputAction holdAction;
        bool wallGrabRequested = false;
        [SerializeField]
            private int consumeCharge = 0; //TODO: make this float a static number that actually consumes a decent amount of stamina
        protected override void Awake()
        {
            base.Awake();
            holdAction = inputManager.PlayerInput.Player.WallGrab;
        }

        protected override void OnCollisionEnter2D(Collision2D collision)
        {
            base.OnCollisionEnter2D(collision);
            if (currentWall == null)
                return;
            
        }
        protected override void OnCollisionExit2D(Collision2D collision)
        {
            base.OnCollisionExit2D(collision);
        }

        private void OnEnable()
        {
            holdAction.Enable();
        }
        private void OnDisable()
        {
            holdAction.Disable();
        }

        protected override void Update()
        {
            base.Update();
            if (!holdAction.IsPressed())
                return;
            wallGrabRequested = true;
        }

        private void FixedUpdate()
        {
            if (!wallGrabRequested)
                return;
            Debug.Log("Wall grab requested");
            if (!playerData.staminaData.HasStamina())
            {
                wallGrabRequested = false;
                Debug.Log("Player doesn't have stamina");
                return;
            }
            if (currentWall == null)
            {
                Debug.Log("Player isn't touching a wall");
                return;
            }
            WallGrab();
            wallGrabRequested = false; //since I have it here, I don't really need it in WallGrab
        }

        void WallGrab()
        {
            if(wallState.WallState != WallStateComponent.WallStateType.Hold)
                wallState.UpdateWallState(WallStateComponent.WallStateType.Hold);

            if ((wallState.WallState & WallStateComponent.WallStateType.Climbing) == 0
                && (wallState.WallState & WallStateComponent.WallStateType.Sliding) == 0)
            {
                playerRb.linearVelocity = Vector2.zero;
                int finalConsumeCharge = (int)(playerData.staminaData.TotalStamina() * (consumeCharge / 100f) / 50f);
                //int finalConsumeCharge = Mathf.Max((int)(playerData.staminaData.TotalStamina() * (consumeCharge / 100f) / 50f), 1);
                playerData.staminaData.staminaStruct.ConsumeCharge(finalConsumeCharge);
            }
            else
            {
                Debug.Log("Player is already climbing or sliding, not consuming stamina");
            }
        }
    }
}

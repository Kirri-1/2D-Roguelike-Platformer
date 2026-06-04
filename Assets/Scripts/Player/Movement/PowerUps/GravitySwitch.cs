using UnityEngine;
using Player.Data;
using Player.Movement.Core;

namespace Player.Movement.PowerUps
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(PlayerData))]
    public class GravitySwitch : AbstractPlayerAbilities
    {
        protected override void Awake()
        {
            base.Awake();
        }
        public void SetGravity(float gravityScale)
        {
            playerData.gravityData.SetGravity(gravityScale);
            playerRb.gravityScale = playerData.gravityData.GravityScale;
        }

        public void ResetGravity()
        {
            playerData.gravityData.SetGravity(playerData.gravityData.DefaultGravityScale);
            playerRb.gravityScale = playerData.gravityData.GravityScale;
        }

        public void ModifyGravity(float amount) //different from SetGravity as it adds to the current gravity scale instead
                                                //of setting it to a specific value, allowing for more dynamic adjustments.
        {
            playerData.gravityData.ModifyGravityScale(amount);
            playerRb.gravityScale = playerData.gravityData.GravityScale;
        }

#if UNITY_EDITOR
        private void Update()
        {
            if (SetGravityBool && playerRb.gravityScale != playerData.gravityData.GravityScale)
            {
                SetGravity(playerData.gravityData.GravityScale);
            }
        }
        public bool SetGravityBool;
#endif
    }
}

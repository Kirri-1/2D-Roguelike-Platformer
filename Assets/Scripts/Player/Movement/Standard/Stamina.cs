using UnityEngine;

namespace Player.Movement.Structs
{
    [System.Serializable]
    public struct StaminaData
    {
        [System.Flags]
        public enum PlayerState
        {
            NotOnWall = 0,
            Climbing = 1 << 0,
            WallJump = 1 << 1,
        }
        public MovementStruct staminaStruct;
        public PlayerState state;
        public StatBuffs staminaStatBuffs;

        public float TotalStamina() => staminaStruct.MaxCharges + staminaStatBuffs.TotalBuffs();

        public bool HasStamina() => staminaStruct.CurrentCharge < TotalStamina();
    }
}

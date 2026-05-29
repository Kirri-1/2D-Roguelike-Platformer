using Newtonsoft.Json;
using UnityEngine;

namespace Player.Movement.Structs
{
    [System.Serializable]
    public struct JumpData
    {
        public MovementStruct jumpStruct;

        [JsonProperty("JumpForce")]
        [SerializeField]
        float jumpForce;
        [JsonIgnore]
        public float JumpForce => jumpForce;

        public void ModifyJumpForce(float amount)
        {
            jumpForce += amount;
        }
        public void IncreaseCharge(int amount)
        {
            jumpStruct.IncreaseCharge(amount);
        }
        public void Unlock() => jumpStruct.Unlock();

        public void SetDefaults()
        {
            jumpForce = 15f;
            jumpStruct.SetDefaults(1, true);
        }
    }
}

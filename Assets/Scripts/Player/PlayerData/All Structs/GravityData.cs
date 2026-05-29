using Newtonsoft.Json;
using UnityEngine;

namespace Player.Movement.Structs
{
    [System.Serializable]
    public struct GravityData
    {
        public MovementStruct gravityStruct;

        [JsonProperty("GravityScale")]
        [SerializeField]
        float gravityScale;
        [JsonIgnore]
        public float GravityScale => gravityScale;

        [JsonProperty("DefaultGravityScale")]
        [SerializeField]
        float defaultGravityScale;
        [JsonIgnore]
        public float DefaultGravityScale => defaultGravityScale;

        public float ModifyGravityScale(float amount)
        {
            gravityScale += amount;
            return gravityScale;
        }
        public float SetGravity(float newGravityScale)
        {
            gravityScale = newGravityScale;
            return gravityScale;
        }

        [JsonProperty("IsUnlocked")]
        [SerializeField]
        bool isUnlocked;
        [JsonIgnore]
        public bool IsUnlocked => isUnlocked;

        public void Unlock() => isUnlocked = true;

        public void SetDefaults()
        {
            defaultGravityScale = 3f;
            gravityScale = defaultGravityScale;
        }
    }
}
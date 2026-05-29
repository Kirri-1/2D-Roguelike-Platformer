using Newtonsoft.Json;
using UnityEngine;

namespace Player.Movement.Structs
{
    [System.Serializable]
    public struct MovementStruct
    {
        [JsonProperty("MaxCharges")]
        [SerializeField]
        int maxCharges;
        [JsonIgnore]
        public int MaxCharges => maxCharges;
        [JsonProperty("CurrentCharge")]
        [SerializeField]
        int currentCharge;
        [JsonIgnore]
        public int CurrentCharge => currentCharge;

        public bool HasCharges => currentCharge > 0;

        [JsonProperty("IsUnlocked")]
        [SerializeField]
        bool isUnlocked;
        [JsonIgnore]
        public bool IsUnlocked => isUnlocked;

        public void ResetCharges() => currentCharge = maxCharges;
        public void ConsumeCharge()
        {
            currentCharge = Mathf.Max(currentCharge - 1, 0);
        }

        public void IncreaseCharge(int amount = 1)
        {
            maxCharges += amount;
            currentCharge = maxCharges;
        }

        public void Unlock() => isUnlocked = true;
        public void Lock() => isUnlocked = false;

        public void SetDefaults(int defaultCharges, bool unlock = false)
        {
            maxCharges = defaultCharges;
            currentCharge = maxCharges;
            isUnlocked = unlock;
        }
    }
}

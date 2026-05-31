using Newtonsoft.Json;
using Player.Movement.SharedProperties;
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

        public bool HasCharges => currentCharge < maxCharges;

        [JsonProperty("IsUnlocked")]
        [SerializeField]
        bool isUnlocked;
        [JsonIgnore]
        public bool IsUnlocked => isUnlocked;

        public void ResetCharges() => currentCharge = 0;
        public void ConsumeCharge(int amount = 1)
        {
            currentCharge = Mathf.Min(currentCharge + amount, maxCharges);
        }

        public void IncreaseCharge(int amount = 1)
        {
            maxCharges += amount;
        }

        public void RecoverCharge(int amount = 1)
        {
            if (amount <= 0)
                return;
            currentCharge = Mathf.Max(currentCharge - amount, 0);
        }

        public void Unlock() => isUnlocked = true;
        public void Lock() => isUnlocked = false;

        public void SetDefaults(int defaultCharges, bool unlock = false)
        {
            maxCharges = defaultCharges;
            currentCharge = 0;
            isUnlocked = unlock;
        }

        public bool CanUseAbility(int levelMaxCharges)
        {
            if (!isUnlocked) return false;
            int maxAllowed = Mathf.Min(levelMaxCharges, maxCharges);
            if (currentCharge >= maxAllowed) return false;
            return true;
        }
    }
}

using Newtonsoft.Json;
using UnityEngine;

namespace Player.Movement.Structs
{
    [System.Serializable]
    public struct DashData
    {
        public MovementStruct dashStruct;
        [JsonProperty("DashSpeed")]
        [SerializeField]
        float dashSpeed;
        [JsonIgnore]
        public float DashSpeed => dashSpeed;
        [JsonProperty("DashDuration")]
        [SerializeField]
        float dashDuration;
        [JsonIgnore]
        public float DashDuration => dashDuration;

        public StatBuffs dashSpeedBuff;
        public StatBuffs dashDurationBuff;

        public void ModifySpeed(float amount)
        {
            dashSpeed += amount;
        }

        public void IncreaseCharge(int amount)
        {
            dashStruct.IncreaseCharge(amount);
        }

        public void ModifyDuration(float amount)
        {
            dashDuration += amount;
        }

        public void Unlock() => dashStruct.Unlock();

        public void SetDefaults()
        {
            dashSpeed = 40f;
            dashDuration = 0.15f;
            dashStruct.SetDefaults(1);
        }

        public float TotalSpeed()
        {
            return dashSpeed + dashSpeedBuff.TotalBuffs();
        }

        public float TotalDuration()
        {
            return dashDuration + dashDurationBuff.TotalBuffs();
        }

        public int MaxCharges() => dashStruct.MaxCharges;
        public int CurrentCharge() => dashStruct.CurrentCharge;
    }
}
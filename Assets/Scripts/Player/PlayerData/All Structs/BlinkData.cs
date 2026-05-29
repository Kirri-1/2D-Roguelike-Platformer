using Newtonsoft.Json;
using UnityEngine;
using Player.Movement.Structs;

[System.Serializable]
public struct BlinkData
{
    public MovementStruct blinkStruct;
    [JsonProperty("BlinkDistance")]
    [SerializeField]
    float blinkDistance;
    [JsonIgnore]
    public float BlinkDistance => blinkDistance;

    [JsonProperty("BlinkDuration")]
    [SerializeField]
    float blinkDuration;
    [JsonIgnore]
    public float BlinkDuration => blinkDuration;

    [JsonProperty("BlinkDistanceCheck")]
    [SerializeField]
    float blinkDistanceCheck;
    [JsonIgnore]
    public float BlinkDistanceCheck => blinkDistanceCheck;

    public void ModifyDistance(float amount)
    {
        blinkDistance += amount;
    }
    public void IncreaseCharge(int amount)
    {
        blinkStruct.IncreaseCharge(amount);
    }

    public void Unlock() => blinkStruct.Unlock();

    public void SetDefaults()
    {
        blinkDistance = 10f;
        blinkDuration = 0.3f;
        blinkDistanceCheck = 5f;
        blinkStruct.SetDefaults(1);
    }
}

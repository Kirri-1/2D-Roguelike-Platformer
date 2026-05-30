using Newtonsoft.Json;
using UnityEngine;

[System.Serializable]
public struct MovementData
{
    [JsonProperty("MovementSpeed")]
    [SerializeField]
    float movementSpeed;
    [JsonIgnore]
    public float MovementSpeed => movementSpeed;

    [JsonProperty("IsUnlocked")]
    [SerializeField]
    bool isUnlocked;
    [JsonIgnore]
    public bool IsUnlocked => isUnlocked;

    public StatBuffs movementSpeedBuff;

    public void ModifyMovementSpeed(float amount)
    {
          movementSpeed += amount;
    }

    public void SetMovementSpeed(float newSpeed)
    {
        movementSpeed = newSpeed;
    }

    public void Unlock() => isUnlocked = true;
    public void Lock() => isUnlocked = false;

    public void SetDefaults()
    {
        movementSpeed = 10f;
        isUnlocked = true;
    }

    public float TotalSpeed()
    {
        return movementSpeed + movementSpeedBuff.TotalBuffs();
    }
}

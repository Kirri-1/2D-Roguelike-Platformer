using UnityEngine;

[System.Serializable]
public struct BlinkData
{
    public MovementStruct blinkStruct;
    [SerializeField]
    float blinkDistance;
    public float BlinkDistance => blinkDistance;
    [SerializeField]
    float blinkDuration;
    public float BlinkDuration => blinkDuration;
    [SerializeField]
    float blinkDistanceCheck;
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

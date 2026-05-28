using UnityEngine;

[System.Serializable]
public struct BlinkData
{
    MovementStruct blinkStruct;
    [SerializeField]
    float blinkDistance;
    public float BlinkDistance => blinkDistance;
    [SerializeField]
    float blinkDuration;
    public float BlinkDuration => blinkDuration;


    public void IncreaseDistance(float amount)
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
        blinkStruct.SetDefaults(1);
    }
}

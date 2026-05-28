using UnityEngine;

[System.Serializable]
public struct DashData
{
    MovementStruct dashStruct;
    [SerializeField]
    float dashSpeed;
    public float DashSpeed => dashSpeed;
    [SerializeField]
    float dashDuration;
    public float DashDuration => dashDuration;

    public void IncreaseSpeed(float amount)
    {
        dashSpeed += amount;
    }

    public void IncreaseCharge(int amount)
    {
         dashStruct.IncreaseCharge(amount);
    }

    public void IncreaseDuration(float amount)
    {
        dashDuration += amount;
    }

    public void Unlock() => dashStruct.Unlock();

    public void SetDefaults()
    {
         dashSpeed = 20f;
         dashDuration = 0.15f;
         dashStruct.SetDefaults(1);
    }
}

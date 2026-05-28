using UnityEngine;

[System.Serializable]
public struct DashData
{
    public MovementStruct dashStruct;
    [SerializeField]
    float dashSpeed;
    public float DashSpeed => dashSpeed;
    [SerializeField]
    float dashDuration;
    public float DashDuration => dashDuration;

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
}

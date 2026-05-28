using UnityEngine;

[System.Serializable]
public struct JumpData
{
    public MovementStruct jumpStruct;
    [SerializeField]
    float jumpForce;
    public float JumpForce => jumpForce;

    public void ModifyJumpForce(float amount)
    {
        jumpForce += amount;
    }
    public void IncreaseCharge(int amount)
    {
        jumpStruct.IncreaseCharge(amount);
    }
    public void Unlock() => jumpStruct.Unlock();

    public void SetDefaults()
    {
        jumpForce = 15f;
        jumpStruct.SetDefaults(1, true);
    }
}

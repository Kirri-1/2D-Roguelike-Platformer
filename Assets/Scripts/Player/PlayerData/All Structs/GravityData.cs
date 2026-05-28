using UnityEngine;

[System.Serializable]
public struct GravityData
{
    public MovementStruct gravityStruct;
    [SerializeField]
    float gravityScale;
    public float GravityScale => gravityScale;

    [SerializeField]
    float defaultGravityScale;
    public float DefaultGravityScale => defaultGravityScale;

    public float ModifyGravityScale(float amount)
    {
        gravityScale += amount;
        return gravityScale;
    }
    public float SetGravity(float newGravityScale)
    {
        gravityScale = newGravityScale;
        return gravityScale;
    }
    [SerializeField]
    bool isUnlocked;
    public bool IsUnlocked => isUnlocked;

    public void Unlock() => isUnlocked = true;

    public void SetDefaults()
    {
        defaultGravityScale = 3f;
        gravityScale = defaultGravityScale;
    }
}

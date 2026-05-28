using UnityEngine;

[System.Serializable]
public struct MovementData
{
    [SerializeField]
    float movementSpeed;
    public float MovementSpeed => movementSpeed;

    [SerializeField]
    bool isUnlocked;
    public bool IsUnlocked => isUnlocked;

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
}

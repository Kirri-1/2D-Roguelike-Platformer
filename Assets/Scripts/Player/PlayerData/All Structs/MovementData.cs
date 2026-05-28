using UnityEngine;

[System.Serializable]
public struct MovementData
{
    [SerializeField]
    float movementSpeed;
    public float MovementSpeed => movementSpeed;

    public void ModifyMovementSpeed(float amount)
    {
          movementSpeed += amount;
    }

    public void SetMovementSpeed(float newSpeed)
    {
        movementSpeed = newSpeed;
    }

    public void SetDefaults()
    {
        movementSpeed = 5f;
    }
}

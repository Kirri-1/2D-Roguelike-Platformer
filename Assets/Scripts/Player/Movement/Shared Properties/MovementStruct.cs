using UnityEngine;

[System.Serializable]
public struct MovementStruct
{
    [SerializeField]
    int maxCharges;
    public int MaxCharges => maxCharges;
    [SerializeField]
    int currentCharge;
    public int CurrentCharge => currentCharge;

    public bool HasCharges => currentCharge > 0;

    [SerializeField]
    bool isUnlocked;
    public bool IsUnlocked => isUnlocked;

    public void ResetCharges() => currentCharge = maxCharges;
    public void ConsumeCharge()
    {
        currentCharge = Mathf.Max(currentCharge - 1, 0);
    }

    public void IncreaseCharge(int amount = 1)
    {
        maxCharges += amount;
        currentCharge = maxCharges;
    }

    public void Unlock() => isUnlocked = true;
    public void Lock() => isUnlocked = false;

    public void SetDefaults(int defaultCharges, bool unlock = false)
    {
        maxCharges = defaultCharges;
        currentCharge = maxCharges;
        isUnlocked = unlock;
    }
}

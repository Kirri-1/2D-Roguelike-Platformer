using UnityEngine;

[System.Serializable]
public struct MovementStruct
{
    public int maxCharges;
    public int currentCharge;

    public bool HasCharges => currentCharge > 0;

    public void ResetCharges() => currentCharge = maxCharges;
    public void ConsumeCharge()
    {
        currentCharge = Mathf.Max(currentCharge - 1, 0);
    }
}

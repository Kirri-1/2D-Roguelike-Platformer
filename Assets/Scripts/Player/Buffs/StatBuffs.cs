using Newtonsoft.Json;
using UnityEngine;

[System.Serializable]
public struct StatBuffs
{
    [SerializeField]
    [JsonProperty("TempBuffs")]
    float tempBuffs;
    [JsonIgnore]
    public float TempBuffs => tempBuffs;

    [SerializeField]
    [JsonProperty("PermBuffs")]
    float permBuffs;
    [JsonIgnore]
    public float PermBuffs => permBuffs;

    public void ModifyTempBuffs(float amount) => tempBuffs += amount;
    public void ResetTempBuffs() => tempBuffs = 0;

    public void ModifyPermBuffs(float amount) => permBuffs += amount;
    public void ResetPermBuffs() => permBuffs = 0;

    public float TotalBuffs()
    {
        return permBuffs + tempBuffs;
    }
}

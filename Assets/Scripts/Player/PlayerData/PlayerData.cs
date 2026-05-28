using UnityEngine;

public class PlayerData : MonoBehaviour
{
    [Header("Movement Structs")]
    public BlinkData blinkData;
    public DashData dashData;
    public GravityData gravityData;
    public JumpData jumpData;
    public MovementData movementData;

    private void Awake()
    {
        bool hasData = LoadPlayerData();

        if (!hasData)
        {
            blinkData.SetDefaults();
            dashData.SetDefaults();
            gravityData.SetDefaults();
            jumpData.SetDefaults();
            movementData.SetDefaults();
        }
    }

    public bool SavePlayerData()
    {
        return false; //this will pretty much say if the save failed or succeeded in the future when it's written out
    }

    public bool LoadPlayerData()
    {
        return false;//this will pretty much say if the load failed or succeeded in the future when it's written out
    }

    public void SetPlayerData()
    { }
}

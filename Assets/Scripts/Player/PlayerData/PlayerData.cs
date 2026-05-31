using System;
using System.IO;
using UnityEngine;
using Newtonsoft.Json;
using Player.Movement.Structs;
using Player.Currency.Structs;

namespace Player.Data
{
    [System.Serializable]
    public class PlayerData : MonoBehaviour
    {
        [Header("Movement Structs")]
        public BlinkData blinkData;
        public DashData dashData;
        public GravityData gravityData;
        public JumpData jumpData;
        public MovementData movementData;
        public StaminaData staminaData;

        [Header("Currency Struct")]
        public CurrencyData currencyData;

        [SerializeField]
        bool doSaveBool = true;
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
                currencyData.SetDefaults();
                bool saveSuccess = SavePlayerData();
                if (saveSuccess)
                {
                    Debug.Log("Player data initialized and saved successfully.");
                }
            }
            if (doSaveBool)
                SavePlayerData();
        }

        public bool SavePlayerData()
        {
            try
            {
                var saveData = new
                {
                    blinkData,
                    dashData,
                    gravityData,
                    jumpData,
                    movementData,
                    currencyData
                };
                string jsonData = JsonConvert.SerializeObject(saveData, Formatting.Indented);
                File.WriteAllText(SaveSystem.FilePath, jsonData);
                return true;
            }
            catch (Exception ex)
            {
                Debug.LogError("Failed to save player data: " + ex.Message);
                return false;
            }
        }

        public bool LoadPlayerData()
        {
            try
            {
                if (File.Exists(SaveSystem.FilePath))
                {
                    string jsonData = File.ReadAllText(SaveSystem.FilePath);
                    JsonConvert.PopulateObject(jsonData, this);
                    return true;
                }

                else
                {
                    Debug.LogWarning("Player data file not found at: " + SaveSystem.FilePath);
                    return false;
                }
            }
            catch (Exception ex)
            {
                Debug.LogError("Failed to load player data: " + ex.Message);
                return false;//this will pretty much say if the load failed or succeeded in the future when it's written out
            }
        }

        public void SetPlayerData()
        { }

#if UNITY_EDITOR
        public bool doSave = false;
        private void Update()
        {
            if (doSave)
            {
                bool saveSuccess = SavePlayerData();
                if (saveSuccess)
                {
                    Debug.Log("Player data saved successfully.");
                }
                doSave = false;
            }
        }
#endif
    }

    public static class SaveSystem
    {
        public static string FilePath => Application.persistentDataPath + "/playerdata.json";
    }
}
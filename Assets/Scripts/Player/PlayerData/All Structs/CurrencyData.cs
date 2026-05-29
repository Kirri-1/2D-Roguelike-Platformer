using Newtonsoft.Json;
using UnityEngine;

namespace Player.Currency.Structs
{
    [System.Serializable]
    public struct CurrencyData
    {
        [SerializeField]
        [JsonProperty("CurrencyAmount")]
        int currencyAmount;
        [JsonIgnore]
        public int CurrencyAmount => currencyAmount;

        [JsonProperty("AllowedDebtAmount")]
        [SerializeField]
        int allowedDebtAmount;
        [JsonIgnore]
        public int AllowedDebtAmount => allowedDebtAmount;

        public void SetDefaults()
        {
            currencyAmount = 0;
            allowedDebtAmount = 0;
        }

        public void AddCurrency(int amount)
        {
            if (amount < 0)
            {
                Debug.LogError("Cannot add a negative amount of currency.");
                return;
            }
            currencyAmount += amount;
        }

        public void SubtractCurrency(int amount)
        {
            if (amount < 0)
            {
                Debug.LogError("Cannot subtract a negative amount of currency.");
                return;
            }
            currencyAmount = Mathf.Max(currencyAmount - amount, -allowedDebtAmount);
        }

        public void AddAllowedDebt(int amount)
        {
            if (amount < 0)
            {
                Debug.LogError("Cannot add a negative amount of allowed debt.");
                return;
            }
            allowedDebtAmount += amount;
        }
        public void SubtractAllowedDebt(int amount)
        {
            if (amount < 0)
            {
                Debug.LogError("Cannot subtract a negative amount of allowed debt.");
                return;
            }
            allowedDebtAmount = Mathf.Max(allowedDebtAmount - amount, 0);
        }

        public void ResetCurrency()
        {
            currencyAmount = 0;
        }
    }
}
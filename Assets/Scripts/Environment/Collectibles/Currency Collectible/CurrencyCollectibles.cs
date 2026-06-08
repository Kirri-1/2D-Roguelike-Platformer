using UnityEngine;
using Player.Data;

namespace Environment.Collectibles.CurrencyCollectible
{
    [RequireComponent(typeof(Collider2D))]
    public class CurrencyCollectibles : MonoBehaviour
    {
        Collider2D col;
        [SerializeField]
        [Tooltip("Amount of currency the player will get upon collection")]
        private int currencyValue = 1;

        [SerializeField]
        private bool isTrigger = true;
        private void Awake()
        {
            col = GetComponent<Collider2D>();
            if(isTrigger)
            col.isTrigger = true;
        }

        private void OnTriggerEnter2D(Collider2D collider)
        {
            if (!isTrigger)
                return;

            if (!collider.TryGetComponent(out PlayerData playerData))
                return;
            AddCurrency(playerData);
        }

        void OnCollisionEnter2D(Collision2D collision)
        {
            if (isTrigger)
                return;
            if (!collision.gameObject.TryGetComponent(out PlayerData playerData))
                return;
            AddCurrency(playerData);
        }

        void AddCurrency(PlayerData playerData)
        {
            playerData.currencyData.AddCurrency(currencyValue);
            Debug.Log($"Player earned {currencyValue} currency");
            gameObject.SetActive(false);
        }
    }
}

using UnityEngine;
using Player.Respawn.Owner;
using Player.Data;

namespace Level.Checkpoints
{
    [RequireComponent(typeof(Collider2D))]
    public class Checkpoints : MonoBehaviour
    {
        [SerializeField]
        private bool wasTriggered = false;
        Collider2D col;
        [SerializeField]
        private int checkpointCurrency = 1;

        private void Awake()
        {
            col = GetComponent<Collider2D>();
            col.isTrigger = true;
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (wasTriggered)
                return;
            if (!collision.TryGetComponent(out RespawnOwner respawnOwner))
                return;
            if (!collision.TryGetComponent(out PlayerData playerData))
                return;
            respawnOwner.SetRespawnerTransform(transform);
            playerData?.currencyData.AddCurrency(checkpointCurrency);
            wasTriggered = true;
            gameObject.SetActive(false);
        }
        public void ResetCheckpointTriggered() => wasTriggered = false;

        public void SetCheckpointCurrency(int newCurrencyValue) => checkpointCurrency = newCurrencyValue;
    }
}

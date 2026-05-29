using UnityEngine;
using DebugN;

public class RespawnOwner : Singleton<RespawnOwner>
{
    [SerializeField]
    Transform respawnerTransform;
    [SerializeField]
    Vector2 _respawnOffset;

    [SerializeField]
    bool canSwitchRespawn = false;
    public bool CanSwitchRespawn => canSwitchRespawn;

    public Transform RespawnerTransform => respawnerTransform;

    protected override void Awake()
    {
        base.Awake();
        if (respawnerTransform != null)
            transform.position = respawnerTransform.position;
    }

    public void SetRespawnerTransform(Transform newRespawnerTransform, Vector2 offset = default)
    {
        if(newRespawnerTransform == null)
        {
            if(DebugMode.DebugModeActive)
                Debug.LogError("Respawner transform cannot be null.");
            return;
        }
        respawnerTransform = newRespawnerTransform;
        _respawnOffset = offset;
    }

    public void Respawn(bool isTrueRespawn = true, bool resetVelocity = true) //true respawn means the player died
                                                   //and false respawn means the player is transitioning
    {
        if(TryGetComponent(out Rigidbody2D rb))
        {
            if(resetVelocity)
                rb.linearVelocity = Vector2.zero;
        }
        transform.position = (Vector2)respawnerTransform.position + _respawnOffset;
    }

    public void SetRespawnSwitch(bool canSwitch)
    {
        canSwitchRespawn = canSwitch;
    }
}

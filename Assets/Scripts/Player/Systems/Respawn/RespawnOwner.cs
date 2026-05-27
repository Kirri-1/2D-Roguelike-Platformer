using UnityEngine;

public class RespawnOwner : Singleton<RespawnOwner>
{
    [SerializeField]
    Transform respawnerTransform;
    [SerializeField]
    Vector2 _respawnOffset;
    
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

    public void Respawn(bool isTrueRespawn = true)
    {
        if(TryGetComponent(out Rigidbody2D rb))
        {
            rb.linearVelocity = Vector2.zero;
        }
        transform.position = (Vector2)respawnerTransform.position + _respawnOffset;
    }
}

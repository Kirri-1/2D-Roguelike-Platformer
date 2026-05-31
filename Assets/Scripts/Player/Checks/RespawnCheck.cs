using Player.HurtBox;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class RespawnCheck : MonoBehaviour
{
    [SerializeField]
    Collider2D col;
    private void Awake()
    {
        if(col == null)
            col = GetComponent<Collider2D>();
        col.isTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent(out HurtBoxComponent hurtBox))
        {
            //if(Player is immortal && objectIsNotDeathVoid)
            //return;

            RespawnOwner owner = collision.GetComponentInParent<RespawnOwner>();
            if (owner == null)
                return;

            //later on I can do sounds, animations, etc. Although that may be within the RespawnOwner script instead,
            //with each Respawn having it's own animation and sounds that I pass in through the parameters.
            owner.Respawn();
        }
    }
}

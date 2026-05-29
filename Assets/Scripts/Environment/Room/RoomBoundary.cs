using Player.PowerUps.Blockers;
using UnityEditor.EditorTools;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(BlinkBlocker))]
public class RoomBoundary : MonoBehaviour
{
    [Tooltip("The room this boundary belongs to.")]
    [SerializeField] private Room targetRoom;
    [SerializeField] private Transform targetSpawnPoint;

    private void Awake()
    {
        // Ensure the collider is a trigger
        Collider2D col = GetComponent<Collider2D>();
        col.isTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
       
        if (!collision.CompareTag("Player")) return;

        
        if (targetRoom == null)
        {
            Debug.LogWarning($"RoomBoundary on {gameObject.name} has no Room assigned!");
            return;
        }

        // Report to the Brain (RoomManager) that we have entered a new area
        RoomManager.Instance.SetCurrentRoom(targetRoom, targetSpawnPoint, collision, targetRoom.CameraPosition);
    }
}
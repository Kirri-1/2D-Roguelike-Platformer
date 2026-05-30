using UnityEngine;
using Singleton.SingletonN;
using RoomN.RoomInfo;
using RoomN.RoomBoundary;

namespace RoomN.Manager
{
    public class RoomManager : Singleton<RoomManager>
    {
        public Room CurrentActiveRoom { get; private set; }
        [SerializeField] Camera mainCamera;
        protected override void Awake()
        {
            base.Awake();
            if (mainCamera == null)
            {
                mainCamera = Camera.main;
            }
        }

        public void SetCurrentRoom(Room newRoom, Transform targetSpawnPoint,
            Collider2D collider, Transform cameraTransform)
        {
            if (!collider.CompareTag("Player"))
                return;

            if (!collider.TryGetComponent(out RespawnOwner respawnOwner))
                return;

            if (CurrentActiveRoom == newRoom) return;

            CurrentActiveRoom = newRoom;

            mainCamera.transform.position = cameraTransform.position;

            respawnOwner.SetRespawnerTransform(targetSpawnPoint);
            respawnOwner.Respawn(false);


            // Now you can trigger events here that other systems listen to
            Debug.Log($"Entered room: {newRoom.name}");
        }
    }
}
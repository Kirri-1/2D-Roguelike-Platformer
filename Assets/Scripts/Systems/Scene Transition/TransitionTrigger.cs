using System.Linq;
using UnityEngine;
using Player.Respawn.Owner;

namespace Scenes.Transitions
{
    [RequireComponent(typeof(CircleCollider2D))]
    public class TransitionTrigger : MonoBehaviour
    {
        CircleCollider2D _collider;
        [SerializeField]
        Transform destination;
        GameObject _camera;
        [SerializeField]
        Transform nextRoomCameraPosition;
        private void Awake()
        {
            _collider = GetComponent<CircleCollider2D>();
            _collider.isTrigger = true;
            _camera = GameObject.FindGameObjectWithTag("MainCamera");
            if (!_camera)
            {
                Debug.LogError("No camera found in the scene with the tag 'Camera'. Please assign the correct tag to your camera.");
            }
        }


        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (!collision.CompareTag("Player"))
                return;

            if (collision.TryGetComponent(out RespawnOwner respawnOwner))
            {
                respawnOwner.SetRespawnSwitch(true);
                _camera.transform.position = nextRoomCameraPosition.position;
                //camera transition
            }
        }
    }
}
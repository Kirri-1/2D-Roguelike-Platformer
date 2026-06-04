using UnityEngine;
using Player.Data;
using Player.InputManagerN;
using Player.Movement.SharedProperties;
using Player.Checks;
using Systems.Gravity;

namespace Player.Movement.Core
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(PlayerData))]
    [RequireComponent(typeof(InputManager))]
    [RequireComponent(typeof(CancelMovementEnums))]
    [RequireComponent(typeof(GroundCheck))]
    [RequireComponent(typeof(Gravity))]
    public abstract class AbstractPlayerAbilities : MonoBehaviour
    {
        protected PlayerData playerData;
        protected Rigidbody2D playerRb;
        protected InputManager inputManager;
        protected CancelMovementEnums cancelMovementEnums;
        protected GroundCheck groundCheck;
        protected Gravity gravity;

        protected virtual void Awake()
        {
            playerData = GetComponent<PlayerData>();
            playerRb = GetComponent<Rigidbody2D>();
            inputManager = InputManager.Instance;
            cancelMovementEnums = GetComponent<CancelMovementEnums>();
            groundCheck = GetComponent<GroundCheck>();
            gravity = GetComponent<Gravity>();
        }
    }
}
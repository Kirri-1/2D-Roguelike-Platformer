using UnityEngine;
using Player.Data;
using Player.InputManagerN;
using Player.Movement.SharedProperties;
using Player.Checks;

namespace Player.Movement.Core
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(PlayerData))]
    [RequireComponent(typeof(InputManager))]
    [RequireComponent(typeof(CancelMovementEnums))]
    [RequireComponent(typeof(GroundCheck))]
    public abstract class AbstractPlayerAbilities : MonoBehaviour
    {
        protected PlayerData playerData;
        protected Rigidbody2D playerRb;
        protected InputManager inputManager;
        protected CancelMovementEnums cancelMovementEnums;
        protected GroundCheck groundCheck;

        protected virtual void Awake()
        {
            playerData = GetComponent<PlayerData>();
            playerRb = GetComponent<Rigidbody2D>();
            inputManager = GetComponent<InputManager>();
            cancelMovementEnums = GetComponent<CancelMovementEnums>();
            groundCheck = GetComponent<GroundCheck>();
        }
    }
}
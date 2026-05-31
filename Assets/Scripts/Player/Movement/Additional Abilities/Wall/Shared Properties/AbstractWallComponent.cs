using UnityEngine;
using Player.Data;
using UnityEngine.InputSystem;
using Player.InputManagerN;
using System.Linq;
namespace Player.Movement.AdditionalAbilities
{
    [RequireComponent(typeof(PlayerData))]
    [RequireComponent(typeof(WallStateComponent))]
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(InputManager))]
    public abstract class AbstractWallComponent : MonoBehaviour
    {
        protected PlayerData playerData;
        protected Wall currentWall;
        protected WallStateComponent wallState;
        protected Rigidbody2D playerRb;
        protected InputAction moveAction;
        protected InputManager inputManager;
        protected virtual void Awake()
        {
            inputManager = GetComponent<InputManager>();
            moveAction = inputManager.PlayerInput.Player.Movement;
            playerData = GetComponent<PlayerData>();
            wallState = GetComponent<WallStateComponent>();
            playerRb = GetComponent<Rigidbody2D>();
        }

        protected virtual void Update()
        {
            ResetWallStamina();
        }

        protected virtual void OnCollisionEnter2D(Collision2D collision)
        {
            if (currentWall != null && currentWall.gameObject == collision.gameObject)
                return;
            if (!collision.gameObject.TryGetComponent<Wall>(out Wall thisWall))
                return;
            currentWall = thisWall;
        }

        protected virtual void OnCollisionExit2D(Collision2D collision)
        {
            if (currentWall != null && currentWall.gameObject != collision.gameObject)
                return;
            if (!collision.gameObject.TryGetComponent<Wall>(out Wall thisWall))
                return;
            currentWall = null;
            wallState.UpdateWallState(WallStateComponent.WallStateType.None);
        }

        protected bool IsCorrectWallState(params WallStateComponent.WallStateType[] wallStateTypes)
        {
            if (!wallStateTypes.Contains(wallState.WallState) && wallState.WallState != WallStateComponent.WallStateType.None)
                return false;
            return true;
        }

        protected virtual void ResetWallStamina()
        {
            playerData.staminaData.staminaStruct.ResetCharges();
        }
    }
}

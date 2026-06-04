using System.Collections.Generic;
using Player.Data;
using Player.InputManagerN;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using Systems.Gravity;
namespace Player.Movement.AdditionalAbilities.WallAbilities
{
    [RequireComponent(typeof(PlayerData))]
    [RequireComponent(typeof(WallStateComponent))]
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(InputManager))]
    [RequireComponent(typeof(Gravity))]
    public abstract class AbstractWallComponent : MonoBehaviour
    {
        protected PlayerData playerData;
        protected Wall currentWall;
        protected WallStateComponent wallState;
        protected Rigidbody2D playerRb;
        protected InputAction moveAction;
        protected InputManager inputManager;
        ContactFilter2D wallFilter = ContactFilter2D.noFilter;
        List<Collider2D> nearbyWalls = new List<Collider2D>();
        protected virtual void Awake()
        {
            moveAction = InputManager.Instance.PlayerInput.Player.Movement;
            playerData = GetComponent<PlayerData>();
            wallState = GetComponent<WallStateComponent>();
            playerRb = GetComponent<Rigidbody2D>();
            inputManager = InputManager.Instance;
        }

        protected virtual void Update()
        {
            ResetWallStamina();
        }

        protected virtual void OnCollisionEnter2D(Collision2D collision)
        {
            //Debug.Log("Collided with: " + collision.gameObject.name);
            if (currentWall != null && currentWall.gameObject == collision.gameObject)
            {
                //Debug.Log("Already on this wall, ignoring collision. OR, currentWall isn't null");
                return;
            }
            if (!collision.gameObject.TryGetComponent<Wall>(out Wall thisWall))
            {
                //Debug.Log("Collided object isn't a wall, ignoring collision.");
                return;
            }
            currentWall = thisWall;
        }

        protected Wall GetWall()
        {
            Physics2D.OverlapCircle(transform.position, 1f, wallFilter, nearbyWalls);
            foreach(var obj in nearbyWalls)
            {
                if (obj.gameObject.TryGetComponent<Wall>(out Wall wall))
                    return wall;
            }
            return null;
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

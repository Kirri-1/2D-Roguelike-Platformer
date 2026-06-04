using UnityEngine;
using Singleton.SingletonN;

namespace Player.InputManagerN
{
    public class InputManager : Singleton<InputManager>
    {
        PlayerMovement playerInput;
        public PlayerMovement PlayerInput => playerInput;
        protected override void Awake()
        {
            playerInput = new PlayerMovement();
        }

        private void OnEnable()
        {
            playerInput.Player.Enable();
        }
        private void OnDisable()
        {
            playerInput.Player.Disable();
        }
    }
}

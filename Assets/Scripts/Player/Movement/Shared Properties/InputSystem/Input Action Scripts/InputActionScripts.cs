using UnityEngine;

namespace Player.InputManagerN
{
    public class InputManager : MonoBehaviour
    {
        PlayerMovement playerInput;
        public PlayerMovement PlayerInput => playerInput;
        private void Awake()
        {
            playerInput = new PlayerMovement();
        }

        private void OnEnable()
        {
            playerInput.Enable();
        }
        private void OnDisable()
        {
            playerInput.Disable();
        }
    }
}

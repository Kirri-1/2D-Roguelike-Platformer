using UnityEngine;

namespace Player.Movement.AdditionalAbilities
{
    public class WallStateComponent : MonoBehaviour
    {
        [System.Flags]
        public enum WallStateType
        {
            None = 0,
            Hold = 1 << 0,
            Climbing = 1 << 1,
            Sliding = 1 << 2,
            Jump = 1 << 3
        }

        [SerializeField]
        private WallStateType wallState;
        public WallStateType WallState=> wallState;

        public void UpdateWallState(WallStateType wallStateType) => wallState = wallStateType;
    }
}

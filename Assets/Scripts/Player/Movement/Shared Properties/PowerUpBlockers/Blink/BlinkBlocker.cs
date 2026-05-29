using UnityEngine;

namespace Player.PowerUps.Blockers
{
    public class BlinkBlocker : MonoBehaviour
    {
        [SerializeField]
        private bool completelyBlock = true;
        public bool CompletelyBlock => completelyBlock;
    }
}

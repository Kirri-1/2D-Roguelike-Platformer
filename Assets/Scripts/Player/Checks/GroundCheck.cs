using System.Collections.Generic;
using UnityEngine;
using DebugN;

namespace Player.Checks
{
    public class GroundCheck : MonoBehaviour
    {
        public Transform groundCheck;
        public LayerMask groundMask;
        public bool isGrounded => CheckOverlap(groundMask);

        public LayerMask rechargableMask;
        public bool isRechargable => CheckOverlap(rechargableMask);

        public LayerMask drainChargeMask;
        public bool isDrainingCharge => CheckOverlap(drainChargeMask);
        List<(LayerMask mask, Color color, int priority)> maskColors = new();

        private void Awake()
        {
            maskColors.Add((groundMask, Color.green, 0));
            maskColors.Add((rechargableMask, Color.blue, 2));
            maskColors.Add((drainChargeMask, Color.yellow, 1));
            maskColors.Sort((a, b) => a.priority.CompareTo(b.priority));
        }
        private void OnDrawGizmos()
        {
            if (!DebugMode.DebugModeActive)
                return;
            foreach (var kvp in maskColors)
            {
                if (Physics2D.OverlapCircle(groundCheck.position, 0.3f, kvp.mask))
                {
                    Gizmos.color = kvp.color;
                    Gizmos.DrawWireSphere(groundCheck.position, 0.3f);
                    return;
                }
            }
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(groundCheck.position, 0.3f);
        }

        bool CheckOverlap(LayerMask mask)
        {
            return Physics2D.OverlapCircle(groundCheck.position, 0.3f, mask);
        }
    }
}
using System.Collections.Generic;
using UnityEngine;

namespace Player.Movement.SharedProperties
{
    public class CancelMovementEnums : MonoBehaviour
    {
        [System.Flags]
        public enum CancelMovementType
        {
            None = 0,
            Dash = 1 << 0,
            Jump = 1 << 1,
            Blink = 1 << 2,
            Attack = 1 << 3,
            Stun = 1 << 4,
            Knockback = 1 << 5,
            Cutscene = 1 << 6,
        }
        public CancelMovementType cancelMovementType;

        public void AddCancelMovementType(CancelMovementType type)
        {
            cancelMovementType |= type;
        }

        public void RemoveCancelMovementType(CancelMovementType type)
        {
            cancelMovementType &= ~type;
        }

        public void ResetCancelMovement()
        {
            cancelMovementType = CancelMovementType.None;
        }

        public bool HasAnyFlag(HashSet<CancelMovementType> flags)
        {
            foreach (var flag in flags)
            {
                if ((cancelMovementType & flag) != 0)
                    return true;
            }
            return false;
        }
    }
}
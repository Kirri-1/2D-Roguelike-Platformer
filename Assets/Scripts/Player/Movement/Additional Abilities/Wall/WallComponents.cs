using UnityEngine;

namespace Player.Movement.AdditionalAbilities
{
    [RequireComponent(typeof(WallClimb))]
    [RequireComponent(typeof(WallHold))]
    [RequireComponent(typeof(WallJump))]
    [RequireComponent(typeof(WallStateComponent))]
    public class WallComponents : MonoBehaviour
    { }
}

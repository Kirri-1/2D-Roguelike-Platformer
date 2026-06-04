using UnityEngine;
using Player.Movement.AdditionalAbilities.WallAbilities.Hold;
using Player.Movement.AdditionalAbilities.WallAbilities.Jump;
namespace Player.Movement.AdditionalAbilities.WallAbilities
{
    [RequireComponent(typeof(WallClimb))]
    [RequireComponent(typeof(WallHold))]
    [RequireComponent(typeof(WallJump))]
    [RequireComponent(typeof(WallStateComponent))]
    public class WallComponents : MonoBehaviour
    { }
}

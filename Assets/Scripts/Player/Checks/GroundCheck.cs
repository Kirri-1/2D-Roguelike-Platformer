using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    public Transform groundCheck;
    public LayerMask groundMask;
    public bool isGrounded => Physics2D.CircleCast(groundCheck.position, 0.3f, Vector2.down, 0.1f, groundMask);

    private void OnDrawGizmos()
    {
        if (!DebugMode.DebugModeActive)
            return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, 0.3f);
    }
}

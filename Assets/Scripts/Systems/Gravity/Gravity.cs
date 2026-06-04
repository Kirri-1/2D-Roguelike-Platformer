using UnityEngine;

namespace Systems.Gravity
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Gravity : MonoBehaviour
    {
        [Header("Gravity Settings")]
        [SerializeField]
        [Tooltip("The strength of the gravity force applied to the object.")]
        private float gravityStrength = 20f;
        public float GravityStrength => gravityStrength;
        [SerializeField]
        [Tooltip("The multiplier applied to the object while in the air")]
        private float riseMultiplier = 0.5f;
        [SerializeField]
        [Tooltip("The multiplier applied to the gravity force when the object is falling.")]
        private float fallMultiplier = 3.5f;
        [SerializeField]
        [Tooltip("The maximum downward velocity the object can reach.")]
        private float terminalVelocity = -20f;
        [SerializeField]
        [Tooltip("Whether gravity should be applied to the object.")]
        bool doGravity = true;
        [SerializeField]
        [Tooltip("Whether the jump is being cut short (e.g., when the player releases the jump button).")]
        bool jumpCut = false;
        public bool JumpCut => jumpCut;
        [SerializeField]
        [Tooltip("The multiplier applied to the gravity force when the jump is cut short.")]
        float jumpCutMultiplier = 3f;

        Rigidbody2D objectRb;
        private void Awake()
        {
            objectRb = GetComponent<Rigidbody2D>();
            objectRb.gravityScale = 0f;
        }

        public void SetJumpCut(bool value)
        {
            jumpCut = value;
        }

        private void FixedUpdate()
        {
            if (!doGravity)
                return;

            float multiplier = jumpCut ? jumpCutMultiplier :
                               objectRb.linearVelocity.y < 0 ? fallMultiplier : riseMultiplier;

            objectRb.linearVelocity = new Vector2(
                objectRb.linearVelocity.x,
                Mathf.Max(objectRb.linearVelocity.y - gravityStrength * multiplier * Time.fixedDeltaTime, terminalVelocity)
            );
        }

        public void SetDoGravity(bool value = true)
        {
            doGravity = value;
        }

        public void SetGravityStrength(float value)
        {
            gravityStrength = value;
        }

        public void SetFallMultiplier(float value)
        {
            fallMultiplier = value;
        }

        public void SetTerminalVelocity(float value)
        {
            terminalVelocity = value;
        }

        public void SetRiseMultiplier(float value)
        {
            riseMultiplier = value;
        }

        public void SetJumpCutMultiplier(float value)
        {
            jumpCutMultiplier = value;
        }
    }
}

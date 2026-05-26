using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class Dash : MonoBehaviour
{
    public MovementStruct dashStruct;
    PlayerMovement playerMovement;
    InputAction dashAction;
    InputAction moveAction;
    bool dashRequested = false;
    Rigidbody2D playerRb;
    GroundCheck groundCheck;

    public bool isDashing;

    public float dashSpeed = 20f;
    public float dashDuration = 0.5f;

    [SerializeField]
    private float maxSpeed = 40f; // Maximum speed to prevent excessive velocity

    Coroutine dashCoroutine;
    private void Awake()
    {
        groundCheck = GetComponent<GroundCheck>();
        playerRb = GetComponent<Rigidbody2D>();
        playerMovement = new PlayerMovement();
        dashAction = playerMovement.Player.Dash;
        moveAction = playerMovement.Player.Movement;
    }

    private void OnEnable()
    {
        dashAction.Enable();
        moveAction.Enable();
    }
    private void OnDisable()
    {
        dashAction.Disable();
        moveAction.Disable();
    }

    private void Update()
    {
        if(dashAction.triggered)
        {
            dashRequested = true;
            return;
        }
        ResetDash();
    }
    private void FixedUpdate()
    {
        if(dashRequested)
        {
            DashVoid();
            dashRequested = false;
        }
    }

    void DashVoid()
    {
        if (!dashStruct.HasCharges)
        {
            dashRequested = false;
            return;
        }

        dashStruct.currentCharge--;
        Vector2 moveInput = moveAction.ReadValue<Vector2>();

        if (moveInput == Vector2.zero)
        {
            Debug.Log("Dash attempted with no movement input. Dash cancelled.");
            return; //nothing happened
        }

        Vector2 dashDirection = moveInput.normalized;

        playerRb.linearVelocity = Vector2.zero;
        if(playerRb.linearVelocity.magnitude > maxSpeed)
        {
            playerRb.linearVelocity = playerRb.linearVelocity.normalized * maxSpeed;
        }
        if (dashDirection.y < -0.5f)
        {
            if(DebugMode.DebugModeActive)
                Debug.Log("Stomping downwards!");
            playerRb.AddForce(dashDirection * (dashSpeed * 1.5f), ForceMode2D.Impulse);
        }
        else
        {
            playerRb.AddForce(dashDirection * dashSpeed, ForceMode2D.Impulse);
            if (DebugMode.DebugModeActive)
                Debug.Log($"Dashed in direction: {dashDirection}");
        }
        if(dashCoroutine != null)
        {
            StopCoroutine(dashCoroutine);
            isDashing = false;
        }
        dashCoroutine = StartCoroutine(DashCoroutine());
    }

    void ResetDash()
    {
        if(groundCheck.isGrounded)
        {
            dashStruct.ResetCharges();
        }
    }

    public IEnumerator DashCoroutine()
    {
        isDashing = true;
        yield return new WaitForSeconds(dashDuration);
        isDashing = false;
    }
}

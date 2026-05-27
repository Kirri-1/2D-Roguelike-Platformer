using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CancelMovementEnums))]
public class Dash : MonoBehaviour
{
    public MovementStruct dashStruct;
    PlayerMovement playerMovement;
    InputAction dashAction;
    InputAction moveAction;
    bool dashRequested = false;
    Rigidbody2D playerRb;
    GroundCheck groundCheck;


    public float dashSpeed = 20f;
    public float dashDuration = 0.5f;

    [SerializeField]
    private float maxSpeed = 40f; // Maximum speed to prevent excessive velocity

    Coroutine dashCoroutine;

    CancelMovementEnums cancelMovementEnums;
    private void Awake()
    {
        groundCheck = GetComponent<GroundCheck>();
        playerRb = GetComponent<Rigidbody2D>();
        playerMovement = new PlayerMovement();
        dashAction = playerMovement.Player.Dash;
        moveAction = playerMovement.Player.Movement;
        cancelMovementEnums = GetComponent<CancelMovementEnums>();
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
        if(dashRequested && cancelMovementEnums.cancelMovementType == CancelMovementEnums.CancelMovementType.None)
        {
            if(dashStruct.HasCharges)
            DashVoid();
            dashRequested = false;
        }
    }

    void DashVoid()
    {
        cancelMovementEnums.AddCancelMovementType(CancelMovementEnums.CancelMovementType.Dash);
        Vector2 moveInput = moveAction.ReadValue<Vector2>();
        bool isMoving = moveInput != Vector2.zero;

        Vector2 dashDirection = moveInput.normalized;

        playerRb.linearVelocity = Vector2.zero;
        if (isMoving)
        {
            if (dashDirection.y < -0.5f)
            {
                if (DebugMode.DebugModeActive)
                    Debug.Log("Stomping downwards!");
                playerRb.AddForce(dashDirection * (dashSpeed * 1.5f), ForceMode2D.Impulse);
            }
            else
            {
                playerRb.AddForce(dashDirection * dashSpeed, ForceMode2D.Impulse);
                if (DebugMode.DebugModeActive)
                    Debug.Log($"Dashed in direction: {dashDirection}");
            }
        }
        else
        {
            playerRb.AddForce(Vector2.right * dashSpeed, ForceMode2D.Impulse);
            if (DebugMode.DebugModeActive)
                Debug.Log("Dashed forward with no input direction!");
        }
        if (dashCoroutine != null)
        {
            StopCoroutine(dashCoroutine);
        }
        dashCoroutine = StartCoroutine(DashCoroutine());
        dashStruct.ConsumeCharge();
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
        yield return new WaitForSeconds(dashDuration);
        cancelMovementEnums.RemoveCancelMovementType(CancelMovementEnums.CancelMovementType.Dash);
    }
}

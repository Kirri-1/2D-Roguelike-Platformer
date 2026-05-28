using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

//TODO: Refactor to use DashData struct from PlayerData

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CancelMovementEnums))]
[RequireComponent(typeof(PlayerData))]
public class Dash : MonoBehaviour
{
    PlayerData playerData;
    PlayerMovement playerMovement;

    InputAction dashAction;
    InputAction moveAction;

    bool dashRequested = false;

    Rigidbody2D playerRb;
    GroundCheck groundCheck;

    [Header("Dash Settings")]
    [SerializeField]
    [Tooltip("The speed at which the player will dash.")]
    float dashSpeed = 20f;
    public float DashSpeed => dashSpeed;
    [SerializeField]
    [Tooltip("The duration of the dash action.")]
    float dashDuration = 0.15f;
    public float DashDuration => dashDuration;

    Coroutine dashCoroutine;

    CancelMovementEnums cancelMovementEnums;
    private void Awake()
    {
        playerData = GetComponent<PlayerData>();
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
            if(playerData.dashData.dashStruct.HasCharges && playerData.dashData.dashStruct.IsUnlocked)
            DashVoid();
            dashRequested = false;
        }
    }

    void DashVoid()
    {
        cancelMovementEnums.AddCancelMovementType(CancelMovementEnums.CancelMovementType.Dash);
        if (dashCoroutine != null)
        {
            StopCoroutine(dashCoroutine);
        }
        dashCoroutine = StartCoroutine(DashCoroutine());
        playerData.dashData.dashStruct.ConsumeCharge();
    }

    void ResetDash()
    {
        if(groundCheck.isGrounded)
        {
            playerData.dashData.dashStruct.ResetCharges();
        }
    }

    public IEnumerator DashCoroutine()
    {
        Vector2 dashDirection = moveAction.ReadValue<Vector2>();
        if (dashDirection == Vector2.zero) dashDirection = Vector2.right;
        dashDirection = dashDirection.normalized;

        float elapsed = 0f;
        while (elapsed < playerData.dashData.DashDuration)
        {
            playerRb.linearVelocity = dashDirection * playerData.dashData.DashSpeed;
            elapsed += Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }

        playerRb.linearVelocity = Vector2.zero;
        cancelMovementEnums.RemoveCancelMovementType(CancelMovementEnums.CancelMovementType.Dash);
    }

    public void IncreaseDashCharge(int amount = 1) => playerData.dashData.dashStruct.IncreaseCharge(amount);
    public void IncreaseDashDuration(float amount = 0.5f) => playerData.dashData.ModifyDuration(amount);
    public void IncreaseDashSpeed(float amount = 1f) => playerData.dashData.ModifySpeed(amount);
}

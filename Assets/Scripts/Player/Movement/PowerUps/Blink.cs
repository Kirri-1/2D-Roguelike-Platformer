using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CancelMovementEnums))]
[RequireComponent(typeof(CapsuleCollider2D))]
public class Blink : MonoBehaviour
{
    Rigidbody2D playerRb;
    CapsuleCollider2D playerCollider;
    public MovementStruct blinkStruct;

    PlayerMovement playerMovement;
    InputAction blinkAction;
    InputAction moveAction;
    bool blinkRequested = false;
    GroundCheck groundCheck;

    CancelMovementEnums cancelMovementEnums;

    public float blinkDistance = 10f;
    [SerializeField]
    float blinkDuration = 0.3f;

    Coroutine blinkCoroutine;
    [SerializeField]
    LayerMask ignorePlayer;
    private void Awake()
    {
        playerCollider = GetComponent<CapsuleCollider2D>();
        playerRb = GetComponent<Rigidbody2D>();
        playerMovement = new PlayerMovement();
        blinkAction = playerMovement.Player.Blink;
        moveAction = playerMovement.Player.Movement;
        cancelMovementEnums = GetComponent<CancelMovementEnums>();
        groundCheck = GetComponent<GroundCheck>();
        ignorePlayer = LayerMask.GetMask("Player");
    }

    private void OnEnable()
    {
        blinkAction.Enable();
        moveAction.Enable();
    }
    private void OnDisable()
    {
        blinkAction.Disable();
        moveAction.Disable();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(blinkAction.triggered)
        {
            blinkRequested = true;
            return;
        }
        ResetBlink();
    }

    private void FixedUpdate()
    {
        if(blinkRequested)
        {
            if(blinkStruct.HasCharges)
                BlinkVoid();
            blinkRequested = false;
        }
    }

    void BlinkVoid()
    {
        if (DebugMode.DebugModeActive)
            Debug.Log("Blink attempted");

        cancelMovementEnums.AddCancelMovementType(CancelMovementEnums.CancelMovementType.Blink);
        playerRb.linearVelocity = Vector2.zero;
        Vector2 moveInput = moveAction.ReadValue<Vector2>();

        Vector2 newLocation;
        Vector2 blinkDirection = GetBlinkDirection(moveInput);
        Vector2 moveDirection = blinkDirection.normalized;

        Vector2 castSize = new Vector2(playerCollider.bounds.size.x, playerCollider.bounds.size.y * 0.9f);
        var hit = Physics2D.BoxCast(transform.position, castSize, 0, moveDirection, blinkDistance, ~ignorePlayer);
        if (hit.collider == null)
        {
            newLocation = (Vector2)transform.position + blinkDirection * blinkDistance;
            transform.position = newLocation;
        }
        else
        {
            newLocation = (Vector2)transform.position + blinkDirection * hit.distance;
            if (hit.distance < 5f)
            {
                cancelMovementEnums.RemoveCancelMovementType(CancelMovementEnums.CancelMovementType.Blink);
                if (DebugMode.DebugModeActive)
                    Debug.Log("Blink cancelled due to short distance");
                return;
            }
            transform.position = newLocation;
        }
        if (DebugMode.DebugModeActive)
            Debug.Log("Blinked in direction: " + moveDirection);
        
        if(blinkCoroutine != null)
        {
            StopCoroutine(blinkCoroutine);
        }
        blinkCoroutine = StartCoroutine(BlinkCoroutine());
        blinkStruct.ConsumeCharge();
    }

    void ResetBlink()
    {
        if (groundCheck.isGrounded)
        {
            blinkStruct.ResetCharges();
        }
    }

    IEnumerator BlinkCoroutine()
    {
        yield return new WaitForSeconds(blinkDuration);
        cancelMovementEnums.RemoveCancelMovementType(CancelMovementEnums.CancelMovementType.Blink);
    }
    Vector2 GetBlinkDirection(Vector2 moveInput)
    {
        if (moveInput == Vector2.zero)
            return Vector2.right;
        return moveInput;
    }
}
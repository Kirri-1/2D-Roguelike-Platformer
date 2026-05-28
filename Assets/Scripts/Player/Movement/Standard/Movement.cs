using UnityEngine;
using UnityEngine.InputSystem;

//TODO: Refactor to use MovementData struct from PlayerData

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Dash))]
[RequireComponent(typeof(CancelMovementEnums))]
public class Movement : MonoBehaviour
{
    PlayerMovement playerMovement;
    InputAction moveAction;

    [SerializeField]
    float moveSpeed = 5f;
    public float MoveSpeed => moveSpeed;

    private Rigidbody2D playerRb;
    CancelMovementEnums cancelMovementEnums;
    public CancelMovementEnums.CancelMovementType cancelMovementType = CancelMovementEnums.CancelMovementType.None;

    private void Awake()
    {
        playerMovement = new PlayerMovement();
        moveAction = playerMovement.Player.Movement;
        cancelMovementEnums = GetComponent<CancelMovementEnums>();
    }
    private void OnEnable()
    {
        moveAction.Enable();
    }
    private void OnDisable()
    {
        moveAction.Disable();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerRb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        Move();
    }


    private void Move()
    {
        if (cancelMovementEnums.cancelMovementType != CancelMovementEnums.CancelMovementType.None)
            return;

        Vector2 moveInput = moveAction.ReadValue<Vector2>();
        Vector2 moveVelocity = moveInput * moveSpeed;

        playerRb.linearVelocity = new Vector2(moveVelocity.x, playerRb.linearVelocity.y);
    }

    public void IncreaseMovementSpeed(float amount) => moveSpeed += amount;
}

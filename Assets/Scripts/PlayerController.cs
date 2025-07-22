using UnityEngine;
using UnityEngine.InputSystem; // REQUIRED for new Input System

public class PlayerController : MonoBehaviour
{
    private CharacterController controller;
    private PlayerInput playerInput; // Reference to the PlayerInput component

    // Input Actions references (assign these in Awake or GetComponent)
    private InputAction moveAction;
    private InputAction dashAction;
    private InputAction creepAction;
    private InputAction touchAction;
    private InputAction focusObserveAction;
    private InputAction commandingShoutAction;

    [Header("Movement Speeds")]
    public float strollSpeed = 2.0f;  // Default walk speed
    public float creepSpeed = 1.0f;   // Slower, methodical pace
    public float dashSpeed = 5.0f;    // Fast, decisive movement
    public float rotationSpeed = 10f; // Smooth rotation speed

    private Vector3 currentMoveInput; // Stores normalized movement input
    private float currentMoveSpeed;

    void Awake()
    {
        controller = GetComponent<CharacterController>();
        playerInput = GetComponent<PlayerInput>(); // Get the PlayerInput component

        // Get references to our actions from the "Player" Action Map
        moveAction = playerInput.actions.FindAction("Move");
        dashAction = playerInput.actions.FindAction("Dash");
        creepAction = playerInput.actions.FindAction("Creep");
        touchAction = playerInput.actions.FindAction("Touch");
        focusObserveAction = playerInput.actions.FindAction("FocusObserve");
        commandingShoutAction = playerInput.actions.FindAction("CommandingShout");

        // --- Subscribe to Action Events (for button presses) ---
        // Example for Touch action (you'd add similar for FocusObserve, CommandingShout later)
        // touchAction.performed += ctx => Debug.Log("Touch action performed!"); 
        // This is how you'd trigger your specific ability logic.
    }

    void Start()
    {
        currentMoveSpeed = strollSpeed; // Start with default stroll speed
    }

    void Update()
    {
        // --- Handle Movement Input ---
        // Read the Vector2 input from the Move action
        Vector2 inputVector = moveAction.ReadValue<Vector2>();
        currentMoveInput = new Vector3(inputVector.x, 0, inputVector.y);
        currentMoveInput.Normalize(); // Ensure consistent speed diagonally

        // --- Handle Speed Changes based on Action State ---
        if (dashAction.IsPressed()) // Check if Dash action button is held down
        {
            currentMoveSpeed = dashSpeed;
        }
        else if (creepAction.IsPressed()) // Check if Creep action button is held down
        {
            currentMoveSpeed = creepSpeed;
        }
        else
        {
            currentMoveSpeed = strollSpeed;
        }

        // Apply gravity (CharacterController requires manual gravity)
        if (!controller.isGrounded)
        {
            currentMoveInput.y -= 9.81f * Time.deltaTime; // Simple gravity
        }
        else
        {
            currentMoveInput.y = 0; // Reset Y when grounded
        }

        // Move the character
        controller.Move(currentMoveInput * currentMoveSpeed * Time.deltaTime);

        // --- Handle Rotation ---
        Vector3 lookDirection = new Vector3(currentMoveInput.x, 0, currentMoveInput.z);
        if (lookDirection.magnitude > 0.1f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(lookDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
        }

        // You can also check for button presses for your actions here, e.g.:
        // if (touchAction.WasPressedThisFrame()) { /* Trigger Touch ability */ }
        // if (focusObserveAction.WasPressedThisFrame()) { /* Trigger Focus/Observe ability */ }
        // if (commandingShoutAction.WasPressedThisFrame()) { /* Trigger Commanding Shout ability */ }
    }
}
using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController), typeof(PlayerInput))]
public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float playerSpeed = 2.0f;
    [SerializeField]
    private float jumpHeight = 1.0f;
    [SerializeField]
    private float gravityValue = -9.81f;
    [SerializeField]
    private float rotationSpeed = 5f;

    [SerializeField]
    private float poopCD = 3f;

    [SerializeField]
    private Transform poopSpawn;
    [SerializeField]
    private GameObject poopPrefab;
    [SerializeField]
    private InGameUIHandler uiHandler;
    [SerializeField]
    private CinemachineBrain m_Brain;

    private CharacterController controller;
    private PlayerInput playerInput;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    private Transform cameraTransform;

    private bool canPoop = true;

    private InputAction moveAction;
    private InputAction jumpAction;
    private InputAction poopAction;
    private InputAction menuAction;


    private void Start()
    {
        controller = GetComponent<CharacterController>();
        playerInput = GetComponent<PlayerInput>();
        Cursor.lockState = CursorLockMode.Locked;
        cameraTransform = Camera.main.transform;
        moveAction = playerInput.actions["Movement"];
        jumpAction = playerInput.actions["Jump"];
        poopAction = playerInput.actions["Poop"];
        menuAction = playerInput.actions["Menu"];
    }

    void Update()
    {

        //Check if the player is on the ground
        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        //Movement of the player
        Vector2 input = moveAction.ReadValue<Vector2>();
        Vector3 move = new Vector3(input.x, 0, input.y);
        move = move.x * cameraTransform.right.normalized + move.z * cameraTransform.forward.normalized;
        move.y = 0;
        controller.Move(move * Time.deltaTime * playerSpeed);

        if (move != Vector3.zero)
        {
            gameObject.transform.forward = move;
        }

        // Changes the height position of the player..
        if (jumpAction.triggered && groundedPlayer)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);

        // Rotate towards camera direction
        if (move == Vector3.zero)
        {
            float targetAngle = cameraTransform.eulerAngles.y;
            Quaternion targetRotation = Quaternion.Euler(0, cameraTransform.eulerAngles.y, 0);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }

        // Pooping
        if (poopAction.triggered && canPoop)
        {
            Instantiate(poopPrefab, poopSpawn.position, new Quaternion(0, 0, 0, 0));
            canPoop = false;
            Invoke("PoopCooldown", poopCD);

        }

        //Open's MainMenu
        if (menuAction.triggered)
        {
            uiHandler.OnGamePause();
            Cursor.lockState = CursorLockMode.None;
        }

        if (Time.deltaTime == 0f)
        {
            m_Brain.enabled = false;
        }
        else
        {
            m_Brain.enabled = true;
        }
    }

    void PoopCooldown()
    {
        canPoop = true;
    }
}

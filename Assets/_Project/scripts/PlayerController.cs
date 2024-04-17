using System;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(CharacterController), typeof(PlayerInputs))]
public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;
    // References
    [SerializeField] CharacterController controller;
    [SerializeField] InputReader inputs;
    [SerializeField] Animator animator;
    [SerializeField] AutoSave autosave;

    private Vector3 playerVelocity;
    private Vector3 playerRotation;
    private bool groundedPlayer;
    
    // Settings
    [SerializeField] float walkSpeed = 2.0f;
    [SerializeField] float runSpeed = 5.0f;
    [SerializeField] float crouchSpeed = 1.0f;
    [SerializeField] float jumpHeight = 1.0f;
    [SerializeField] float gravityValue = -9.81f;
    [SerializeField] float rotationSpeed = 8f;

    private Transform mainCam;

    // All states
    private readonly int InitialState = Animator.StringToHash("Locomotion");
    private readonly int MoveX = Animator.StringToHash("MoveX");
    private readonly int MoveZ = Animator.StringToHash("MoveZ");
    private readonly int IsMoving = Animator.StringToHash("isMoving");
    private readonly int IsRunning = Animator.StringToHash("isRunning");
    private readonly int Jump = Animator.StringToHash("Jumping");
    private readonly int Crouch = Animator.StringToHash("Crouch");
    private readonly int Dance = Animator.StringToHash("Dance");

    private readonly int rightTurn = Animator.StringToHash("RightTurn");
    private readonly int leftTurn = Animator.StringToHash("LeftTurn");

    // Shoot Layer
    //private readonly int Shoot = Animator.StringToHash("MagicAttack");
    //private readonly int Shoot = Animator.StringToHash("ShootingArrow");
    private readonly int Shoot = Animator.StringToHash("Shooting");

    // Params
    private const float ZeroF = 0.0f;
    private Vector2 blendVector;
    private Vector2 animVelocity;
    private float smoothTime = .2f;
    private float animTransition = .06f;

    private bool crouched;
    private bool running;
    private bool dancing;
    private bool inputsEnableled;

    private bool isTurning;
    private bool isTurningLeft;
    private bool isTurningRight;

    private void Start()
    {
        mainCam = Camera.main.transform;
        inputsEnableled = true;
        autosave.pointSave = transform.position;
    }
    private void OnEnable()
    {
        inputs.Crouch += HandleCrouch;
        inputs.Dance += HandleDance;
    }


    private void OnDisable()
    {
        inputs.Crouch -= HandleCrouch;
        inputs.Dance -= HandleDance;
    }
    void Update()
    {
        if (inputsEnableled)
        {
            HandleMovement();
            HandleRun();
            HandleShoot();
        }

        if (inputs.pointSave)
        {
            autosave.pointSave = transform.position;
            Debug.Log("Save!" + autosave.pointSave);
        }
    }

    private void CheckGroundedPlayer()
    {
        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0) playerVelocity.y = 0f;
    }

    private void HandleRun()
    {
        if (inputs.isRunning)
        {
            running = true;
            animator.SetBool(IsRunning, true);
        }
        else
        {
            running = false;
            animator.SetBool(IsRunning, false);
        }
    }

    private void HandleShoot()
    {
        if (inputs.shooting)
        {   
            Debug.Log("Shoot");
            animator.CrossFade(Shoot, animTransition);
        }
    }

    private void HandleMovement()
    {
        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0) playerVelocity.y = 0f;
        
        blendVector = Vector2.SmoothDamp(blendVector, inputs.movement, ref animVelocity, smoothTime);
        
        Vector3 move = new Vector3(blendVector.x, 0, blendVector.y);
        move = move.x * mainCam.right.normalized + move.z * mainCam.forward.normalized;
        move.y = ZeroF;

        // different motion vector
        if (crouched) controller.Move(move * (Time.deltaTime * crouchSpeed));
        else if (running) controller.Move(move * (Time.deltaTime * runSpeed));
        else controller.Move(move * (Time.deltaTime * walkSpeed));
        
        if (move.magnitude > ZeroF) animator.SetBool(IsMoving,true);
        
        // animations
        animator.SetFloat(MoveX, blendVector.x);
        animator.SetFloat(MoveZ, blendVector.y);

        // Changes the height position of the player..
        if (inputs.jumping && groundedPlayer)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -0.8f * gravityValue);
            animator.CrossFade(Jump, animTransition);
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);

        // Rotate towards camera direction
        Quaternion targetRotation = Quaternion.Euler(0, mainCam.eulerAngles.y, 0);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    private void HandleCrouch(bool arg0)
    {
        if (inputsEnableled)
        {
            crouched = !crouched;

            if (crouched) animator.CrossFade(Crouch, animTransition);
            else animator.CrossFade(InitialState, animTransition);
        }
    }
    
    private void HandleDance(bool arg0)
    {
        dancing = !dancing;

        if (dancing)
        {
            inputsEnableled = false;
            animator.CrossFade(Dance, animTransition);
        }
        
        else
        {
            inputsEnableled = true;
            animator.CrossFade(InitialState, animTransition);
        }
    }
}
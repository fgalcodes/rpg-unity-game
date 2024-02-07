using System;
using System.Numerics;
using Cinemachine;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class PlayerController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] CharacterController controller;
    [SerializeField] CinemachineVirtualCamera virtualCamera;
    [SerializeField] Animator animator;
    [SerializeField] InputReader input;

    float playerSpeed = 10.0f;
    float jumpSpeed = 2.0f;
    float rotationSpeed = 180.0f;
    float gravityValue = -9.81f;
    float smoothTime = .2f;


    float currentSpeed;
    float velocity;

    const float ZeroF = 0.0f;

    private Vector3 playerVelocity;

    [Header("Variables")]
    private bool movementPressed;
    private bool isJumping;
    public bool isGrounded;
    
    // Animator parameters
    private static readonly int Speed = Animator.StringToHash("Speed");
    private static readonly int Jump = Animator.StringToHash("isJumping");

    private Transform mainCam;

    private void Awake()
    {
        mainCam = Camera.main.transform;
    }

    void Update()
    {
        isGrounded = controller.isGrounded;

        if (controller.isGrounded)
        {
            isJumping = false;
        }
        
        HandleMovement();
        HandleJump();
        UpdateAnimations();
        
    }

    private void UpdateAnimations()
    {
        animator.SetFloat(Speed, currentSpeed);
        animator.SetBool(Jump, isJumping);
    }

    private void HandleJump()
    {
        if (input.Jumping)
        {
            isJumping = true;
            animator.CrossFade("Jumping", smoothTime);
            //animator.SetBool(Jump, isJumping);

            playerVelocity.y += Mathf.Sqrt(jumpSpeed * smoothTime * gravityValue);

            var moveDirection = new Vector3(input.Direction.x, playerVelocity.y, input.Direction.y);
            //moveDirection.y = playerVelocity.y += gravityValue * Time.deltaTime;

            var ajustMovement = moveDirection * (Time.deltaTime * playerSpeed);
            controller.Move(moveDirection);
        }

       

    }

    private void HandleMovement()
    {
        var moveDirection = new Vector3(input.Direction.x, 0, input.Direction.y);

        var ajustDirection = Quaternion.AngleAxis(mainCam.eulerAngles.y, Vector3.up) * moveDirection;

        if (ajustDirection.magnitude > ZeroF)
        {
            var targetRotation = Quaternion.LookRotation(ajustDirection);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            
            var ajustMovement = ajustDirection * (Time.deltaTime * playerSpeed);
            controller.Move(ajustMovement);
            
            currentSpeed = SmoothSpeed(moveDirection.magnitude);

        } else
        {
            currentSpeed = SmoothSpeed(ZeroF);
        }
    }

    private float SmoothSpeed(float value)
    {
        return Mathf.SmoothDamp(currentSpeed, value, ref velocity, smoothTime);
    }
}
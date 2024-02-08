using System;
using System.Numerics;
using Cinemachine;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class OLD_PlayerController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] CharacterController controller;
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
    private bool isjumping;
    public bool isGrounded;
    
    // Animator parameters
    private static readonly int Speed = Animator.StringToHash("Speed");
    private static readonly int Jump = Animator.StringToHash("isjumping");

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
            isjumping = false;
        }
        
        HandleMovement();
        HandleJump();
        UpdateAnimations();
        
    }

    private void UpdateAnimations()
    {
        animator.SetFloat(Speed, currentSpeed);
        animator.SetBool(Jump, isjumping);
    }

    private void HandleJump()
    {
        if (input.jumping)
        {
            isjumping = true;
            animator.CrossFade("jumping", smoothTime);
            //animator.SetBool(Jump, isjumping);

            playerVelocity.y += Mathf.Sqrt(jumpSpeed * smoothTime * gravityValue);

            var movementDir = new Vector3(input.movement.x, playerVelocity.y, input.movement.y);
            //movementDir.y = playerVelocity.y += gravityValue * Time.deltaTime;

            var ajustMovement = movementDir * (Time.deltaTime * playerSpeed);
            controller.Move(movementDir);
        }

       

    }

    private void HandleMovement()
    {
        var movementDir = new Vector3(input.movement.x, 0, input.movement.y);

        var ajustmovement = Quaternion.AngleAxis(mainCam.eulerAngles.y, Vector3.up) * movementDir;

        if (ajustmovement.magnitude > ZeroF)
        {
            var targetRotation = Quaternion.LookRotation(ajustmovement);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            
            var ajustMovement = ajustmovement * (Time.deltaTime * playerSpeed);
            controller.Move(ajustMovement);
            
            currentSpeed = SmoothSpeed(movementDir.magnitude);

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
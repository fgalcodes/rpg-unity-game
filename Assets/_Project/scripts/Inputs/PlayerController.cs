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
    float jumpSpeed = 10.0f;
    float rotationSpeed = 180.0f;

    float currentSpeed;
    float velocity;

    const float ZeroF = 0.0f;

    private Vector2 currentMovement;
    private bool movementPressed;
    
    // Animator parameters
    private static readonly int Speed = Animator.StringToHash("Speed");

    private Transform mainCam;

    private void Awake()
    {
        mainCam = Camera.main.transform;
    }

    void Update()
    {
        HandleMovement();
        HandleJump();
        animator.SetFloat(Speed, currentSpeed);
        
    }

    private void HandleJump()
    {
        if (input.Jumping)
        {
            Vector3 jump = new Vector3(0,20f,0);
            controller.Move(jump * (jumpSpeed * Time.deltaTime));
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
        return Mathf.SmoothDamp(currentSpeed, value, ref velocity, .2f);
    }
}
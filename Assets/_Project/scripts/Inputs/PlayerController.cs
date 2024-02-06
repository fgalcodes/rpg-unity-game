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

    [SerializeField] float playerSpeed = 2.0f;
    [SerializeField] float jumpSpeed = 2.0f;

    float currentSpeed;
    float velocity;

    const float ZeroF = 0.0f;

    private Vector2 currentMovement;
    private bool movementPressed;
    
    // Animator parameters
    private static readonly int Speed = Animator.StringToHash("Speed");

    void Update()
    {
        HandleMovement();
        HandleJump();
        animator.SetFloat(Speed, currentSpeed);
        
    }

    private void HandleJump()
    {
        if (input.Jumping > ZeroF)
        {
            Vector3 jump = new Vector3(0,jumpSpeed,0);
            controller.Move(jump);
        }
    }

    private void HandleMovement()
    {
        Vector3 move = new Vector3(input.Direction.x, 0, input.Direction.y);
        
        if (move.magnitude > ZeroF)
        {
            controller.Move(move * (playerSpeed * Time.deltaTime));
            currentSpeed = Mathf.SmoothDamp(currentSpeed, move.magnitude, ref velocity, .2f);

        } else
        {
            currentSpeed = Mathf.SmoothDamp(currentSpeed, ZeroF, ref velocity, .2f);
        }
    }
}
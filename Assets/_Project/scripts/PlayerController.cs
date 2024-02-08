using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController), typeof(PlayerInputs))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] CharacterController controller;
    [SerializeField] InputReader inputs;
    [SerializeField] Animator animator;
    
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    
    [SerializeField] float playerSpeed = 2.0f;
    [SerializeField] float jumpHeight = 1.0f;
    [SerializeField] float gravityValue = -9.81f;
    [SerializeField] float rotationSpeed = 8f;

    private Transform mainCam;

    private readonly int MoveX = Animator.StringToHash("MoveX");
    private readonly int MoveZ = Animator.StringToHash("MoveZ");
    private readonly int IsRunning = Animator.StringToHash("isRunning");
    private readonly int Jump = Animator.StringToHash("Jumping");
    
    bool jumpAnim;

    private const float ZeroF = 0.0f;
    private Vector2 blendVector;
    private Vector2 animVelocity;
    private float smoothTime = .2f;
    private float animTransition = .15f;

    private void Start()
    {
        mainCam = Camera.main.transform;
    }

    void Update()
    {
        HandleMovement();
    }

    private void HandleMovement()
    {
        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        blendVector = Vector2.SmoothDamp(blendVector, inputs.movement, ref animVelocity, smoothTime);
        
        Vector3 move = new Vector3(blendVector.x, 0, blendVector.y);
        move = move.x * mainCam.right.normalized + move.z * mainCam.forward.normalized;
        move.y = ZeroF;
        controller.Move(move * (Time.deltaTime * playerSpeed));
        
        // animations
        animator.SetFloat(MoveX, blendVector.x);
        animator.SetFloat(MoveZ, blendVector.y);

        // Changes the height position of the player..
        if (inputs.jumping && groundedPlayer)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
            animator.CrossFade(Jump, animTransition);

        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);

        // Rotate towards camera direction
        Quaternion targetRotation = Quaternion.Euler(0, mainCam.eulerAngles.y, 0);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }
}
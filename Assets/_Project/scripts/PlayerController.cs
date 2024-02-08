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

    private const float ZeroF = 0.0f;
    private Transform mainCam;

    bool jumpAnim;

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

        Vector3 move = new Vector3(inputs.movement.x, 0, inputs.movement.y);
        move = move.x * mainCam.right.normalized + move.z * mainCam.forward.normalized;
        move.y = ZeroF;
        controller.Move(move * (Time.deltaTime * playerSpeed));

        // Changes the height position of the player..
        if (inputs.jumping && groundedPlayer)
        {
            animator.CrossFade("Jumping", ZeroF);
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);

        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);

        // Rotate towards camera direction
        Quaternion targetRotation = Quaternion.Euler(0, mainCam.eulerAngles.y, 0);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }
}
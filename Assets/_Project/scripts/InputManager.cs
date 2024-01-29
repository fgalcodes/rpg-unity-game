using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{

    PlayerInputs inputs;
    Animator animator;

    private void Awake()
    {
        inputs = new PlayerInputs();
        animator = GetComponent<Animator>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(GetPlayerMovement().magnitude != 0)
        {
            animator.SetBool("isWalking", true);
        }
        else
        {
            animator.SetBool("isWalking", false);

        }

        animator.SetFloat("moveX", GetPlayerMovement().x);
        animator.SetFloat("moveZ", GetPlayerMovement().y);
    }

    private void OnEnable()
    {
        inputs.Enable();
    }

    private void OnDisable()
    {
        inputs.Disable();
    }

    public Vector2 GetPlayerMovement()
    {
        return inputs.Player.Walk.ReadValue<Vector2>();
    }
}

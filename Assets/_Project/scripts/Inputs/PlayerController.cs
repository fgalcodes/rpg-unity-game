using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] CharacterController controller;
    [SerializeField] Animator animator;
    [SerializeField] InputReader input;

    [SerializeField] float playerSpeed = 2.0f;
    float currentSpeed;
    float velocity;

    const float ZeroF = 0.0f;

    // Animator parameters
    private static readonly int Speed = Animator.StringToHash("Speed");

    void Update()
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

        if (currentSpeed < 0 || currentSpeed > 1)
        {
            currentSpeed = ZeroF;
        }

        animator.SetFloat(Speed, currentSpeed);
    }
}
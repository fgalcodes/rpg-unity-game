using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("References")] [SerializeField]
    CharacterController controller;

    [SerializeField] Animator animator;
    [SerializeField] InputReader input;


    [Header("Settings")] [SerializeField] float playerSpeed = 1.0f;

    private float currentSpeed;

    // Animator parameters
    private static readonly int Speed = Animator.StringToHash("Speed");

    private const float ZeroF = 0.0f;

    private void Awake()
    {
        // controller = gameObject.GetComponent<CharacterController>();
        // animator = gameObject.GetComponent<Animator>();
        // input = gameObject.GetComponent<InputReader>();
    }

    void Update()
    {
        Vector3 move = new Vector3(input.Direction.x, 0, input.Direction.y).normalized;
        controller.Move(move * (Time.deltaTime * playerSpeed));
        animator.SetFloat(Speed, playerSpeed);
    }
}
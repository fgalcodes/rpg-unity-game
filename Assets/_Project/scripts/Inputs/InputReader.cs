using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using static PlayerInputs;

[CreateAssetMenu]
public class InputReader : ScriptableObject, IPlayerActions
{
    public event UnityAction<Vector2> Move = delegate {  };

    private PlayerInputs inputActions;

    public Vector3 Direction => inputActions.Player.Walk.ReadValue<Vector2>();

    void OnEnable()
    {
        if (inputActions == null)
        {
            inputActions = new PlayerInputs();
            inputActions.Player.SetCallbacks(this);
        }    
        inputActions.Enable();
    }
    public void OnWalk(InputAction.CallbackContext context)
    {
        Move.Invoke(context.ReadValue<Vector2>());
    }
}

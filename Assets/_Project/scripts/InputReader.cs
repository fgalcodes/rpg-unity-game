using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using static PlayerInputs;

[CreateAssetMenu]
public class InputReader : ScriptableObject, IPlayerActions
{
    public event UnityAction<Vector2> Move = delegate {  };
    public event UnityAction<Vector2> Look = delegate {  };
    public event UnityAction<bool> Jump = delegate {  };
    public event UnityAction<bool> Aim = delegate {  };
    public event UnityAction<bool> Shoot = delegate {  };
    public event UnityAction<bool> Run = delegate {  };
    public event UnityAction<bool> Crouch = delegate {  };
    public event UnityAction<bool> Dance = delegate {  };

    private PlayerInputs inputActions;
    
    public Vector3 movement => inputActions.Player.Move.ReadValue<Vector2>();
    public Vector3 looking => inputActions.Player.Look.ReadValue<Vector2>();
    public bool jumping => inputActions.Player.Jump.triggered;
    public bool shooting => inputActions.Player.Shoot.triggered;

    public bool aiming => inputActions.Player.Aim.triggered;
    public bool isRunning => inputActions.Player.Run.IsPressed();
    public bool crounching => inputActions.Player.Crouch.triggered;

    void OnEnable()
    {
        if (inputActions == null)
        {
            inputActions = new PlayerInputs();
            inputActions.Player.SetCallbacks(this);
        }
        inputActions.Enable();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        Move.Invoke(context.ReadValue<Vector2>());
    }


    public void OnLook(InputAction.CallbackContext context)
    {
        Look.Invoke(context.ReadValue<Vector2>());

    }
    public void OnJump(InputAction.CallbackContext context)
    {
        Jump.Invoke(context.ReadValueAsButton());
    }

    public void OnAim(InputAction.CallbackContext context)
    {
        Aim.Invoke(context.performed);
    }

    public void OnShoot(InputAction.CallbackContext context)
    {
        Shoot.Invoke(context.ReadValueAsButton());
    }

    public void OnRun(InputAction.CallbackContext context)
    {
        Run.Invoke(context.performed);
    }

    public void OnCrouch(InputAction.CallbackContext context)
    {
        Crouch.Invoke(context.performed);
    }

    public void OnDance(InputAction.CallbackContext context)
    {
        Dance.Invoke(context.performed);
    }
}

using Cinemachine;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class SwitchVCam : MonoBehaviour
{
    [SerializeField] PlayerInputs playerinputs;
    [SerializeField] int priorityBoosCam = 10;

    private CinemachineVirtualCamera _virtualCamera;

    private InputAction aimAction;

    private void Awake()
    {
        _virtualCamera = gameObject.GetComponent<CinemachineVirtualCamera>();
       aimAction = playerinputs.FindAction("Aim");
    }


    private void OnEnable()
    {
        aimAction.performed += _ => StartAim();
        aimAction.canceled += _ => ExitAim();
    }
    private void OnDisable() { 

        aimAction.performed -= _ => StartAim();
        aimAction.canceled -= _ => ExitAim();
    }

    private void StartAim()
    {
        _virtualCamera.Priority += priorityBoosCam;
    }

    private void ExitAim()
    {
        _virtualCamera.Priority -= priorityBoosCam;
    }


}

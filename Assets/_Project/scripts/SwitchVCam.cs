using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Unity.VisualScripting;
using UnityEngine;

public class SwitchVCam : MonoBehaviour
{
    [SerializeField] private InputReader input;
    [SerializeField] private int priorityBoosCam = 10;

    private CinemachineVirtualCamera _virtualCamera;

    private bool trigger;
    private void Awake()
    {
        _virtualCamera = gameObject.GetComponent<CinemachineVirtualCamera>();
    }

  

    void StartAim()
    {
        _virtualCamera.Priority += priorityBoosCam;
    }
    
    void ExitAim()
    {
        _virtualCamera.Priority -= priorityBoosCam;
    }
}

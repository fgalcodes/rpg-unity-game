using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchCamera : MonoBehaviour
{
    [SerializeField] InputReader inputReader;
    [SerializeField] int priorityBoostAmount = 10;
    private CinemachineVirtualCamera virtualCamera;
    private bool isPriorityBoosted = false;

    bool trigger;

    private void Awake()
    {
        virtualCamera = GetComponent<CinemachineVirtualCamera>();
    }
    private void OnEnable()
    {
        inputReader.Aim += StartAim;
    }
    private void OnDisable()
    {
        inputReader.Aim -= StartAim;
    }

    private void StartAim(bool arg0)
    {

        isPriorityBoosted = !isPriorityBoosted;
        virtualCamera.Priority += isPriorityBoosted ? +priorityBoostAmount : -priorityBoostAmount;
    }

}

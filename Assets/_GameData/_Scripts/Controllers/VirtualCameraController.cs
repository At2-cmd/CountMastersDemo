using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class VirtualCameraController : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera runnerPhaseCam; 
    [SerializeField] private CinemachineVirtualCamera stairClimPhaseCam;


    private void OnEnable()
    {
        EventManager.Instance.OnFinishPointReached += OnFinishPointReachedHandler;
    }

    private void OnFinishPointReachedHandler()
    {
        runnerPhaseCam.enabled = false;
        stairClimPhaseCam.enabled = true;
    }
}

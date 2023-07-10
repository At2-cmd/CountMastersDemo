using Cinemachine;
using System;
using UnityEngine;

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

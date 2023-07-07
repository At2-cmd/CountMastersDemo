using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovementController : MovementBase
{
    private Vector3 _targetPosition;
    private Collider collider;

    private void OnEnable()
    {
        EventManager.Instance.OnGameFailed += OnGameFailedHandler;
    }

    private void Awake()
    {
        collider = GetComponent<Collider>();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerCrowdController player))
        {
            collider.enabled = false;
            transform.forward = _targetPosition - transform.position;
            moveToTargetRoutine = StartCoroutine(MoveToFightTarget(_targetPosition, 3));
        }
    }
    private void OnGameFailedHandler()
    {
        StopAllCoroutines();
    }

    public void SetTargetDestination(Vector3 targetPosition)
    {
        _targetPosition = targetPosition;
    }
}

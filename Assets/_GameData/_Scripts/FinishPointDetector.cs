using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishPointDetector : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerStateManager player))
        {
            player.SwitchState(player.stairClimbState);
        }
    }
}

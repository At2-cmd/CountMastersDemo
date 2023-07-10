using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishPointDetector : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerStateManager player))
        {
            player.transform.position = new Vector3(0, player.transform.position.y, player.transform.position.z);
            player.SwitchState(player.stairClimbState);
        }
    }
}

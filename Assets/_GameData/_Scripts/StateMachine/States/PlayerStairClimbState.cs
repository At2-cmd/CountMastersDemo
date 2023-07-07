using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStairClimbState : PlayerBaseState
{
    public override void EnterState(PlayerStateManager player)
    {
        EventManager.Instance.RaiseFinishPointReached();
    }

    public override void ExitState(PlayerStateManager player)
    {
        
    }

    public override void OnTriggerEnterState(PlayerStateManager player, Collider other)
    {
        
    }

    public override void UpdateState(PlayerStateManager player)
    {
        
    }
}

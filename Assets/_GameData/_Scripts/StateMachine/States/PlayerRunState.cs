using UnityEngine;
using DG.Tweening;

public class PlayerRunState : PlayerBaseState
{
    public override void EnterState(PlayerStateManager player)
    {
        EventManager.Instance.RaiseRunStateEntered();
        SmoothLookAtForward(player.transform);
    }

    public override void ExitState(PlayerStateManager player)
    {
        
    }

    public override void OnTriggerEnterState(PlayerStateManager player, Collider other)
    {
        if (other.TryGetComponent(out EnemyMovementController enemy))
        {
            enemy.SetTargetDestination(player.transform.position);
            EventManager.Instance.RaiseFightStarted(enemy.transform.position);
            player.SwitchState(player.fightState);
        }
    }

    public override void UpdateState(PlayerStateManager player)
    {
        
    }

    private void SmoothLookAtForward(Transform player)
    {
        player.transform.DORotate(Vector3.zero,.5f);
    }
}

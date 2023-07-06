using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateManager : MonoBehaviour
{
    
    public PlayerIdleState idleState = new PlayerIdleState();
    public PlayerRunState runState = new PlayerRunState();
    public PlayerFightState fightState = new PlayerFightState();
    public PlayerStairClimbState stairClimbState = new PlayerStairClimbState();
    public PlayerWinState winState = new PlayerWinState();
    public PlayerFailState failState = new PlayerFailState();
    private PlayerBaseState currentState;


    private void Start()
    {
        currentState = idleState;
        currentState.EnterState(this);
    }

    private void Update()
    {
        currentState.UpdateState(this);
    }

    public void SwitchState(PlayerBaseState newState)
    {
        currentState = newState;
        newState.EnterState(this);
    }

    private void OnTriggerEnter(Collider other)
    {
        currentState.OnTriggerEnterState(this, other);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBaseState : MonoBehaviour
{
    protected PlayerStateMachine stateMachine;
    protected readonly PlayerData playerData;

    public PlayerBaseState(PlayerStateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
        playerData = stateMachine.Player.Data.playerData;
    }
}

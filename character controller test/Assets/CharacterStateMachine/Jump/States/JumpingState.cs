using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class JumpingState : PlayerState
{

    public override void EnterState(params System.Object[] data)
    {

        ControlSingleton.Jump.canceled += StopJump;

    }

    private void StopJump(InputAction.CallbackContext obj)
    {

        PlayerPhysics.AccelerationY = PlayerStats.instance.FallingAcceleration;
        PlayerPhysics.VelocityY = 0;

        JumpStateMachine.SwitchState(JumpStateMachine.FallingState);

    }

    public override void FixedUpdateState()
    {
        if (PlayerPhysics.Velocity.y <= 0)
        {

            PlayerPhysics.VelocityY = 0;
            PlayerPhysics.AccelerationY = PlayerStats.instance.FallingAcceleration;
            JumpStateMachine.SwitchState(JumpStateMachine.FallingState);

        }
    }

    public override void ExitState()
    {
        ControlSingleton.Jump.canceled -= StopJump;
    }




}

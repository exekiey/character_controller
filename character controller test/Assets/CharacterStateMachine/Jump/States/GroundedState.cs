using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class GroundedState : PlayerState
{



    public override void EnterState(params object[] data)
    {

        ControlSingleton.Jump.started += DoJump;
    }


    private void DoJump(InputAction.CallbackContext obj)
    {

        PlayerPhysics.AccelerationY = PlayerStats.instance.JumpGravity;
        PlayerPhysics.VelocityY = PlayerStats.instance.JumpInitialVelocity;
        JumpStateMachine.SwitchState(JumpStateMachine.JumpingState);


    }

    public override void LateUpdateState()
    {



        (Vector3, bool) isGrounded = Detections.IsGrounded;

        if (!isGrounded.Item2)
        {
        
            //Debug.Log("que me caigo");

            PlayerPhysics.VelocityY = 0;

            PlayerPhysics.AccelerationY = PlayerStats.instance.FallingAcceleration;
            JumpStateMachine.SwitchState(JumpStateMachine.CoyoteFalling);
        }

    }

    public override void ExitState()
    {
        ControlSingleton.Jump.started -= DoJump;
    }

}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class FallingState : PlayerState
{

    bool isJumpBuffered;

    float timer;

    public override void EnterState(params object[] data)
    {
        isJumpBuffered = false;
        timer = 0;
        ControlSingleton.Jump.performed += BufferJump;
    }

    private void BufferJump(InputAction.CallbackContext obj)
    {

        timer = PlayerStats.instance.JumpBufferTime;
        isJumpBuffered = true;
    }

    public override void UpdateState()
    {

        if (timer <= 0)
        {

            timer -= Time.deltaTime;
            isJumpBuffered = false;

        }

        LimitVelocity();

    }

    private static void LimitVelocity()
    {
        if (PlayerPhysics.VelocityY <= PlayerStats.instance.FallingMaxVelocity)
        {

            PlayerPhysics.AccelerationY = 0;
            PlayerPhysics.VelocityY = PlayerStats.instance.FallingMaxVelocity;

        }
    }

    public override void LateUpdateState()
    {

        (Vector3, bool) isGrounded = Detections.IsGrounded;



        if (isGrounded.Item2)
        {


            if (isJumpBuffered)
            {

                PlayerPhysics.VelocityY = PlayerStats.instance.JumpInitialVelocity;
                PlayerPhysics.AccelerationY = PlayerStats.instance.JumpGravity;
                JumpStateMachine.SwitchState(JumpStateMachine.JumpingState);

            } else
            {

                PlayerPhysics.NextFramePosition = isGrounded.Item1;

                PlayerPhysics.AccelerationY = 0;
                PlayerPhysics.VelocityY = 0;
                JumpStateMachine.SwitchState(JumpStateMachine.GroundedState);
            }


        }

    }

}

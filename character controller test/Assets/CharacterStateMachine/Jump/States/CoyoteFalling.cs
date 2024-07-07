using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CoyoteFalling : PlayerState
{

    float timer;

    public override void EnterState(params object[] data)
    {
        timer = Time.time + PlayerStats.instance.CoyoteTime;

        ControlSingleton.Jump.performed += DoJump;
    }

    public override void UpdateState()
    {
        timer += Time.deltaTime;

        if (timer > PlayerStats.instance.CoyoteTime)
        {

            JumpStateMachine.SwitchState(JumpStateMachine.CoyoteFalling);

        }

    }
    private void DoJump(InputAction.CallbackContext obj)
    {

        Debug.Log("saltar");
        PlayerPhysics.AccelerationY = PlayerStats.instance.JumpGravity;
        PlayerPhysics.VelocityY = PlayerStats.instance.JumpInitialVelocity;
        JumpStateMachine.SwitchState(JumpStateMachine.JumpingState);

    }
    public override void LateUpdateState()
    {

        //Debug.Log("Falling");

        (Vector3, bool) isGrounded = Detections.IsGrounded;

        if (isGrounded.Item2)
        {
            PlayerPhysics.NextFramePosition = isGrounded.Item1;

            PlayerPhysics.AccelerationY = 0;
            PlayerPhysics.VelocityY = 0;
            JumpStateMachine.SwitchState(JumpStateMachine.GroundedState);

        }

    }

    public override void ExitState()
    {
        ControlSingleton.Jump.performed -= DoJump;
    }


}

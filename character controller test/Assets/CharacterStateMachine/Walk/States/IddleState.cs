using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class IddleState : PlayerState
{


    public override void EnterState(params System.Object[] data)
    {

        ControlSingleton.Walk.performed += DoWalk;
    }

    private void DoWalk(InputAction.CallbackContext obj)
    {

        bool isWalkingRight = obj.ReadValue<Vector2>().x > 0;

        if (isWalkingRight && Detections.IsRightWall.Item2)
        {
            return;
        }

        if (!isWalkingRight && Detections.IsLeftWall.Item2)
        {
            return;
        }

        if (JumpStateMachine.OnAir)
        {
            PlayerPhysics.VelocityX = PlayerStats.instance.WalkMaxVelocity * ControlSingleton.MoveDirection;
            WalkStateMachine.SwitchState(WalkStateMachine.OnAirWalkingState);
        }
        else
        {
            PlayerPhysics.AccelerationX = PlayerStats.instance.WalkAcceleration * ControlSingleton.MoveDirection;
            WalkStateMachine.SwitchState(WalkStateMachine.AcceleratingState);
        }


    }



    public override void ExitState()
    {
        ControlSingleton.Walk.performed -= DoWalk;
    }
}

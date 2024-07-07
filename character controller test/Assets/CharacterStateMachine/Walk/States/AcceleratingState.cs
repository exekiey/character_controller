using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AcceleratingState : PlayerState
{

    public override void EnterState(params System.Object[] data)
    {

        ControlSingleton.Walk.canceled += StopWalking;

    }

    public override void FixedUpdateState()
    {

        if (Mathf.Abs(PlayerPhysics.VelocityX) >= PlayerStats.instance.WalkMaxVelocity)
        {
            PlayerPhysics.VelocityX = PlayerStats.instance.WalkMaxVelocity * ControlSingleton.MoveDirection;
            PlayerPhysics.AccelerationX = 0;
            WalkStateMachine.SwitchState(WalkStateMachine.GroundedWalkingState, ControlSingleton.MoveDirection);

        }


        if (JumpStateMachine.OnAir)
        {

            PlayerPhysics.VelocityX = PlayerStats.instance.WalkMaxVelocity * ControlSingleton.MoveDirection;
            PlayerPhysics.AccelerationX = 0;
            WalkStateMachine.SwitchState(WalkStateMachine.GroundedWalkingState, ControlSingleton.MoveDirection);
        }

        DetectWallsAndFixPosition();

    }

    private static void DetectWallsAndFixPosition()
    {
        (Vector2, bool) isLeftWall = Detections.IsLeftWall;

        if (isLeftWall.Item2 && ControlSingleton.MoveDirection == -1)
        {
            PlayerPhysics.NextFramePosition = isLeftWall.Item1;
            PlayerPhysics.VelocityX = 0;
            PlayerPhysics.AccelerationX = 0;
            WalkStateMachine.SwitchState(WalkStateMachine.IddleState);
        }

        (Vector2, bool) isRightWall = Detections.IsRightWall;

        if (isRightWall.Item2 && ControlSingleton.MoveDirection == 1)
        {

            PlayerPhysics.NextFramePosition = isRightWall.Item1;
            PlayerPhysics.VelocityX = 0;
            PlayerPhysics.AccelerationX = 0;
            WalkStateMachine.SwitchState(WalkStateMachine.IddleState);
        }
    }

    private void StopWalking(InputAction.CallbackContext obj)
    {

        PlayerPhysics.AccelerationX = 0;
        PlayerPhysics.VelocityX = 0;
        WalkStateMachine.SwitchState(WalkStateMachine.IddleState);
    }

    public override void ExitState()
    {
        ControlSingleton.Walk.canceled -= StopWalking;
    }
}

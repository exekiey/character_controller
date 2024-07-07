using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class OnAirWalkingState : PlayerState
{


    public override void EnterState(params System.Object[] data)
    {

        ControlSingleton.Walk.canceled += StopWalking;

    }

    private void StopWalking(InputAction.CallbackContext obj)
    {
        PlayerPhysics.VelocityX = 0;
        WalkStateMachine.SwitchState(WalkStateMachine.IddleState);

    }


    public override void UpdateState()
    {
        if (!JumpStateMachine.OnAir)
        {
            WalkStateMachine.SwitchState(WalkStateMachine.GroundedWalkingState);
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

    public override void ExitState()
    {
        ControlSingleton.Walk.canceled -= StopWalking;
    }

}

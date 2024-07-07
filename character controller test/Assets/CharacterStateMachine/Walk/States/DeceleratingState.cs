using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DeceleratingState : PlayerState
{

    int direction;

    public override void EnterState(params System.Object[] data)
    {

        direction = (int)Mathf.Sign(PlayerPhysics.VelocityX);

        ControlSingleton.Walk.performed += DoWalk;

    }

    public override void FixedUpdateState()
    {

        if (Mathf.Sign(PlayerPhysics.VelocityX) != direction)
        {
            PlayerPhysics.VelocityX = 0;
            WalkStateMachine.SwitchState(WalkStateMachine.IddleState);
            PlayerPhysics.AccelerationX = 0;

        }


        (Vector2, bool) isLeftWall = Detections.IsLeftWall;

        if (isLeftWall.Item2)
        {
            PlayerPhysics.NextFramePosition = isLeftWall.Item1;
            PlayerPhysics.VelocityX = 0;
            PlayerPhysics.AccelerationX = 0;
            WalkStateMachine.SwitchState(WalkStateMachine.IddleState);
        }

        (Vector2, bool) isRightWall = Detections.IsRightWall;

        if (isRightWall.Item2)
        {

            PlayerPhysics.NextFramePosition = isRightWall.Item1;
            PlayerPhysics.VelocityX = 0;
            PlayerPhysics.AccelerationX = 0;
            WalkStateMachine.SwitchState(WalkStateMachine.IddleState);
        }

    }

    private void DoWalk(InputAction.CallbackContext obj)
    {

        Vector2 moveDirection = obj.ReadValue<Vector2>();

        float moveAcceleration = moveDirection.x * PlayerStats.instance.WalkAcceleration;

        PlayerPhysics.AccelerationX = moveAcceleration;

        WalkStateMachine.SwitchState(WalkStateMachine.AcceleratingState, moveAcceleration);
    }

    public override void ExitState()
    {
        ControlSingleton.Walk.performed -= DoWalk;
    }
}

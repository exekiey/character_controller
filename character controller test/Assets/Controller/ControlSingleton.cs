using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
public class ControlSingleton : MonoBehaviour
{
    
    private InputAction _jump;
    private InputAction _walk;

    MovementInputAction movement;

    public static ControlSingleton _instance;


    private static float moveDirection;

    public void Awake()
    {
        _instance = this;

        movement = new MovementInputAction();
        
        Jump = movement.Movement.Jump;
        Jump.Enable();

        Walk = movement.Movement.Walk;
        Walk.Enable();

        Walk.performed += SetDirection;

    }

    private void SetDirection(InputAction.CallbackContext obj)
    {
        
        Vector2 moveVector = obj.ReadValue<Vector2>();
        MoveDirection = moveVector.x;

    }

    public static InputAction Jump { get => _instance._jump; set => _instance._jump = value; }
    public static InputAction Walk { get => _instance._walk; set => _instance._walk = value; }
    public static float MoveDirection { get => moveDirection; set => moveDirection = value; }
}

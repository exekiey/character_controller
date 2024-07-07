using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEditor;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [Header("Detection")]
    public float IsGroundedDetection;
    public float IsWallDetection;

    [Header("Falling")]
    public float FallingAcceleration;
    public float FallingMaxVelocity;
    [Space]
    public float CoyoteTime;

    [Header("Jump")]
    public float JumpInitialVelocity;
    public float JumpGravity;
    [Space]
    public float JumpBufferTime;

    [Header("Walk")]
    public float WalkAcceleration;
    public float WalkDeceleration;
    public float WalkMaxVelocity;
    [Space]
    //public float OnAirWalkVelocity;

    [Space]
    public bool AutomaticCalculateJumpVariables;

    [HideInInspector] public float JumpHeight;
    [HideInInspector] public float JumpTime;


    [Space]
    public bool AutomaticCalculateWalkVariables;

    [HideInInspector] public float TimeToReachMaxVelocity;


    public static PlayerStats instance;

    private void Awake()
    {
        instance = this;
    }

    private void OnDrawGizmos()
    {

        GameObject player = GameObject.Find("player");


        MyDebug.DrawLine(player.transform.position, Vector2.down, IsGroundedDetection);
        MyGizmos.DrawLine(player.transform.position, Vector2.right, IsWallDetection);
        MyGizmos.DrawLine(player.transform.position, Vector2.left, IsWallDetection);
    }

}
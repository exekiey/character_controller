using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerPhysics : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] Vector3 _velocity;

    [SerializeField] Vector3 _acceleration;

    [SerializeField] SpriteRenderer _spriteRenderer;

    public static PlayerPhysics _instance;


    Vector2 nextFramePosition;
    bool isNextPositionChanged;

    void Awake()
    {
        _instance = this;
    }
    private void Update()
    {
        transform.position += _velocity * Time.deltaTime;


        _velocity += _acceleration * Time.deltaTime;




        if (_instance.isNextPositionChanged)
        {

            _instance.transform.position = nextFramePosition;

            isNextPositionChanged = false;

        }

    }

    public static Vector3 Velocity { get => _instance._velocity; set => _instance._velocity = value; }
    public static float VelocityX { get => _instance._velocity.x; set => _instance._velocity.x = value; }
    public static float VelocityY { get => _instance._velocity.y; set => _instance._velocity.y = value; }
    public static Vector3 Acceleration { get => _instance._acceleration; set => _instance._acceleration = value; }
    public static float AccelerationX { get => _instance._acceleration.x; set => _instance._acceleration.x = value; }
    public static float AccelerationY { get => _instance._acceleration.y; set => _instance._acceleration.y = value; }
    public static SpriteRenderer SpriteRenderer { get => _instance._spriteRenderer; set => _instance._spriteRenderer = value; }
    public static Vector3 Position { get => _instance.transform.position; set => _instance.transform.position = value; }
    public static Vector3 NextPosition { get => _instance.transform.position + _instance._velocity; }
    public static Vector2 NextFramePosition
    {
        get => _instance.nextFramePosition;
        set
        {

            _instance.isNextPositionChanged = true;

            _instance.nextFramePosition = value;
        }
    }

}

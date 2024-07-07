using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class JumpStateMachine : MonoBehaviour
{

    PlayerState currentState;

    JumpingState jumpingState = new JumpingState();
    FallingState fallingState = new FallingState();
    CoyoteFalling coyoteFalling = new CoyoteFalling();
    GroundedState groundedState = new GroundedState();

    static JumpStateMachine _instance;

    [SerializeField] TextMeshPro currentStateText;

    public static JumpingState JumpingState { get => _instance.jumpingState; }
    public static FallingState FallingState { get => _instance.fallingState; }
    public static GroundedState GroundedState { get => _instance.groundedState; }
    public static CoyoteFalling CoyoteFalling { get => _instance.coyoteFalling; }

    public static bool OnAir { get => _instance.currentState == _instance.fallingState || _instance.currentState == _instance.coyoteFalling || _instance.currentState == _instance.jumpingState; }

    private void Awake()
    {
        _instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        _instance.currentState = GroundedState;
        _instance.currentState.EnterState();
        currentStateText.text = currentState.GetType().ToString();
    }

    // Update is called once per frame
    void Update()
    {
        _instance.currentState.UpdateState();
    }

    private void LateUpdate()
    {
        _instance.currentState.LateUpdateState();
    }

    private void FixedUpdate()
    {
        _instance.currentState.FixedUpdateState();
    }

    public static void SwitchState(PlayerState newState)
    {

        _instance.currentState.ExitState();

        _instance.currentState = newState;


        _instance.currentState.EnterState();
        
        _instance.currentStateText.text = _instance.currentState.GetType().ToString();

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem.LowLevel;

public class WalkStateMachine : MonoBehaviour
{

    IddleState _iddleState = new IddleState();

    GroundedWalkingState _groundedWalkingState = new GroundedWalkingState();
    AcceleratingState _acceleratingState = new AcceleratingState();
    DeceleratingState _deceleratingState = new DeceleratingState();

    OnAirWalkingState _onAirWalkingState = new OnAirWalkingState();

    PlayerState currentState;

    static WalkStateMachine _instance;

    public static IddleState IddleState { get => _instance._iddleState;}

    public static GroundedWalkingState GroundedWalkingState { get => _instance._groundedWalkingState;}
    public static AcceleratingState AcceleratingState { get => _instance._acceleratingState;}
    public static DeceleratingState DeceleratingState { get => _instance._deceleratingState;}

    public static OnAirWalkingState OnAirWalkingState { get => _instance._onAirWalkingState;}


    [SerializeField] TextMeshPro currentStateText;

    private void Awake()
    {
        _instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        currentState = _iddleState;
        currentState.EnterState();
        _instance.currentStateText.text = currentState.GetType().ToString();
    }

    // Update is called once per frame
    void Update()
    {
        currentState.UpdateState();
    }

    private void FixedUpdate()
    {
        currentState.FixedUpdateState();
    }

    public static void SwitchState(PlayerState newState, params object[] data)
    {

        _instance.currentState.ExitState();

        _instance.currentState = newState;

        _instance.currentState.EnterState(data);

        _instance.currentStateText.text = _instance.currentState.GetType().ToString();

    }
}

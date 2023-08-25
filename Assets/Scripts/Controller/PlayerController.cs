using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Model")]
    private Player playerModel;

    [Header("Input")]
    private InputHandler playerInput;
    private InputAction run;
    private InputAction jump;
    private InputAction dash;
    private InputAction push;
    private InputAction look;

    private bool inJumping = false;
    private bool inPushing = false;
    private CollideCheck colCheck;

    #region Life Cycle

    private void Start()
    {
        LoadModel();
        LoadInput();
        colCheck = new CollideCheck(transform, 0.1f);

        SubscribeDataEvents();
        SubscribeStateEvents();
        playerInput.Player.Enable();
    }

    private void Update()
    {
        if (run.IsPressed()) ProcessRun();

        if (dash.IsPressed()) ProcessDash();

        if (jump.IsPressed()) ProcessJump();
        if (inJumping) DuringJump();

        //if (push.IsPressed()) ProcessPush();
        //if (inPushing) DuringPush();

        playerModel.StateMachine.Update();
        colCheck.Update();
    }

    private void OnDestroy()
    {
        DesubscribeDataEvents();
        DesubscribeStateEvents();
        playerInput.Player.Disable();
    }

    #endregion

    #region Set Data in Model

    private void LoadModel()
    {
        if (!gameObject.TryGetComponent(out playerModel))
            Debug.LogWarning("Player Model Missing!");
    }

    private void SubscribeDataEvents()
    {
        if (playerModel == null) return;
    }

    private void DesubscribeDataEvents()
    {
        if (playerModel == null) return;
    }

    #region Level

    public void DownGrade(int amount)
    {
        playerModel.level?.Decrement(amount);
    }

    public void Upgrade(int amount)
    {
        playerModel.level?.Increment(amount);
    }

    #endregion

    #region Health

    public void Damage(int amount)
    {
        playerModel.health?.Decrement(amount);
    }

    public void Heal(int amount)
    {
        playerModel.health?.Increment(amount);
    }

    public void Reset()
    {
        playerModel.health?.Maximise();
    }

    #endregion

    #region Speed

    public void SpeedDown(int amount)
    {
        playerModel.speed?.Decrement(amount);
    }

    public void SpeedUp(int amount)
    {
        playerModel.speed?.Increment(amount);
    }

    public void SpeedMax()
    {
        playerModel.speed?.Maximise();
    }

    #endregion

    #region Energy

    public void LoseEngergy(int amount)
    {
        playerModel.energy?.Decrement(amount);
    }

    public void GainEnergy(int amount)
    {
        playerModel.energy?.Increment(amount);
    }

    public void ResetEnergy()
    {
        playerModel.energy?.Minimise();
    }

    #endregion

    #region Frequency

    public void DropFreq(int amount)
    {
        playerModel.freq?.Decrement(amount);
    }

    public void RiseFreq(int amount)
    {
        playerModel.freq?.Increment(amount);
    }

    public void ResetFreq()
    {
        playerModel.freq?.Maximise();
    }



    #endregion

    #endregion

    #region Set States in Model

    private void SubscribeStateEvents()
    {
        if (playerModel == null) return;
        playerModel.StateMachine.Changed += StateChange;
    }

    private void DesubscribeStateEvents()
    {
        if (playerModel == null) return;
        playerModel.StateMachine.Changed -= StateChange;
    }

    private void StateChange(State fromState, State toState)
    {
        switch (fromState.m_StateType)
        {
            case (int)PlayerStateType.IDLE:
                break;
            case (int)PlayerStateType.RUN:
                break;
            case (int)PlayerStateType.DASH:
                break;
            case (int)PlayerStateType.JUMP:
                break;
            case (int)PlayerStateType.PUSH:
                break;
            default:
                break;
        }

        switch (toState.m_StateType)
        {
            case (int)PlayerStateType.IDLE:
                break;
            case (int)PlayerStateType.RUN:
                break;
            case (int)PlayerStateType.DASH:
                break;
            case (int)PlayerStateType.JUMP:
                break;
            case (int)PlayerStateType.PUSH:
                break;
            default:
                break;
        }
    }

    #endregion

    #region Operate Inputs

    private void LoadInput()
    {
        playerInput = new InputHandler();
        run = playerInput.Player.Run;
        jump = playerInput.Player.Jump;
        dash = playerInput.Player.Dash;
        push = playerInput.Player.Push;
        look = playerInput.Player.Look;
    }

    #region Run

    private void ProcessRun()
    {
        //if (!colCheck.Ground() || colCheck.Edge() || colCheck.Obstacle()) return;
        if (playerModel.speed.Curr < 1) playerModel.speed.Curr = 1;

        Vector2 inputDirect = run.ReadValue<Vector2>();
        Vector3 forward = CameraHandler.Instance.GetCameraForward() * inputDirect.y;
        Vector3 horizontal = CameraHandler.Instance.GetCameraRight() * inputDirect.x;
        Vector3 moveDirection = forward + horizontal;
        moveDirection.Normalize();

        Vector3 moveAmount = playerModel.speed.Curr * Time.deltaTime * moveDirection;
        transform.Translate(moveAmount, Space.World);

        Debug.Log($"Run with speed {playerModel.speed.Curr}");
    }

    #endregion

    #region Dash

    private void ProcessDash()
    {
        if(playerModel.energy.Curr >= 0)
        {
            playerModel.speed.Maximise();
            playerModel.energy.Curr -= 10;
            ProcessRun();
        }

        Debug.Log($"Dash with speed {playerModel.speed.Curr} and energy {playerModel.energy.Curr}");
    }

    #endregion

    #region Jump

    private void DuringJump()
    {
        playerModel.speed.Curr += playerModel.speed.Gravity *
                                playerModel.speed.GravityScale * Time.deltaTime;
        Debug.Log($"gravity: {(int)(playerModel.speed.Gravity * playerModel.speed.GravityScale * Time.deltaTime)}");

        if (colCheck.Ground() && playerModel.speed.Curr < 0)
        {
            playerModel.speed.Curr = 0;
            float offset = 1f;
            Vector3 snappedPosition = new(transform.position.x,
                                          colCheck.ClosestPoint.y + offset,
                                          transform.position.z);

            transform.position = snappedPosition;
            inJumping = false;
        }
        Debug.Log($"isJumping: {inJumping} with speed: {playerModel.speed.Curr}");
        transform.Translate(new Vector3(0, playerModel.speed.Curr, 0) * Time.deltaTime);
    }

    private void ProcessJump()
    {
        playerModel.speed.Curr = 5;
        inJumping = true;
    }

    #endregion

    #region Push

    private void DuringPush()
    {
        inPushing = false;
    }

    private void ProcessPush()
    {
        Debug.Log("Push!");
        inPushing = true;
    }

    #endregion

    #endregion
}


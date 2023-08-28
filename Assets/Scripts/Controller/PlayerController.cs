using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

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

    [Header("Control")]
    private bool inJumping = false;
    private bool inPushing = false;
    private Collider col;
    private CollideCheck colCheck;
    private Vector3 moveDirection;

    #region Life Cycle

    private void Start()
    {
        LoadModel();
        LoadInput();
        LoadControl();

        SubscribeDataEvents();
        SubscribeStateEvents();
        SubscribeInputEvents();

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
        DesubscribeInputEvents();

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

    public void LevelDown(int amount)
    {
        playerModel.level.Curr -= amount;
    }

    public void LevelUp(int amount)
    {
        playerModel.level.Curr += amount;
    }

    #endregion

    #region Health

    public void HealthDown(int amount)
    {
        playerModel.health.Curr -= amount;
    }

    public void HealthUp(int amount)
    {
        playerModel.health.Curr += amount;
    }

    public void HealthReset()
    {
        playerModel.health.Curr = playerModel.health.Max;
    }

    #endregion

    #region Speed

    public void SpeedSet(float amount)
    {
        playerModel.movement.speed.Curr = amount;
    }

    public void SpeedXZSet(float amount)
    {
        playerModel.movement.speed.SpeedXZ = amount;
    }

    public void SpeedXSet(float amount)
    {
        playerModel.movement.speed.SpeedX = amount;
    }

    public void SpeedYSet(float amount)
    {
        playerModel.movement.speed.SpeedY = amount;
    }

    public void SpeedZSet(float amount)
    {
        playerModel.movement.speed.SpeedZ = amount;
    }

    public void SpeedDown(float amount)
    {
        playerModel.movement.speed.Curr -= amount;
    }

    public void SpeedUp(float amount)
    {
        playerModel.movement.speed.Curr += amount;
    }

    public void SpeedMax()
    {
        playerModel.movement.speed.Curr = playerModel.movement.speed.Max;
    }

    public void SpeedMin()
    {
        playerModel.movement.speed.Curr = playerModel.movement.speed.Min;
    }

    #endregion

    #region Energy

    public void EngergyLose(int amount)
    {
        playerModel.energy.Curr -= amount;
    }

    public void EnergyGain(int amount)
    {
        playerModel.energy.Curr += amount;
    }

    public void EnergyReset()
    {
        playerModel.energy.Curr = playerModel.energy.Min;
    }

    #endregion

    #region Frequency

    public void FreqDrop(float amount)
    {
        playerModel.freq.Curr -= amount;
    }

    public void FreqRise(float amount)
    {
        playerModel.freq.Curr += amount;
    }

    public void FreqReset()
    {
        playerModel.freq.Curr = playerModel.freq.Max;
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
            default:
                break;
        }

        switch (toState.m_StateType)
        {
            case (int)PlayerStateType.IDLE:
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

    private void SubscribeInputEvents()
    {
        run.performed += SpeedXZChanged;
        dash.performed += SpeedXZChanged;
        jump.performed += SpeedYChanged;
        push.performed += SpeedYChanged;

        run.canceled += SpeedXZChanged;
        dash.canceled += SpeedXZChanged;
        jump.canceled += SpeedYChanged;
        push.canceled += SpeedYChanged;
    }

    private void DesubscribeInputEvents()
    {
        run.performed -= SpeedXZChanged;
        dash.performed -= SpeedXZChanged;
        jump.performed -= SpeedYChanged;
        push.performed -= SpeedYChanged;

        run.canceled -= SpeedXZChanged;
        dash.canceled -= SpeedXZChanged;
        jump.canceled -= SpeedYChanged;
        push.canceled -= SpeedYChanged;
    }

    private void SpeedYChanged(InputAction.CallbackContext context)
    {
        SpeedYSet(playerModel.movement.speed.Max);
    }

    private void SpeedXZChanged(InputAction.CallbackContext context)
    {
        SpeedXSet(context.ReadValue<Vector2>().x * playerModel.movement.speed.Curr);
        SpeedZSet(context.ReadValue<Vector2>().y * playerModel.movement.speed.Curr);
        SpeedXZSet((Math.Abs(context.ReadValue<Vector2>().x) + Math.Abs(context.ReadValue<Vector2>().y)) * playerModel.movement.speed.Curr);
    }

    private void LoadControl()
    {
        colCheck = new CollideCheck(transform, playerModel.movement.CheckRange);
        col = transform.gameObject?.GetComponent<Collider>();
        moveDirection = new Vector3();
    }

    #region Run

    private void ProcessRun()
    {
        Vector2 inputDirect = run.ReadValue<Vector2>();
        Vector3 forward = CameraHandler.Instance.GetCameraForward() * inputDirect.y;
        Vector3 horizontal = CameraHandler.Instance.GetCameraRight() * inputDirect.x;
        moveDirection = forward + horizontal;
        moveDirection.Normalize();

        if (!colCheck.Ground() || colCheck.Edge(moveDirection) || colCheck.Obstacle(moveDirection))
            return;
        
        if (playerModel.movement.speed.Curr < 1)
            SpeedSet(1f);

        Vector3 moveAmount = playerModel.movement.speed.Curr * Time.deltaTime * moveDirection;
        transform.Translate(moveAmount, Space.World);
    }

    #endregion

    #region Dash

    private void ProcessDash()
    {
        if(playerModel.energy.Curr >= 0)
        {
            SpeedMax();
            EngergyLose(10);
            ProcessRun();
        }

        Debug.Log($"Dash with speed {playerModel.movement.speed.Curr} and energy {playerModel.energy.Curr}");
    }

    #endregion

    #region Jump

    private void DuringJump()
    {
        float grav = playerModel.movement.Gravity * playerModel.movement.GravityScale * Time.deltaTime;
        SpeedUp(grav);

        if (colCheck.Ceil() && playerModel.movement.speed.Curr > 0)
        {
            SpeedSet(0);
        }

        if (colCheck.Ground() && playerModel.movement.speed.Curr < 0)
        {
            SpeedSet(0);
            Vector3 snappedPosition = new(transform.position.x,
                                          colCheck.ClosestPoint.y + playerModel.movement.CheckRange,
                                          transform.position.z);

            transform.position = snappedPosition;
            inJumping = false;
        }

        transform.Translate(new Vector3(0, playerModel.movement.speed.Curr, 0) * Time.deltaTime);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        if (col == null) return;

        // Ceil
        Gizmos.DrawCube(new Vector3(col.bounds.center.x, col.bounds.max.y, col.bounds.center.z),
                new Vector3(playerModel.movement.CheckRange, playerModel.movement.CheckRange * -0.5f, playerModel.movement.CheckRange) * 2);

        // Ground
        Gizmos.DrawCube(new Vector3(col.bounds.center.x, col.bounds.min.y, col.bounds.center.z),
                        new Vector3(playerModel.movement.CheckRange, playerModel.movement.CheckRange * 0.5f, playerModel.movement.CheckRange) * 2);
        // Obstacle
        Gizmos.DrawLine(col.bounds.center,
                        col.bounds.center + moveDirection * (col.bounds.size.x / 2 + playerModel.movement.CheckRange));

        //Edge
        Gizmos.DrawLine(col.bounds.center + moveDirection * (col.bounds.size.x / 2 + playerModel.movement.CheckRange),
                        col.bounds.center + moveDirection * (col.bounds.size.x / 2 + playerModel.movement.CheckRange)
                        + Vector3.down * (col.bounds.size.y / 2 + playerModel.movement.CheckRange));
    }

    private void ProcessJump()
    {
        if(colCheck.Ground()) SpeedMax();
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


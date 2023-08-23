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

    //    [SerializeField] private LayerMask groundLayer;
    //    [SerializeField] private float moveSpeed = 5f;
    //    [SerializeField] private float dashSpeed = 10f;
    //    [SerializeField] private float dashEnergyConsumption = 10f;
    //    [SerializeField] private float energyRegenRate = 5f;
    //    [SerializeField] private float maxEnergy = 100f;

    //    private float currentEnergy;
    //    private Vector2 moveInput;
    //    private bool isGrounded;

    #region Life Cycle

    private void Start()
    {
        LoadModel();
        LoadInput();
        SubscribeDataEvents();
        SubscribeStateEvents();
        playerInput.Player.Enable();
    }

    private void Update()
    {
        if (run.IsPressed()) ProcessRun();
        if (dash.triggered) ProcessDash();
        if (jump.IsPressed()) ProcessJump();
        if (push.triggered) ProcessPush();
        playerModel.StateMachine.Update();
    }

    //    private void Start()
    //    {
    //        currentEnergy = maxEnergy;
    //    }

    //    private void Update()
    //    {
    //        CheckGrounded();
    //        ProcessMovement();
    //        ProcessDash();
    //    }

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
                ExitDashState();
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
                EnterDashState();
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
        Debug.Log("Load Input");
    }

    #region Run

    private void ProcessRun()
    {
        //if (!IsGrounded(0.3f)) return;

        Vector2 inputDirect = run.ReadValue<Vector2>();
        Vector3 forward = CameraHandler.Instance.GetCameraForward() * inputDirect.y;
        Vector3 horizontal = CameraHandler.Instance.GetCameraRight() * inputDirect.x;
        Vector3 moveDirection = forward + horizontal;
        moveDirection.Normalize();
        transform.Translate(playerModel.speed.Curr * Time.deltaTime * moveDirection, Space.World);

        CameraHandler.Instance.LookAt();

        Debug.Log($"Run with speed {playerModel.speed.Curr} to " +
                  $"direction [{moveDirection.x}, {moveDirection.y}, {moveDirection.z}]");
    }

    #endregion

    #region Dash

    private void ProcessDash()
    {
        playerModel.speed.Maximise();

        Debug.Log($"Dash with speed {playerModel.speed.Curr}");
    }

//    private void ProcessDash()
//    {
//        if (DashInput())
//        {
//            if (currentEnergy >= dashEnergyConsumption)
//            {
//                Vector3 cameraForward = cameraTransform.forward;
//                cameraForward.y = 0f;
//                cameraForward.Normalize();

//                Vector3 dashDirection = cameraForward;
//                Vector3 dashAmount = dashDirection * dashSpeed * Time.deltaTime;

//                playerTransform.Translate(dashAmount, Space.World);

//                currentEnergy -= dashEnergyConsumption;
//                currentEnergy = Mathf.Max(currentEnergy, 0f);
//            }
//        }
//        else
//        {
//            currentEnergy = Mathf.Min(currentEnergy + energyRegenRate * Time.deltaTime, maxEnergy);
//        }
//    }

//    private bool DashInput()
//    {
//        return InputHandler.instance.Dash.triggered;
//    }

    private void EnterDashState()
    {
        playerModel.speed.Maximise();
    }

    private void ExitDashState()
    {
        playerModel.speed.Curr = playerModel.speed.Min;
    }

    #endregion

    #region Jump

    private void ProcessJump()
    {
        playerModel.speed.Curr += (int)(playerModel.speed.Gravity *
                                        playerModel.speed.GravityScale * Time.deltaTime);

        if (IsGrounded(0.3f)) playerModel.speed.Curr = 10;

        transform.Translate(new Vector3(0, playerModel.speed.Curr, 0) * Time.deltaTime);

        Debug.Log("Jump!");
    }

    private bool IsGrounded(float rayLength)
    {
        Ray ray = new(transform.position + Vector3.up * rayLength, Vector3.down);

        if (Physics.Raycast(ray, out _, rayLength))
        {
            Debug.Log("Is Grounded!");
            return true;
        }

        Debug.Log("Not Grounded!");
        return false;
    }

    //    private void CheckGrounded()
    //    {
    //        isGrounded = Physics.OverlapBox(playerTransform.position, playerTransform.localScale / 2,
    //                                        Quaternion.identity, groundLayer).Length > 0;
    //    }

    #endregion

    #region Push

    private void ProcessPush()
    {
        Debug.Log("Push!");
    }

    #endregion

    #endregion
}

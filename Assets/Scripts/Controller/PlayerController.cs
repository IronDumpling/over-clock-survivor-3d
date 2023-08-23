using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Model")]
    private Player playerModel;

    [Header("View")]

    [Header("Input")]
    private InputHandler playerInput;
    private InputAction run;
    private InputAction jump;
    private InputAction dash;
    private InputAction push;
    private InputAction look;

    #region Life Cycle

    private void Start()
    {
        LoadModel();
        LoadInput();
        SubscribeDataEvents();
        SubscribeInputEvents();
    }

    private void Update()
    {
        if (run.IsPressed()) DoRun();
    }

    private void OnEnable()
    {
        SubscribeDataEvents();
        SubscribeInputEvents();
    }

    private void OnDisable()
    {
        DesubscribeDataEvents();
        DesubscribeInputEvents();
    }

    private void OnDestroy()
    {
        DesubscribeDataEvents();
        DesubscribeInputEvents();   
    }

    #endregion

    #region Set Data in Model

    private void LoadModel()
    {
        playerModel = gameObject.GetComponent<Player>();
        if (playerModel == null) Debug.LogWarning("Player Model Missing!");
    }

    private void SubscribeDataEvents()
    {
        if (playerModel == null) return;
        playerModel.level.Changed += OnLevelChanged;
        playerModel.health.Changed += OnHealthChanged;
        playerModel.speed.Changed += OnSpeedChanged;
        playerModel.energy.Changed += OnEnergyChanged;
        playerModel.freq.Changed += OnFreqChanged;
    }

    private void DesubscribeDataEvents()
    {
        if (playerModel == null) return;
        playerModel.level.Changed -= OnLevelChanged;
        playerModel.health.Changed -= OnHealthChanged;
        playerModel.speed.Changed -= OnSpeedChanged;
        playerModel.energy.Changed -= OnEnergyChanged;
        playerModel.freq.Changed -= OnFreqChanged;
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

    public void UpdateLevelView()
    {

    }

    public void OnLevelChanged()
    {
        UpdateLevelView();
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

    public void UpdateHealthView()
    {

    }

    public void OnHealthChanged()
    {
        UpdateHealthView();
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

    public void UpdateSpeedView()
    {

    }

    public void OnSpeedChanged()
    {
        UpdateSpeedView();
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

    public void UpdateEnergyView()
    {

    }

    public void OnEnergyChanged()
    {
        UpdateEnergyView();
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

    public void UpdateFreqView()
    {

    }

    public void OnFreqChanged()
    {
        UpdateFreqView();
    }

    #endregion

    #endregion

    #region Set States in Model

    private void EnterRunState(InputAction.CallbackContext context)
    {

    }

    private void ExitRunState(InputAction.CallbackContext context)
    {

    }

    private void EnterDashState(InputAction.CallbackContext context)
    {

    }

    private void ExitDashState(InputAction.CallbackContext context)
    {

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

    private void SubscribeInputEvents()
    {
        if (playerInput == null) return;
        run.performed += EnterRunState;
        run.canceled += ExitRunState;
        dash.performed += EnterDashState;
        dash.canceled += ExitDashState;
        playerInput.Player.Enable();
    }

    private void DesubscribeInputEvents()
    {
        if (playerInput == null) return;
        run.performed -= EnterRunState;
        run.canceled -= ExitRunState;
        dash.performed -= EnterDashState;
        dash.canceled -= ExitDashState;
        playerInput.Player.Disable();
    }

    #region Run

    private void DoRun()
    {
        Vector2 inputDirect = run.ReadValue<Vector2>();
        Vector3 forward = CameraHandler.Instance.GetCameraForward() * inputDirect.y;
        Vector3 horizontal = CameraHandler.Instance.GetCameraRight() * inputDirect.x;
        Vector3 runment = Vector3.ClampMagnitude(forward + horizontal, 1);
        transform.Translate(playerModel.speed.Curr * Time.deltaTime * runment, Space.World);
        CameraHandler.Instance.LookAt();

        Debug.Log("Run!");
    }

    #endregion

    #region Dash

    private void DoDash()
    {
        playerModel.speed.Maximise();

        Debug.Log("Dash!");
    }

    #endregion

    #region Jump

    private void DoJump()
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
            return true;
        return false;
    }

    #endregion

    #region Push

    private void DoPush()
    {
        Debug.Log("Push!");
    }

    #endregion

    #endregion
}


